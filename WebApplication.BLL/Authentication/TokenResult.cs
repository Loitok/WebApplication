using System;

namespace WebApplication.BLL.Authentication
{
    class TokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}
