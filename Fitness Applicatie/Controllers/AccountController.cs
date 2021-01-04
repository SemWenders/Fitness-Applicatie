using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fitness_Applicatie.Models;
using FitTracker.Logic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Fitness_Applicatie.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel accountViewModel)
        {
            if (!ModelState.IsValid) return View(accountViewModel);

            UserCollection userCollection = new UserCollection();
            User user = userCollection.GetUser(accountViewModel.UserName);

            //check if user exists
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(accountViewModel);
            }

            //check password
            var hasher = new PasswordHasher<User>();
            /*
            if (hasher.VerifyHashedPassword(user, user.Password, accountViewModel.Password) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Username of password is incorrect");
                return View(accountViewModel);
            }
            */

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Id", user.UserID)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
            return RedirectToAction("Index", "Home");
        }
    }
}
