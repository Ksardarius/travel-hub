FROM node:20-slim AS base
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable

WORKDIR /usr/server/app

COPY ./package.json ./
RUN pnpm install
COPY ./ .
RUN pnpm build
ENV NODE_ENV=production
CMD [ "pnpm", "start" ]