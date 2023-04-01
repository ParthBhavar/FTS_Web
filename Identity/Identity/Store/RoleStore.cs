using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Model;
using Microsoft.AspNetCore.Identity;
namespace Identity
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly IApplicationRoleRepository _applicationRoleRepository;

        public RoleStore(IApplicationRoleRepository applicationRoleRepository)
        {
            _applicationRoleRepository = applicationRoleRepository;
        }

        /// <summary>Creates a new role in a store as an asynchronous operation.</summary>
        /// <param name="role">The role to create in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the asynchronous query.</returns>
        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _applicationRoleRepository.CreateAsync(role, cancellationToken);           
            return IdentityResult.Success;
        }

        /// <summary>Updates a role in a store as an asynchronous operation.</summary>
        /// <param name="role">The role to update in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the asynchronous query.</returns>
        /// <exception cref="ArgumentNullException">role</exception>
        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            await _applicationRoleRepository.UpdateAsync(role, cancellationToken);         
            return IdentityResult.Success;
        }

        /// <summary>Deletes a role from the store as an asynchronous operation.</summary>
        /// <param name="role">The role to delete from the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult"/> of the asynchronous query.</returns>
        /// <exception cref="ArgumentNullException">role</exception>
        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            await _applicationRoleRepository.DeleteAsync(role.Id,cancellationToken);           
            return IdentityResult.Success;
        }

        /// <summary>Gets the ID for a role from the store as an asynchronous operation.</summary>
        /// <param name="role">The role whose ID should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that contains the ID of the role.</returns>
        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        /// <summary>Gets the name of a role from the store as an asynchronous operation.</summary>
        /// <param name="role">The role whose name should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that contains the name of the role.</returns>
        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        /// <summary>Sets the name of a role in the store as an asynchronous operation.</summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        /// <summary>Get a role's normalized name as an asynchronous operation.</summary>
        /// <param name="role">The role whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that contains the name of the role.</returns>
        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        /// <summary>Set a role's normalized name as an asynchronous operation.</summary>
        /// <param name="role">The role whose normalized name should be set.</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        /// <summary>Finds the role who has the specified ID as an asynchronous operation.</summary>
        /// <param name="roleId">The role ID to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that result of the look up.</returns>
        /// <exception cref="ArgumentNullException">roleId</exception>
        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(roleId))
                throw new ArgumentNullException(nameof(roleId));
            return await _applicationRoleRepository.FindByIdAsync(Convert.ToInt32(roleId), cancellationToken);
        }

        /// <summary>Finds the role who has the specified normalized name as an asynchronous operation.</summary>
        /// <param name="normalizedRoleName">The normalized role name to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1"/> that result of the look up.</returns>
        /// <exception cref="ArgumentNullException">normalizedRoleName</exception>
        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));
            return await _applicationRoleRepository.FindByNormalizedNameAsync(normalizedRoleName, cancellationToken);            
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
