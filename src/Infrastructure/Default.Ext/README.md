# Default.Ext  Проект Default.Ext в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions - Consumers: Default.Api   ## DI / Config - DI files: None - Config sections: LicenseConfigSection.cs   ## Key Files
- CryptoGenerator.cs
- License\DateEncryptor.cs
- License\LicenseConfigSection.cs
- License\LicenseException.cs
- License\LicenseService.cs
- UidLockManager.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
