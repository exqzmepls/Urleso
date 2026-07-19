#!/bin/sh
# Blazor WASM reads wwwroot/appsettings.json in the browser, so container env vars
# are not visible to the app; regenerate the file from them on every start.
set -e

: "${API_BASE_ADDRESS:?API_BASE_ADDRESS must be set}"

cat > /usr/share/nginx/html/appsettings.json << EOF
{
  "Api": {
    "BaseAddress": "${API_BASE_ADDRESS}"
  }
}
EOF
