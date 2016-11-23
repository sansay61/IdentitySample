using IdentitySample.BusinessLayer.Helpers;
using IdentitySample.BusinessLayer.Repositories;
using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public User FindByUserName(string username)
        {
            return GetAll().FirstOrDefault(x => x.Username == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Username == username));
        }

        public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Task.Factory.StartNew(() => GetAll().FirstOrDefault(x => x.Username == username),cancellationToken);
        }
    }
}
