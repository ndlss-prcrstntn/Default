# Default.Client

Ionic Angular SPA skeleton for the Default host.

## Run

```bash
cd src/Default.Client
npm ci   # or npm install
npm start
```

App: `http://localhost:4200` → API `http://localhost:5000` (see `src/environments`).

## Structure

```text
src/app/
  layout/           # shell + tab bar
  module/home/      # smoke page (/Ping)
  module/profile/   # shows GET /version
  interceptors/     # auth stub
```

Add feature areas under `module/` and regenerate an OpenAPI client (NSwag) when the API grows.
