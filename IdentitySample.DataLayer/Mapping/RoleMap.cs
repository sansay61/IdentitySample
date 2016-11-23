using FluentNHibernate.Mapping;
using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Mapping
{
      public class RoleMap : ClassMap<Role>
     {

        public RoleMap()
         {
             Table("ASPNETROLES");
             LazyLoad();
             Id(x => x.Id).Column("ID").GeneratedBy.Assigned();
             Map(x => x.Name).Column("NAME");
             HasMany(x => x.UserRoles).KeyColumn("ROLEID");
         }
     }
}
