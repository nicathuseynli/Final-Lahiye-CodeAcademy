using Final_Lahiye.Data;
using Final_Lahiye.Membership;
using Final_Lahiye.ViewModels;
using Final_Layihe.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Final_Lahiye.Controllers;
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<MUser> _signInManager;
    private readonly UserManager<MUser> _userManager;

    public AccountController(AppDbContext context, ILogger<AccountController> logger, SignInManager<MUser> signInManager, UserManager<MUser> userManager)
    {
        _context = context;
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [Route("/login.html")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/login.html")]
    public async Task<IActionResult> Login(LoginFormModel user)
    {
        if (ModelState.IsValid)
        {
            MUser foundedUser = null;
            if (user.Email.IsEmail())
            {
                foundedUser = await _userManager.FindByEmailAsync(user.Email);
            }
            else
            {
                foundedUser = await _userManager.FindByNameAsync(user.Email);
            }
            if (foundedUser == null)
            {
                ViewBag.Message = "Your Email or Password wrong !";
                goto end;
            }
            var signinResult = await _signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);
            if (!signinResult.Succeeded)
            {
                ViewBag.Message = " Your Email or Password wrong !";
                goto end;
            }

            var callBackUrl = Request.Query["ReturnURl"];
            if (!string.IsNullOrWhiteSpace(callBackUrl))
            {
                return Redirect(callBackUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    end:
        return View(user);
    }

    [AllowAnonymous]
    [Route("/register.html")]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("/register.html")]

    public async Task<IActionResult> Register(RegisterFormModel register)
    {
        if (ModelState.IsValid)
        {
            var user = new MUser();
            user.Email = register.Email;
            user.UserName = register.Email;
            user.Name = register.Name;
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, register.Password);


            if (result.Succeeded)
            {
                ViewBag.Message = "Congratulation Successfully Registered !";
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

        }
        return View(register);
    }

    [Route("/logout.html")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index" ,"Home");
    }

    public IActionResult Account()
    {
        return View();
    }
}
