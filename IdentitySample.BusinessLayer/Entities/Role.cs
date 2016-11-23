using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities
{
    public class Role
    {
        private IList<UserRole> _users;
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<UserRole> UserRoles
        {
            get { return _users ?? (_users = new List<UserRole>()); }
            set { _users = value; }
        }
    }
}
