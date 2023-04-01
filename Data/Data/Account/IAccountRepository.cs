using FTS.Model.Account;
using System.Threading.Tasks;

namespace FTS.Data.Account
{
    public interface IAccountRepository
    {


        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Login(string username, string password);


        Task<bool> SaveRefreshToken(string token, int userId);

        Task<User> GetRefreshToken(string token);

    }
}
