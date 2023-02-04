FROM node:16-alpine as builder
ENV NODE_ENV=production
WORKDIR /app
COPY ["package*.json", "./"]
RUN npm install --force --production --silent && mv node_modules ../
COPY . .
RUN npm run build

FROM nginx:stable-alpine as publish
ENV NODE_ENV production
WORKDIR /usr/share/nginx/html
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /app/build/ .
EXPOSE 3000
CMD ["nginx", "-g", "daemon off;"]