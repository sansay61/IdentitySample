using IdentitySample.BusinessLayer.Entities.IdentityModule;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Services.IdentityModule
{
    public class UserManager : UserManager<IdentityUser, string>
    {
        private UserService _store;
        public UserManager(UserService store)
            : base(store)
        {
            _store = store;
        }



        public override Task<IdentityUser> FindAsync(string userName, string passwordHash)
        {
            if (_store.FindByNameAsync(userName).Result.PasswordHash == passwordHash)
                return _store.FindByNameAsync(userName);
            else return null;
        }
        public override Task<IList<string>> GetRolesAsync(string userId)
        {
            return _store.GetRolesAsync(_store.FindByIdAsync(userId).Result);
        }

        public override Task<bool> IsInRoleAsync(string userId, string role)
        {
            return _store.IsInRoleAsync(_store.FindByIdAsync(userId).Result, role);
        }

    }
}