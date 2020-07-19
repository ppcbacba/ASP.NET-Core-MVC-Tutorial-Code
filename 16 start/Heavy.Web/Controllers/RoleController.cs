using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize(Roles="Administrators")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleCreateViewModel role)
        {
            if (!ModelState.IsValid) return View(role);
            else
            {
                var _role = new IdentityRole {Name = role.RoleName};
                var result = await _roleManager.CreateAsync(_role);
                if (result.Succeeded)
                {
                    // return View("Index");
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(role);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) RedirectToAction("Index");
            var r = new RoleEditViewModel
            {
                Id = id,
                Name = role.Name,
                Users = new List<ApplicationUser>()
            };


            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    r.Users.Add(user);
                }
            }

            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel roleEditViewModel)
        {
            var role = await _roleManager.FindByIdAsync(roleEditViewModel.Id);
            if (role != null)
            {
                role.Name = roleEditViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");

                ModelState.AddModelError(string.Empty, "更新角色时出错");
                return View(roleEditViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");
                ModelState.AddModelError(string.Empty, "删除角色时出错");
            }
            ModelState.AddModelError(string.Empty, "没找到角色");
            return View("Index");
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return RedirectToAction("Index");

            var vm = new UserRoleViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel user)
        {
            var _user = await _userManager.FindByIdAsync(user.UserId);
            var _role = await _roleManager.FindByIdAsync(user.RoleId);
            if (_user != null && _role != null)
            {
                var result = await _userManager.AddToRoleAsync(_user, _role.Name);
                if (result.Succeeded) return RedirectToAction("EditRole",new{id=_role.Id});
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return View(user);
            }
            ModelState.AddModelError(string.Empty, "用户或角色未找到");
            return View(user);
        }

        public async Task<IActionResult> RemoveUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return RedirectToAction("Index");

            var vm = new UserRoleViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(UserRoleViewModel user)
        {
            var _user = await _userManager.FindByIdAsync(user.UserId);
            var _role = await _roleManager.FindByIdAsync(user.RoleId);
            if (_user != null && _role != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(_user, _role.Name);
                if (result.Succeeded) return RedirectToAction("EditRole", new { id = _role.Id });
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(user);
            }
            ModelState.AddModelError(string.Empty, "用户或角色未找到");
            return View(user);
        }
    }
}