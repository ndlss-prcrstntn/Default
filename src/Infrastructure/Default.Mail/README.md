# Default.Mail

SMTP email sending with HTML page templates and configuration binding.

## Scope

- Outbound mail transport — content and triggers belong in domain modules.

## Integration

- **Project references:** `Default.Abstractions`, `Default.DependencyInjection`
- **Consumers:** `Default.Api`

## DI / configuration

- **DI extensions:** `EmailServiceCollectionExtensions.cs`
- **Config sections:** `EmailConfigSection.cs`

## Key files

- `EmailConfigSection.cs`
- `EmailService.cs`
- `EmailServiceCollectionExtensions.cs`
- `IEmailService.cs`
- `Pages\ConfirmEmailPage.html`
- `Pages\ForgotPasswordPage.html`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
