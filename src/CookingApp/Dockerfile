FROM node:20-slim AS base
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN npm install -g corepack@latest
RUN corepack enable

WORKDIR /usr/server/app
COPY . /usr/server/app

RUN pnpm install
RUN pnpm --filter=@chub/cooking-app build
ENV NODE_ENV=production
CMD [ "pnpm", "--filter=@chub/cooking-app", "start" ]