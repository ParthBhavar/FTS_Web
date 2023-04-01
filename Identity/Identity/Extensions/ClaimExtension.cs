using Newtonsoft.Json;
using System.Security.Claims;

namespace Identity.Extensions
{
    public static class ClaimExtension
    {
        public static T GetUserFromClaim<T>(this Claim claim)
        {
          return  JsonConvert.DeserializeObject<T>(claim.Value);
        }
    }
}
