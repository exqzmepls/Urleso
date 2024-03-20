# Urleso

Simple URL shortener implemented using dotnet and C#

## Features

- Shortening URLs to compact ones
- Redirecting from shortened URLs to original long links
- UI system theme

## How to run local instance

Requirements: `docker` and `docker compose V2+`

1. Clone this repository
2. Open your terminal in [`src`](src) directory
3. Run following command

```shell
docker compose up -d
```

Docker will run containers and you can use Urleso:

- Urleso UI is available via http://localhost:6080
- Urleso API is accessible on http://localhost:6800
- Specify `localhost` as host and `6432` as port to connect to PostgreSQL database
- Also [WEB API](http://localhost:6808) and [WEB UI](http://localhost:6088) logs are provided by Seq

Fill free to explore [`.env`](src/.env) file and change default values as you wish (containers restart is needed)

## TODO features

- Distributed cache & redirecter app
- Users accounts & autorization
- CQRS pattern
- ...