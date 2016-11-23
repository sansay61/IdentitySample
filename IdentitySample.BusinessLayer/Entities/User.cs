using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.BusinessLayer.Entities
{
    public class User
    {
        private IList<Claim> _claims;
        private IList<Login> _logins;
        private IList<UserRole> _roles;
        public virtual string Id { get; set; }
        [StringLength(512)]
        public virtual string Email { get; set; }
        [Required]
        public virtual short Emailconfirmed { get; set; }
        [StringLength(512)]
        public virtual string Passwordhash { get; set; }
        [StringLength(512)]
        public virtual string Securitystamp { get; set; }
        [StringLength(512)]
        public virtual string Phonenumber { get; set; }
        [Required]
        public virtual short Phonenumberconfirmed { get; set; }
        [Required]
        public virtual short Twofactorenabled { get; set; }
        [Required]
        [StringLength(512)]
        public virtual string Username { get; set; }
        public virtual IList<Login> Logins
        {
            get
            {
                return _logins ??
                    (_logins = new List<Login>());
            }
            set { _logins = value; }
        }

        public virtual IList<UserRole> UserRoles
        {
            get { return _roles ?? (_roles = new List<UserRole>()); }
            set { _roles = value; }
        }
        public virtual IList<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set
            {
                _claims = value;
            }
        }



    }
}
