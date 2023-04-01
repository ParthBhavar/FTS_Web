using Cryptography;
using Email;
using FTS.Configuration;
using FTS.Data.Account;
using FTS.Email;
using FTS.Model.Account;
using FTS.Model.Constants;
using FileManager;
using JwtManager;
using Logger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using FTS.Extensions;
using FTS.Model.Entities;
using FTS.Model.DTO;
using AutoMapper;

namespace FTS.Business.Account
{
    public class AccountBl : IAccountBl
    {
        #region Private Variables


        private readonly IAppConfiguration _appConfiguration;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogging _logger;
        private readonly ICipher _cipher;
        #endregion

        #region Constructor
        public AccountBl(
              IAppConfiguration appConfiguration,
              ILoggerFactory loggerFactory,
              IAccountRepository accountRepository,
              ICipher cipher,
              ITokenGenerator tokenGenerator)
        {
            _appConfiguration = appConfiguration;
            _logger = loggerFactory.CreateLogger<AccountBl>();
            _accountRepository = accountRepository;
            _cipher = cipher;
            _tokenGenerator = tokenGenerator;
        }

        #endregion

        #region public Methods


        /// <summary>
        /// Authenticate/login user on the basis of given user name and password
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Sign in result</returns>
        public async Task<bool> Login(string userName, string password)
        {
            string encryptPassword = _cipher.Encrypt(password);
            return await _accountRepository.Login(userName, encryptPassword);
        }


        /// <summary>
        /// Validate Token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ValidateToken(string token)
        {
            bool response = false;
            string email = GetUserNameFromToken(token, _appConfiguration.JwtForgotPasswordSettings.SecretKey,
                _appConfiguration.JwtForgotPasswordSettings.Issuer, _appConfiguration.JwtForgotPasswordSettings.Audience);
            return response;
        }

        public async Task<bool> SaveRefreshToken(string token, int userId)
        {
            return await _accountRepository.SaveRefreshToken(token, userId);
        }

        public async Task<FTS.Model.Account.User> GetRefreshToken(string token)
        {
            return await _accountRepository.GetRefreshToken(token);
        }

        #endregion

        #region private

        /// <summary>
        /// To get Email from Token by User Claim.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="issuer"></param>
        /// <param name="Audience"></param>
        /// <returns></returns>
        private string GetUserNameFromToken(string token, string secretKey, string issuer, string Audience)
        {
            string email = string.Empty;
            var principal = _tokenGenerator.GetPrincipal(token,
               secretKey, issuer, Audience);
            if (principal != null)
            {
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
                Claim usernameClaim = identity?.FindFirst(ClaimTypes.Email);
                if (usernameClaim != null) email = usernameClaim.Value;
            }
            else
            {
                _logger.Log(ReflectionExtensions.GetClassName() + " => " + ReflectionExtensions.GetMethodName());
            }
            return email;
        }




        #endregion
    }
}
