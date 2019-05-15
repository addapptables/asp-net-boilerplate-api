using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Url
{
    public interface IAppUrlService
    {
        string CreateEmailActivationUrlFormat(int? tenantId);

        string CreatePasswordResetUrlFormat(int? tenantId);

        string CreateEmailActivationUrlFormat(string tenancyName);

        string CreatePasswordResetUrlFormat(string tenancyName);

        string GetTenantUrl(int? tenantId);

        string GetTenantUrl(string tenancyName);
    }
}
