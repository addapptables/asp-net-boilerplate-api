﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Models.TokenAuth
{
    public class ImpersonatedAuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }
    }
}
