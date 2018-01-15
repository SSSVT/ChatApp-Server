using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ESChatServer.Areas.v1.Models.Application.Objects
{
    public static class UserObtainer
    {
        public static string GetCurrentUserUsername(IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
        }
    }
}
