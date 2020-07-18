using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        
        [Required]
        [DataType(dataType:DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }
        [Required]
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; }

    }
}
