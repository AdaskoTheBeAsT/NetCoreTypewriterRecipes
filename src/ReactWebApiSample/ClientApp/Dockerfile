FROM node:18.0.0-alpine as build

WORKDIR /app

COPY package.json yarn.lock ./

RUN yarn install --frozen-lockfile

COPY . .
RUN yarn build

FROM adaskothebeast/nginx-brotli:v1.1.0 as deploy

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

WORKDIR /usr/share/nginx/html

COPY --from=build /../wwwroot .

ENTRYPOINT ["nginx", "-g", "daemon off;"]
