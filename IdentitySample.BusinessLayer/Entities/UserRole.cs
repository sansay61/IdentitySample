using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities
{
    public class UserRole
    {
        public UserRole(User user, Role role)
        {
            User = user;
            Role = role;
        }
        public UserRole()
        {

        }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as UserRole;
            if (t == null) return false;
            if (User.Id == t.User.Id
             && Role.Id == t.Role.Id)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ User.Id.GetHashCode();
            hash = (hash * 397) ^ Role.Id.GetHashCode();

            return hash;
        }
        #endregion
    } 
   
}
