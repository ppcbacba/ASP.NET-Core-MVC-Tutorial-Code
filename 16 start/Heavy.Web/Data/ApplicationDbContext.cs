using System;
using System.Collections.Generic;
using System.Text;
using Heavy.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Heavy.Web.ViewModels;

namespace Heavy.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Heavy.Web.ViewModels.RoleCreateViewModel> RoleCreateViewModel { get; set; }
        public DbSet<Heavy.Web.ViewModels.RoleEditViewModel> RoleEditViewModel { get; set; }
    }
}
