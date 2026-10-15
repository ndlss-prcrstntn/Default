# Default.Mail  Проект Default.Mail в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions, Default.DependencyInjection - Consumers: Default.Api   ## DI / Config - DI files: EmailServiceCollectionExtensions.cs - Config sections: EmailConfigSection.cs   ## Key Files
- EmailConfigSection.cs
- EmailService.cs
- EmailServiceCollectionExtensions.cs
- IEmailService.cs
- Pages\ConfirmEmailPage.html
- Pages\ForgotPasswordPage.html

См. AGENT_CONTEXT.md и DEPENDENCY.md.
