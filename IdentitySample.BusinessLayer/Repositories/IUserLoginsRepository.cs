using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Repositories
{
    public interface IUserLoginsRepository : IRepository<Login>
    {
        Login GetByProviderAndKey(string loginProvider, string providerKey);
        Task<Login> GetByProviderAndKeyAsync(string loginProvider, string providerKey);
        Task<Login> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey);
    }
}
