using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Heavy.Web.Models
{
    public class ApplicationUser: IdentityUser
    {
        [MaxLength(18)]
        public string IdCardNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public virtual  ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual  ICollection<IdentityUserLogin<string>> Logins{ get; set; }
        public virtual  ICollection<IdentityUserToken<string>> Tokens{ get; set; }
        public virtual  ICollection<IdentityUserRole<string>>UserRoles { get; set; }
    }
}
