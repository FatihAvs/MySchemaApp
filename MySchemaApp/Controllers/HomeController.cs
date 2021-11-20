
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySchemaApp.Core;
using MySchemaApp.Models;
using MySchemaApp.Models.Identity;
using MySchemaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.Controllers
{
    public class HomeController : Controller
    {
        readonly UserManager<AppUser> userManager;
        readonly SignInManager<AppUser> signInManager;
        SchemaModel _schemaModel;
       
        List<string> kek;
        List<string> OrganizyonSemasi;
        List<string> F89ProsesEtkilesimTablosu;
        List<string> KaliteTablosu;
        List<string> KaliteRiskAnalizTablosu;
        List<string> F97KaliteHedefleriİzlemeTablosu;

        List<string> GorevTanimlari;
        List<string> F50YillikKalibrasyonPlani;
        List<string> YillikEgitimPlani;
        List<string> F52DogrulamaPlani;
        List<string> YeniForm;
        List<string> RevizeForm;
        List<string> IptalForm;
        List<string> DısKaynakliDokumanListesi;
        List<string> F47TedarikciDegerlendirmeFormu;
        MySchemaCheckedModel _mySchemaCheckedModel;
        List<string> myListt;






        private readonly ILogger<HomeController> _logger;




        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "8 kere başarısız giriş denemesinde bulundunuz tekrar giriş " +
                            "yapabilmek için lütfen bekleyiniz.");
                        return View(loginViewModel);
                    }
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user,
                        loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await userManager.ResetAccessFailedCountAsync(user);
                        if (TempData["returnUrl"] != null)
                        {
                            return Redirect(TempData["returnUrl"].ToString());
                        }
                        return RedirectToAction("GetSchemas", "Member");
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(user);
                        int basarısızGirisSayısı = await userManager.GetAccessFailedCountAsync(user);
                        if (basarısızGirisSayısı > 7)
                        {
                            await userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(25)));
                        }

                        ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    };

                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                }
            }


            return View(loginViewModel);
        }





        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            return View(userViewModel);
        }








        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hakkında()
        {
            return View();
        }
        
     
        
      

     
    }
}
