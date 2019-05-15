using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Authorization.Accounts.Dto
{
    public class ImpersonateInput
    {
        public int? TenantId { get; set; }

        public long UserId { get; set; }
    }
}
