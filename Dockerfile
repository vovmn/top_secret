FROM golang:1.25.1-alpine AS builder

RUN apk add --no-cache git alpine-sdk

WORKDIR /app

COPY go.mod go.sum ./

RUN go mod download

COPY . .

RUN go build -ldflags="-s -w" -o server ./cmd/server-storage-api/main.go

FROM alpine:latest

WORKDIR /root/

COPY --from=builder /app/server .

COPY --from=builder /app/migrations /root/migrations

COPY --from=builder /app/.env .

RUN apk add --no-cache sqlite

RUN sqlite3 /root/database.db < /root/migrations/init.sql

EXPOSE 3020

CMD ["./server"]