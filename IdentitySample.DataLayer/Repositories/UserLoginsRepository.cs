using IdentitySample.BusinessLayer.Entities;
using IdentitySample.BusinessLayer.Helpers;
using IdentitySample.BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Repositories
{
    public class UserLoginsRepository : Repository<Login>, IUserLoginsRepository
    {
        public UserLoginsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Login GetByProviderAndKey(string loginProvider, string providerKey)
        {
            return GetAll().FirstOrDefault(x => x.Loginprovider == loginProvider && x.Providerkey == providerKey);
        }

        public Task<Login> GetByProviderAndKeyAsync(string loginProvider, string providerKey)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Loginprovider == loginProvider && x.Providerkey == providerKey));
        }

        public Task<Login> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Loginprovider == loginProvider && x.Providerkey == providerKey), cancellationToken);
        }

    }
}
