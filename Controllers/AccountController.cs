using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yummy.Models.Auth;
using Yummy.ViewModels.Auth;

namespace Yummy.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<MyAppUser> _userManager;
        private readonly SignInManager<MyAppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<MyAppUser> userManager, SignInManager<MyAppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            MyAppUser newUser = new()
            {
                UserName = registerVM.UserName,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email
            };

            IdentityResult registerResult = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (!registerResult.Succeeded)
            {
                foreach (IdentityError error in registerResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);

            }
            IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());

            if (!roleResult.Succeeded)
            {
                foreach (IdentityError error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            return Json("Ok");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(loginVM);

            MyAppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);


            if (appUser == null)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(loginVM);
            }


            Microsoft.AspNetCore.Identity.SignInResult signInResult =
                await _signInManager.CheckPasswordSignInAsync(appUser, loginVM.Password, false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(loginVM);
            }

            await _signInManager.SignInAsync(appUser, false);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddRoles()
        {
            foreach (object role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }

            return Json("Ok");
        }

        public enum UserRoles
        {
            Admin,
            User,
            Moderator
        }
    }
}
