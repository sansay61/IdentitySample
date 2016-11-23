using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities.IdentityModule
{
    public class IdentityUser : IUser<System.String>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
    }
}