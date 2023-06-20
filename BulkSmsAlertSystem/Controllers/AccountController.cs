using BulkSmsAlertSystem.Data;
using BulkSmsAlertSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulkSmsAlertSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context; // This is the database context

        }

        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Search the database for a user with the given email using the `_userManager` object
            var user = await _userManager.FindByEmailAsync(email);

            // Verify that a user was found with the given email
            if (user != null)
            {
                // Use `_context` to retrieve user credentials and attempt to sign in the user with the given password
                var result = await _signInManager.PasswordSignInAsync(user.UserName = email.Split('@')[0], password, false, false);
                TempData["SignOff"] = "Sent by Koby";
                TempData.Keep("SignOff");

                // Redirect to SMS creation page if successful
                if (result.Succeeded)
                {
                    return RedirectToAction("Create", "Sms");
                }
                else
                {
                    // If the login fails, display error message.
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }
            else
            {
                // If the user is not found, display error message.
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user and redirect to the home page
            await HttpContext.SignOutAsync();
            return View(); /*RedirectToAction("Index", "Home");*/
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string password)
        {
            var username = email.Split('@')[0]; // username is the part of the email address before the @ symbol
            var user = new User { UserName = username, Email = email, Password = password};
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

    }
}
