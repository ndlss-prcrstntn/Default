using Microsoft.Extensions.Options;

namespace Default.Ext.License;

public class LicenseService(IOptions<LicenseConfigSection> options)
{
    private readonly DateTime? _expiryDateUtc = options.Value.Key != null
        ? DateEncryptor.DecryptDate(options.Value.Key).ToUniversalTime()
        : null;

    public void CheckLicense(TimeSpan offset = default)
    {
        if (_expiryDateUtc == null)
            throw new LicenseException(
                "Лицензионный ключ не указан.");

        if (DateTime.UtcNow.AddDays(-offset.TotalDays) > _expiryDateUtc)
            throw new LicenseException("Ваша лицензия истекла.");
    }
}