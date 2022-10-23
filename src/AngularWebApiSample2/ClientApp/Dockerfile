################# Build #################
FROM node:18.0.0-alpine as build

# install all native build libs
RUN apk add --no-cache --update \
  bash \
  curl \
  openjdk11 \
  unzip

# set java env variables for sonar
ENV JAVA_HOME=/usr/lib/jvm/default-jvm/jre \
  PATH="/usr/lib/jvm/default-jvm/jre/bin:$PATH"

# download latest version of sonar
# it needs to be stored in special folder for
# sonarqube-scanner
ENV SONAR_SCANNER_VERSION=4.7.0.2747
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
COPY package.json yarn.lock decorate-angular-cli.js ./

# restore with lockfile - less frequent layer rebuild
RUN yarn install --frozen-lockfile

# copy whole content to build
COPY . .

# patch libs with patches folder
RUN yarn postinstall

# build all
RUN yarn build --prod

RUN yarn test:ci

RUN yarn lint:ci:docker

RUN yarn stylelint:ci:json:docker

RUN yarn htmlhint:ci:html:docker

################# Final #################
# FROM adaskothebeast/nginx-brotli:v1.3.0 as deploy

# COPY ./nginx.conf /etc/nginx/conf.d/default.conf

# WORKDIR /usr/share/nginx/html

# COPY --from=build /../wwwroot .
# COPY --from=build .reports ./.reports

# ENTRYPOINT ["nginx", "-g", "daemon off;"]
