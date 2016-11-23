using FluentNHibernate.Mapping;
using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Mapping
{
    public class UserRoleMap : ClassMap<UserRole> 
    {
        public UserRoleMap()
        {
            Table("ASPNETUSERROLES");
            LazyLoad();
            CompositeId().KeyReference(x => x.User).KeyReference(x => x.Role);
        }

    }
}
