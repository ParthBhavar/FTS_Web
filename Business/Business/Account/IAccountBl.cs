using FTS.Configuration.Options;
using FTS.Model.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FTS.Model.Enum;

namespace FTS.Business.Account
{
    public interface IAccountBl
    {
        /// <summary>
        /// Authenticate/login user on the basis of given user name and password
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Sign in result</returns>
        Task<bool> Login(string userName, string password);


        /// <summary>
        /// Validate Token
        /// </summary>
        /// <returns></returns>
        Task<bool> ValidateToken(string token);


        Task<bool> SaveRefreshToken(string token, int userId);

        /// <summary>
        /// get refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> GetRefreshToken(string token);

    }
}
