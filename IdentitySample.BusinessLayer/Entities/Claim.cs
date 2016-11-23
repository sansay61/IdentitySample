using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Entities
{
    public class Claim
    {
        private User _user;

        public virtual int ClaimId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        public virtual User User { get; set; }
    }
        
}
