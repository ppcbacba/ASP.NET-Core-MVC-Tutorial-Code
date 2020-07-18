using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Models;

namespace Heavy.Web.ViewModels
{
    public class RoleEditViewModel
    {
        public string Id { get; set; }
        [Required,Display(Name = "角色名称")]
        public string Name { get; set; }
        public List<ApplicationUser>Users { get; set; }
    }
}
