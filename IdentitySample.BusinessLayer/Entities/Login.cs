
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities
{
    public class Login
    {
        private User _user;
        public virtual string Loginprovider { get; set; }
        public virtual string Providerkey { get; set; }

        public virtual User User { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as Login;
            if (t == null) return false;
            if (Loginprovider == t.Loginprovider
             && Providerkey == t.Providerkey
             && User.Id == t.User.Id)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ Loginprovider.GetHashCode();
            hash = (hash * 397) ^ Providerkey.GetHashCode();
            hash = (hash * 397) ^ User.Id.GetHashCode();

            return hash;
        }
    }
}
