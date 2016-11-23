using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities.IdentityModule
{
    public class IdentityRole:IRole<System.String>
    {
        public IdentityRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string name)
            : this()
        {
            this.Name = name;
        }

        public IdentityRole(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
