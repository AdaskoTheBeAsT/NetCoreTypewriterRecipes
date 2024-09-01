################# Build #################
FROM node:20.17.0-alpine AS build

# install all native build libs
RUN apk add --no-cache --update \
  bash \
  curl \
  openjdk11 \
  unzip \
  zstd

RUN npm i --location=global dotenv-cli

# set java env variables for sonar
ENV JAVA_HOME=/usr/lib/jvm/default-jvm/jre \
  PATH="/usr/lib/jvm/default-jvm/jre/bin:$PATH"

# download latest version of sonar
# it needs to be stored in special folder for
# sonarqube-scanner
ENV SONAR_SCANNER_VERSION=6.1.0.4477
ENV SONAR_BINARY_CACHE=/tmp
ENV SONAR_ZIP_LOCATION=${SONAR_BINARY_CACHE}/sonar-scanner.zip
ENV SONAR_MAIN_FOLDER=${SONAR_BINARY_CACHE}/.sonar/native-sonar-scanner

# download sonar launcher
RUN curl --output ${SONAR_ZIP_LOCATION} https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-${SONAR_SCANNER_VERSION}.zip

# store in special folder
# based on https://github.com/bellingard/sonar-scanner-npm/blob/6071ae7928a77138e6651e665097284c8accaa1d/src/sonar-scanner-executable.js#L62
RUN mkdir -p ${SONAR_MAIN_FOLDER} && \
  unzip ${SONAR_ZIP_LOCATION} -d ${SONAR_MAIN_FOLDER} && \
  mv ${SONAR_MAIN_FOLDER}/sonar-scanner-${SONAR_SCANNER_VERSION} ${SONAR_MAIN_FOLDER}/sonar-scanner-${SONAR_SCANNER_VERSION}-linux && \
  rm ${SONAR_ZIP_LOCATION}

# add sonar scanner to path
ENV PATH="${SONAR_MAIN_FOLDER}/sonar-scanner-${SONAR_SCANNER_VERSION}-linux/bin:$PATH"

# https://docs.sonarqube.org/latest/analysis/scan/sonarscanner/
# Java heap space error or java.lang.OutOfMemoryError
ENV SONAR_SCANNER_OPTS="-Xmx512m"

# create and change app folder
WORKDIR /app

# copy inly files necessary for node packages restore
COPY package.json yarn.lock check-zstd.js ./

# restore with lockfile - less frequent layer rebuild
RUN yarn install --frozen-lockfile

# copy whole content to build
COPY . .

# patch libs with patches folder
#RUN yarn postinstall

# build all
RUN yarn build

RUN yarn test

RUN yarn lint

#RUN yarn stylelint:ci:json:docker

#RUN yarn htmlhint:ci:html:docker

################# Final #################
FROM adaskothebeast/nginx-brotli:v1.27.0-slim AS deploy

RUN rm /etc/nginx/conf.d/default.conf
COPY ./nginx.conf /etc/nginx/nginx.conf.template
RUN mv /etc/nginx/nginx.conf /etc/nginx/nginx.conf.orig
COPY ./nginx.conf /etc/nginx/nginx.conf

ENV uri=\$uri

WORKDIR /var/www
COPY --from=build /../wwwroot .
COPY ./.env .

# Create user
RUN adduser -D -g 'www' www

# Set permissions
RUN mkdir -p /template \
  && mkdir -p /var/lib/nginx \
  && chown -R www:www /etc/nginx \
  && chown -R www:www /var/lib/nginx \
  && chown -R www:www /var/cache/nginx \
  && chown -R www:www /var/www \
  && chown -R www:www /tmp \
  && chmod -R 770 /tmp

VOLUME ["/var/cache/nginx"]

# Expose server ports
EXPOSE 8080

COPY env.sh /usr/local/bin/env.sh
RUN chmod +x /usr/local/bin/env.sh

# export command exports given env variable to be visible by substenv
# iterate over files in source folder
# use envsubs to modify env variables then compress by gzip brotli
# then copy back to /var/www
# higly inspired by two articles
# https://developers.redhat.com/blog/2021/03/04/making-environment-variables-accessible-in-front-end-containers#conclusion
# in comments there is information about "sponge"
# https://github.com/joellord/frontend-containers/blob/main/envvars.md
# https://github.com/joellord/frontend-containers/blob/main/rootless.md
# https://github.com/joellord/frontend-containers/tree/main/react-project
# also nginx.conf is modified by envsubst
ENTRYPOINT ["sh", "-c", "export API_BASE_URL \
  && export APP_TENANT_ID \
  && export APP_CLIENT_ID \
  && export API_SCOPES \
  && /usr/local/bin/env.sh \
  && envsubst </etc/nginx/nginx.conf.template >/tmp/nginx.conf \
  && nginx -g 'daemon off;'"]
USER www
