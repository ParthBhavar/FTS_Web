using Dapper;
using FTS.Data.Common;
using FTS.Model.Account;
using FTS.Model.Common;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static FTS.Model.Enum;

using Data.Generic;

namespace FTS.Data.Account
{
    public class AccountRepository : IAccountRepository
    {

        #region Private Variables
        private readonly IRepository<User> _userRepository;
        #endregion

        #region Constructor
        public AccountRepository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Login(string username, string password)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@in_username", username);
            param.Add("@in_password", password);
            param.Add("@out_response", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _userRepository.ExecuteProcedureAsync(SPConstants.AuthenticateUser, param);
            return Convert.ToBoolean(param.Get<byte>("@out_response"));
        }

        public async Task<bool> SaveRefreshToken(string token, int userId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@in_user_id", userId);
            param.Add("@in_refresh_token", token);
            await _userRepository.ExecuteProcedureAsync(SPConstants.SaveRefreshToken, param);
            return true;
        }

        public async Task<User> GetRefreshToken(string token)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@in_refresh_token", token);
            return await _userRepository.QuerySingleByProcedureAsync(SPConstants.GetRefreshToken, param);
        }

        #endregion

    }
}

