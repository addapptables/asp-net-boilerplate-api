using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Addapptables.Boilerplate.Storage;

namespace Addapptables.Boilerplate.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public virtual Guid? ProfilePictureId { get; set; }

        public virtual BinaryObject ProfilePicture { get; set; }

        public string ProfilePictureBase64 { get; set; }


        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();
            return user;
        }

        public override void SetNewPasswordResetCode()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(10).ToUpperInvariant();
        }
    }
}
