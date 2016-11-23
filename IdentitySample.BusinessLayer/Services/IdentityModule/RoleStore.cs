using IdentitySample.BusinessLayer.Entities.IdentityModule;
using IdentitySample.BusinessLayer.Entities;
using IdentitySample.BusinessLayer.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Services.IdentityModule
{
    public class RoleService : IRoleStore<IdentityRole, System.String>, IQueryableRoleStore<IdentityRole, System.String>, IDisposable
    {
        private IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
        {
            _repository =repository;
        }

        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = getRole(role);
            return Task.Factory.StartNew(() => _repository.Create(r));
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = getRole(role);
            return Task.Factory.StartNew(() => _repository.Delete(r.Id));
        }


        public System.Threading.Tasks.Task<IdentityRole> FindByIdAsync(string roleId)
        {
            var role = _repository.GetById(roleId);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var role = _repository.FindByName(roleName);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            var r = getRole(role);
            return Task.Factory.StartNew(() => _repository.Update(r));
        }


        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _repository
                    .GetAll()
                    .Select(x => getIdentityRole(x))
                    .AsQueryable();
            }
        }
        private Role getRole(IdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            return new Role
            {
                Id = identityRole.Id,
                Name = identityRole.Name
            };
        }

        private IdentityRole getIdentityRole(Role role)
        {
            if (role == null)
                return null;
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }
        public void Dispose()
        {

        }
    }
}
