# Default.Web  Проект Default.Web в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Cache - Consumers: Default.Api   ## DI / Config - DI files: None - Config sections: JwtConfigSection.cs   ## Key Files
- Errors\ApiException.cs
- Errors\ApiResponse.cs
- Errors\ApiValidationErrorResponse.cs
- JwtConfigSection.cs
- TimeSpanConverter.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
