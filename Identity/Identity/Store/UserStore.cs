using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using Identity.Model;
using Identity.Data;
using System.Security.Claims;

namespace Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>,IUserLockoutStore<ApplicationUser>
    {
        IApplicationUserRepository _applicationUserRepository;
        IUserClaimRepository _userClaimRepository;
        /// <summary>Initializes a new instance of the <see cref="UserStore"/> class.</summary>
        /// <param name="applicationUserRepository">The application user repository.</param>
        /// <param name="userClaimRepository">The user claim repository.</param>
        public UserStore(IApplicationUserRepository applicationUserRepository, IUserClaimRepository userClaimRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _userClaimRepository = userClaimRepository;
        }
        /// <summary>Creates the specified <paramref name="user" /> in the user store.</summary>
        /// <param name="user">The user to create.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the creation operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _applicationUserRepository.CreateUserAysnc(user,cancellationToken);            
            return IdentityResult.Success;
        }

        /// <summary>Deletes the specified <paramref name="user" /> from the user store.</summary>
        /// <param name="user">The user to delete.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the update operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _applicationUserRepository.DeleteUserAysnc(user,cancellationToken);           
            return IdentityResult.Success;
        }

        /// <summary>Finds and returns a user, if any, who has the specified <paramref name="userId" />.</summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.</returns>
        /// <exception cref="ArgumentNullException">userId</exception>
        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            return await _applicationUserRepository.FindUserByIdAsync(Convert.ToInt32(userId),cancellationToken);            
        }

        /// <summary>Finds and returns a user, if any, who has the specified normalized user name.</summary>
        /// <param name="normalizedUserName">The normalized user name to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName" /> if it exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">normalizedUserName</exception>
        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(normalizedUserName))
                throw new ArgumentNullException(nameof(normalizedUserName));
            return await _applicationUserRepository.FindUserByNameAsync(normalizedUserName.ToLower(),cancellationToken);           
        }

        /// <summary>Gets the normalized user name for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.</returns>
        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        /// <summary>Gets the user identifier for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose identifier should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user" />.</returns>
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        /// <summary>Gets the user name for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.</returns>
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        /// <summary>Sets the given normalized name for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="normalizedName">The normalized name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(0);
        }

        /// <summary>Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        /// <summary>Updates the specified <paramref name="user" /> in the user store.</summary>
        /// <param name="user">The user to update.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the update operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _applicationUserRepository.UpdateUserAysnc(user, cancellationToken);
            return IdentityResult.Success;
        }

        /// <summary>Sets the <paramref name="email" /> address for a <paramref name="user" />.</summary>
        /// <param name="user">The user whose email should be set.</param>
        /// <param name="email">The email to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>Gets the email address for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user" />.</returns>
        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>Gets a flag indicating whether the email address for the specified <paramref name="user" /> has been verified, true if the email address is verified otherwise
        /// false.</summary>
        /// <param name="user">The user whose email confirmation status should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="user" />
        /// has been confirmed or not.
        /// </returns>
        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>Sets the flag indicating whether the specified <paramref name="user" />'s email address has been confirmed or not.</summary>
        /// <param name="user">The user whose email confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>Gets the user, if any, associated with the specified, normalized email address.</summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object containing the results of the asynchronous lookup operation, the user if any associated with the specified normalized email address.</returns>
        /// <exception cref="ArgumentNullException">normalizedEmail</exception>
        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(normalizedEmail))
                throw new ArgumentNullException(nameof(normalizedEmail));
            return await _applicationUserRepository.FindUserByEmailAsync(normalizedEmail, cancellationToken);           
        }

        /// <summary>Returns the normalized email for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose email address to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.</returns>
        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>Sets the normalized email for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose email address to set.</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.FromResult(0);
        }

        /// <summary>Sets the telephone number for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose telephone number should be set.</param>
        /// <param name="phoneNumber">The telephone number to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>Gets the telephone number, if any, for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose telephone number should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the user's telephone number, if any.</returns>
        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>Gets a flag indicating whether the specified <paramref name="user" />'s telephone number has been confirmed.</summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a confirmed
        /// telephone number otherwise false.
        /// </returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>Sets a flag indicating if the specified <paramref name="user" />'s phone number has been confirmed.</summary>
        /// <param name="user">The user whose telephone number confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating whether the user's telephone number has been confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>Sets a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.</summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="enabled">A flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>Returns a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.</summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified
        /// <paramref name="user" /> has two factor authentication enabled or not.
        /// </returns>
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>Sets the password hash for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose password hash to set.</param>
        /// <param name="passwordHash">The password hash to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>Gets the password hash for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose password hash to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user" />.</returns>
        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>Gets a flag indicating whether the specified <paramref name="user" /> has a password.</summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a password
        /// otherwise false.
        /// </returns>
        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        /// <summary>Add the specified <paramref name="user" /> to the named role.</summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="roleName">The name of the role to add the user to.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _applicationUserRepository.AssignRoleToUserAysnc(user.Id, roleName, cancellationToken);            
        }

        /// <summary>Remove the specified <paramref name="user" /> from the named role.</summary>
        /// <param name="user">The user to remove the named role from.</param>
        /// <param name="roleName">The name of the role to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _applicationUserRepository.RemoveRoleFromUserAysnc(user.Id, roleName, cancellationToken);           
        }

        /// <summary>Gets a list of role names the specified <paramref name="user" /> belongs to.</summary>
        /// <param name="user">The user whose role names to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing a list of role names.</returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
           IEnumerable<ApplicationRole> roleList = await _applicationUserRepository.GetUserRoleAsync(user.Id, cancellationToken);
            return roleList.Select(x => x.Name).ToList();          
        }

        /// <summary>Returns a flag indicating whether the specified <paramref name="user" /> is a member of the given named role.</summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="roleName">The name of the role to be checked.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user" /> is
        /// a member of the named role.
        /// </returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            IEnumerable<ApplicationRole> roleList = await _applicationUserRepository.GetUserRoleAsync(user.Id, cancellationToken);
            return roleList.Any(x => x.Name.ToLower() == roleName.ToLower());           
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

        /// <summary>Returns a list of Users who are members of the named role.</summary>
        /// <param name="roleName">The name of the role whose membership should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing a list of users who are in the named role.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>Gets the last <see cref="T:System.DateTimeOffset"/> a user's last lockout expired, if any.
        /// Any time in the past should be indicates a user is not locked out.</summary>
        /// <param name="user">The user whose lockout date should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that represents the result of the asynchronous query, a <see cref="T:System.DateTimeOffset"/> containing the last time
        /// a user's lockout expired, if any.
        /// </returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEndDateTimeUtc.HasValue
                ? new DateTimeOffset?(DateTime.SpecifyKind(user.LockoutEndDateTimeUtc.Value, DateTimeKind.Utc))
                : null);
        }

        /// <summary>Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a user.</summary>
        /// <param name="user">The user whose lockout date should be set.</param>
        /// <param name="lockoutEnd">The <see cref="T:System.DateTimeOffset"/> after which the <paramref name="user" />'s lockout should end.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEndDateTimeUtc = lockoutEnd?.UtcDateTime;
            return Task.FromResult<object>(null);
        }

        /// <summary>Records that a failed access has occurred, incrementing the failed access count.</summary>
        /// <param name="user">The user whose cancellation count should be incremented.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the incremented failed access count.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>Resets a user's failed access count.</summary>
        /// <param name="user">The user whose failed access count should be reset.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        /// <remarks>This is typically called after the account is successfully accessed.</remarks>
        public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.AccessFailedCount = 0;
            return Task.FromResult<object>(null);
        }

        /// <summary>Retrieves the current failed access count for the specified <paramref name="user" />.</summary>
        /// <param name="user">The user whose failed access count should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, containing the failed access count.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>Retrieves a flag indicating whether user lockout can enabled for the specified user.</summary>
        /// <param name="user">The user whose ability to be locked out should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation, true if a user can be locked out, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>Set the flag indicating if the specified <paramref name="user" /> can be locked out.</summary>
        /// <param name="user">The user whose ability to be locked out should be set.</param>
        /// <param name="enabled">A flag indicating if lock out can be enabled for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEnabled = enabled;
            return Task.FromResult<object>(null);
        }

        //public IList<Claim> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
        //    }

        //    IEnumerable<UserClaim> userClaims= _userClaimRepository.GetClaimsAsync(user);
        //    return userClaims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList();
        //}

        //public void AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
        //    }

        //    if (claims == null)
        //    {
        //        throw new ArgumentNullException(nameof(claims), "Parameter claims is not set to an instance of an object.");
        //    }

        //    cancellationToken.ThrowIfCancellationRequested();
        //   IList<UserClaim> userClaims=claims.Select(e => new UserClaim
        //    {                
        //        UserId = user.Id,
        //        ClaimType = e.Type,
        //        ClaimValue = e.Value
        //    }).ToList();
        //    userClaims.ToList().ForEach(x=> {
        //        _userClaimRepository.AddClaimsAsync(user,x.ClaimType,x.ClaimValue);
        //    });           
        //}

        //public void ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
        //    }

        //    if (claim == null)
        //    {
        //        throw new ArgumentNullException(nameof(claim), "Parameter claim is not set to an instance of an object.");
        //    }

        //    if (newClaim == null)
        //    {
        //        throw new ArgumentNullException(nameof(newClaim), "Parameter newClaim is not set to an instance of an object.");
        //    }

        //    cancellationToken.ThrowIfCancellationRequested();
        //    _userClaimRepository.UpdateClaimsAsync(user,claim.Type,claim.Value, newClaim.Type,newClaim.Value);
        //}

        //public Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IList<Claim>> IUserClaimStore<ApplicationUser>.GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IUserClaimStore<ApplicationUser>.AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IUserClaimStore<ApplicationUser>.ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>Adds the claims asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets the claims asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">user - Parameter user is not set to an instance of an object.</exception>
        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            IEnumerable<UserClaim> userClaims = _userClaimRepository.GetClaimsAsync(user);
            IList<Claim> result = userClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            return Task.FromResult(result);
        }

        /// <summary>Gets the users for claim asynchronous.</summary>
        /// <param name="claim">The claim.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>Removes the claims asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        /// <summary>Replaces the claim asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <param name="newClaim">The new claim.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
