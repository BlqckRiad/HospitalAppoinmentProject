using HospitalApp.Models;
using HospitalApp.WiewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        private SignInManager<AppUser> _signManager;
        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signManager = signManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signManager.SignOutAsync();
#pragma warning disable CS8604 // Possible null reference argument.
                    var result = await _signManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
#pragma warning restore CS8604 // Possible null reference argument.

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutdate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeleft = lockoutdate.Value - DateTime.UtcNow;

                        ModelState.AddModelError("", $"Hesabınız kitlendi , Lütfen {timeleft.Minutes} dakika sonra tekrar deneyiniz ..");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Parolanız Hatalıdır");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı E-Mail Adresi Girilmiştir");
                }
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateWiewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName };
#pragma warning disable CS8604 // Possible null reference argument.

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
#pragma warning restore CS8604 // Possible null reference argument.
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Login");
        }






        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                // Email boşsa aynı sayfaya geri dön
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                // Kullanıcı bulunamadıysa aynı sayfaya geri dön
                return View();
            }

            // Şifreyi sıfırlama token'ı oluştur
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Token'ı kullanarak şifre sıfırlama sayfasına yönlendir
            return RedirectToAction("ResetPassword", new { email = Email, token = resetToken });
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _userManager.FindByEmailAsync(model.Email);
#pragma warning restore CS8604 // Possible null reference argument.

            if (user == null)
            {
                // Kullanıcı bulunamadıysa aynı sayfaya geri dön
                return View();
            }

            // Şifreyi sıfırla
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
#pragma warning restore CS8604 // Possible null reference argument.

            if (result.Succeeded)
            {
                // Şifre sıfırlama başarılıysa kullanıcıyı otomatik olarak giriş yap
                await _signManager.SignInAsync(user, isPersistent: false);

                // Şifre sıfırlama başarılıysa, istediğiniz bir sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Şifre sıfırlama başarısızsa hata mesajlarıyla aynı sayfaya geri dön
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

    }
}