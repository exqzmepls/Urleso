# Urleso

Simple URL shortener implemented using dotnet and C#

## Features

- Shortening URLs to compact ones
- Redirecting from shortened URLs to original long links
- UI system theme

## How to run local instance

_`docker` and `docker compose V2+` are required_

1. Clone this repository
2. Open your terminal in [`src`](src) directory
3. Copy [`.env.sample`](src/.env.sample) file

    ```shell
    cp .env.sample .env
    ```

4. Run following command

    ```shell
    docker compose up -d
    ```

Docker will run the API, the database and the migrator. The redirect service and the web UI live in their own repositories and run with their own docker compose files — start them after this stack, since both join its `urleso_default` network:

```shell
git clone https://github.com/exqzmepls/urleso-redirect.git
cd urleso-redirect
cp .env.sample .env
docker compose up -d
```

```shell
git clone https://github.com/exqzmepls/urleso-web.git
cd urleso-web
cp .env.sample .env
docker compose up -d
```

The database credentials in urleso-redirect's `.env` must match the ones here, and `REDIRECT_PUBLIC_URL` here must match the port urleso-redirect publishes — that address is what the API puts in the short URLs it hands out.

Feel free to explore your [`.env`](src/.env) file and change default values as you wish (containers restart is needed)

### Endpoints

By default Urleso services are available at:

- <http://localhost:6800> -> API service
- `postgresql://localhost:6432/urleso` -> PostgreSQL database
- <http://localhost:6880> -> redirect service (started from the [urleso-redirect](https://github.com/exqzmepls/urleso-redirect) repository)
- <http://localhost:6080> -> web app (started from the [urleso-web](https://github.com/exqzmepls/urleso-web) repository)

### Logging

Logs are available in docker containers logs or via Seq:

- [API service](http://localhost:6808)
- [redirect service](http://localhost:6888) (started from the [urleso-redirect](https://github.com/exqzmepls/urleso-redirect) repository)

The web app logs to the browser console.

## TODO features

- Distributed cache
- User accounts & authorization service
- CQRS pattern
- ...
