using IdentitySample.BusinessLayer.Entities.IdentityModule;
using IdentitySample.BusinessLayer.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Services.IdentityModule
{
    public class UserService : IUserLoginStore<IdentityUser, System.String>, IUserClaimStore<IdentityUser, System.String>, IUserRoleStore<IdentityUser, System.String>, IUserPasswordStore<IdentityUser, System.String>, IUserSecurityStampStore<IdentityUser, string>, IUserStore<IdentityUser, string>, IDisposable
    {
        private IUserRepository _repositoryUser;
        private IRoleRepository _repositoryRole;
        private IUserLoginsRepository _repositoryUserLogins;

        
        public UserService(IUserRepository repositoryUser,IUserLoginsRepository repositoryUserLogins,IRoleRepository repositoryRole)
        {
            _repositoryUser = repositoryUser;
            _repositoryRole = repositoryRole;
            _repositoryUserLogins = repositoryUserLogins;
        }

        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var r = getUser(user);
            return Task.Factory.StartNew(() => _repositoryUser.Create(r));
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var r = getUser(user);
            return Task.Factory.StartNew(() => _repositoryUser.Delete(r.Id));
        }


        public System.Threading.Tasks.Task<IdentityUser> FindByIdAsync(string userId)
        {
            var user = _repositoryUser.GetById(userId);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }



        public System.Threading.Tasks.Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = _repositoryUser.FindByUserName(userName);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public System.Threading.Tasks.Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var u = _repositoryUser.GetById(user.Id);
            if (u==null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            populateUser(u, user);
            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }


        public Task AddClaimAsync(IdentityUser user, System.Security.Claims.Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var c = new Entities.Claim

            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                User=u
            };
            u.Claims.Add(c);
            return Task.Factory.StartNew(()=> _repositoryUser.Update(u));
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<Claim>>(u.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var c = u.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            u.Claims.Remove(c);

            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }
    

        #region IUserLoginStore<IdentityUser, Guid> Members
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var l = new Entities.Login
            {
                Loginprovider = login.LoginProvider,
                Providerkey = login.ProviderKey,
                User = u
            };
            u.Logins.Add(l);

            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var identityUser = default(IdentityUser);
            var l = _repositoryUserLogins.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            if (l != null)
                identityUser = getIdentityUser(l.User);

            return Task.FromResult<IdentityUser>(identityUser);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<UserLoginInfo>>(u.Logins.Select(x => new UserLoginInfo(x.Loginprovider, x.Providerkey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var l = u.Logins.FirstOrDefault(x => x.Loginprovider == login.LoginProvider && x.Providerkey == login.ProviderKey);
            u.Logins.Remove(l);


            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }
        #endregion

        #region IUserRoleStore<IdentityUser, Guid> Members
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            var r = _repositoryRole.FindByName(roleName);
            if (r == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

            u.UserRoles.Add(new Entities.UserRole(u, r));
            _repositoryUser.Update(u);

            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<string>>(u.UserRoles.Select(x => x.User.Username).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<bool>(u.UserRoles.Any(x => x.Role.Name == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");
            var u = _repositoryUser.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var r = u.UserRoles.FirstOrDefault(x => x.Role.Name== roleName);
            u.UserRoles.Remove(r);

            return Task.Factory.StartNew(() => _repositoryUser.Update(u));
        }
        #endregion

        #region IUserPasswordStore<IdentityUser, Guid> Members
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUser, Guid> Members
        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
       

        private void populateUser(Entities.User user, IdentityUser identityUser)
        {
            user.Id = identityUser.Id;
            user.Username = identityUser.UserName;
            user.Passwordhash = identityUser.PasswordHash;
            user.Securitystamp = identityUser.SecurityStamp;
        }
        

        private void populateIdentityUser(IdentityUser identityUser, Entities.User user)
        {
            identityUser.Id = user.Id;
            identityUser.UserName = user.Username;
            identityUser.PasswordHash = user.Passwordhash;
            identityUser.SecurityStamp = user.Securitystamp;
        }
        #endregion


        public IQueryable<IdentityUser> Users
        {
            get
            {
                return _repositoryUser
                    .GetAll()
                    .Select(x => getIdentityUser(x))
                    .AsQueryable();
            }
        }
        private IdentitySample.BusinessLayer.Entities.User getUser(IdentityUser identityUser)
        {
            if (identityUser == null)
                return null;
            return new IdentitySample.BusinessLayer.Entities.User
            {
                Id = identityUser.Id,
                Username = identityUser.UserName
            };
        }

        private IdentityUser getIdentityUser(IdentitySample.BusinessLayer.Entities.User user)
        {
            if (user == null)
                return null;
            return new IdentityUser
            {
                Id = user.Id,
                UserName = user.Username
            };
        }

        public void Dispose()
        {

        }
    }
}
