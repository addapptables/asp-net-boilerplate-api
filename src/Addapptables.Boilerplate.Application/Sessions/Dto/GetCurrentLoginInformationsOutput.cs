﻿namespace Addapptables.Boilerplate.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }

        public UserLoginInfoDto User { get; set; }

        public TenantLoginInfoDto Tenant { get; set; }

        public int? ImpersonatorTenantId { get; set; }

        public long? ImpersonatorUserId { get; set; }
    }
}
