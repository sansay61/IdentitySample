using FluentNHibernate.Mapping;
using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Mapping
{
    public class ClaimMap : ClassMap<Claim>
    {
        public ClaimMap()
        {
            Table("ASPNETUSERCLAIMS");
            LazyLoad();
            Id(x => x.ClaimId).Column("ID").GeneratedBy.Increment();
            Map(x => x.ClaimType).Column("CLAIMTYPE");
            Map(x => x.ClaimValue).Column("CLAIMVALUE");
            References(x => x.User, "USERID");
        }
    }
}
