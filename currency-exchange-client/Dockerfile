FROM node:17.2.0-alpine AS builder
WORKDIR /app
EXPOSE 3000

ARG REACT_APP_SERVER_URL
ENV REACT_APP_SERVER_URL=$REACT_APP_SERVER_URL

COPY package.json .
COPY yarn.lock .
RUN yarn install --frozen-lockfile

COPY . .
RUN yarn build
RUN yarn global add serve

CMD serve -s build