using Dapper;
using Data.Generic;
using Identity.Model;
using System.Collections.Generic;

namespace Identity.Data
{
    public class UserClaimRepository:IUserClaimRepository
    {
        IRepository<UserClaim> _repository;
        public UserClaimRepository(IRepository<UserClaim> repository)
        {
            _repository = repository;
        }
        public  IEnumerable<UserClaim> GetClaimsAsync(ApplicationUser user)
        {
           var param = new DynamicParameters();
            param.Add("user_id",user.Id);         
         return    _repository.QueryListByProcedure("get_user_claims", param);      
        }
        public void AddClaimsAsync(ApplicationUser user,string claimType,string claimValue)
        {
            var param = new DynamicParameters();
            param.Add("user_id", user.Id);
            param.Add("claim_type", claimType);
            param.Add("claim_value", claimValue);
            _repository.ExecuteProcedure("add_user_claim", param);
        }
        public void UpdateClaimsAsync(ApplicationUser user, string claimType, string claimValue,string newClaimType,string newClaimValue)
        {
            var param = new DynamicParameters();
            param.Add("user_id", user.Id);
            param.Add("claim_type", claimType);
            param.Add("claim_value", claimValue);
            param.Add("new_claim_type", newClaimType);
            param.Add("new_claim_value", newClaimValue);
            _repository.ExecuteProcedure("update_user_claim", param);
        }
    }
}
