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

Docker will run containers and you can use Urleso :D

The web UI lives in the [urleso-web](https://github.com/exqzmepls/urleso-web) repository and runs with its own docker compose:

```shell
git clone https://github.com/exqzmepls/urleso-web.git
cd urleso-web
cp .env.sample .env
docker compose up -d
```

Feel free to explore your [`.env`](src/.env) file and change default values as you wish (containers restart is needed)

### Endpoints

By default Urleso services are available at:

- <http://localhost:6080> -> web app (started from the [urleso-web](https://github.com/exqzmepls/urleso-web) repository)
- <http://localhost:6800> -> API service
- <http://localhost:6880> -> redirect service
- `postgresql://localhost:6432/urleso` -> PostgreSQL database

### Logging

Logs are available in docker containers logs or via Seq:

- [API service](http://localhost:6808)
- [redirect service](http://localhost:6888)

The web app logs to the browser console.

## TODO features

- Distributed cache
- User accounts & authorization service
- CQRS pattern
- ...
