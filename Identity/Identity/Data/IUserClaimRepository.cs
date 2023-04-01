using Identity.Model;
using System.Collections.Generic;

namespace Identity.Data
{
    public interface IUserClaimRepository
    {
        IEnumerable<UserClaim> GetClaimsAsync(ApplicationUser user);
        void AddClaimsAsync(ApplicationUser user, string claimType, string claimValue);
        void UpdateClaimsAsync(ApplicationUser user, string claimType, string claimValue, string newClaimType, string newClaimValue);
    }
}
