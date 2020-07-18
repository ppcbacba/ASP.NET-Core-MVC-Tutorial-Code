using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel user)
        {
            if (!ModelState.IsValid) return View(user);
            var _user = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
            };
            var result = await _userManager.CreateAsync(_user, user.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", await _userManager.Users.ToListAsync());
            }
            else
            {
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "找不到用户");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) return Redirect("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "删除用户时发生错误");
                }
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }


       
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "找不到用户");
                return RedirectToAction("Index");
            }
            else
            {
                var user1 = new UserCreateViewModel {UserName = user.UserName, Email = user.Email};
               return View(user1);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id,UserCreateViewModel userEditViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.UserName = userEditViewModel.UserName;
            user.Email = userEditViewModel.Email;
     
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "更新用户信息时发生错误");
            return View(user);
        }

    }
}