using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
