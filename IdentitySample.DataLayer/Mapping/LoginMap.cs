
using FluentNHibernate.Mapping;
using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.DataLayer.Mapping
{
    public class LoginMap : ClassMap<Login>
    {
        public LoginMap()
        {
            Table("ASPNETUSERLOGINS");
            CompositeId().
                KeyProperty(x => x.Loginprovider).KeyProperty(x => x.Providerkey).KeyReference(x => x.User);
       }
    }
}
