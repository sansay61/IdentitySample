using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using IdentitySample.BusinessLayer.Entities;

namespace IdentitySample.DataLayer.Mapping
{
    public class UserMap : ClassMap<User>
    {

        public UserMap() {
            Table("ASPNETUSERS");
            Id(x => x.Id).Column("ID").GeneratedBy.Assigned();
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Emailconfirmed).Column("EMAILCONFIRMED").Not.Nullable();
            Map(x => x.Passwordhash).Column("PASSWORDHASH");
            Map(x => x.Securitystamp).Column("SECURITYSTAMP");
            Map(x => x.Phonenumber).Column("PHONENUMBER");
            Map(x => x.Phonenumberconfirmed).Column("PHONENUMBERCONFIRMED");
            Map(x => x.Twofactorenabled).Column("TWOFACTORENABLED");
            Map(x => x.Username).Column("USERNAME").Not.Nullable();
            HasMany(x => x.Logins).KeyColumn("USERID");
            HasMany(x => x.UserRoles).KeyColumn("USERID");
            HasMany(x => x.Claims).KeyColumn("USERID");
        }
    }
}
