# Default.Api

ASP.NET host / composition root for the Default skeleton.

## Role

- Transport, DI wiring, middleware, OpenAPI, observability endpoints.
- Must not become a domain module — business rules live in `src/Modules/*`.

## Run

```bash
cd src/Default.Api
cp appsettings.Development.local.json.example appsettings.Development.local.json
# fill secrets, or use dotnet user-secrets
dotnet run
```

- API: `http://localhost:5000`
- Swagger (local): `/swagger`
- Health: `/health/live`, `/health/ready`, `/version`

## Adding a module

1. Create `src/Modules/Default.FeatureX`.
2. Add project reference from this host.
3. Register in `DependencyInjection/AppServicesCollectionExtension.cs`.
