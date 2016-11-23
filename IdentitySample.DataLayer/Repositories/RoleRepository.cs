using IdentitySample.BusinessLayer.Entities;
using IdentitySample.BusinessLayer.Helpers;
using IdentitySample.BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository (IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Role FindByName(string roleName)
        {
            return GetAll().FirstOrDefault(x => x.Name == roleName);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Name == roleName));
        }

        public Task<Role> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Name == roleName),cancellationToken);
        }
    }
}
