using Addapptables.Boilerplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Addapptables.Boilerplate.Authorization.Impersonation
{
    public class UserAndIdentity
    {
        public User User { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public UserAndIdentity(User user, ClaimsIdentity identity)
        {
            User = user;
            Identity = identity;
        }
    }
}
