using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Domain.Entities;
using Resort.Web.ViewModels;

namespace Resort.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._roleManager = roleManager;
    }

    public IActionResult Login(string? ReturnUrl)
    {
        ReturnUrl ??= Url.Content("~/");
        LoginVM loginVm = new LoginVM
        {
            RedirectUrl = ReturnUrl
        };
        return View(loginVm);
    }

    public IActionResult Register(string? returnUrl)
    {
        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
        }
        //ViewBag. rolesList = new SelectList(_roleManager.Roles, "Name", "Name");
        RegisterVM registerVM = new RegisterVM
        {
            RoleList = _roleManager.Roles.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Name
            }),
            RedirectUrl = returnUrl
        };
        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {

        ApplicationUser user = new()
        {
            Name = registerVM.Name,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
            NormalizedEmail = registerVM.Email.ToUpper(),
            UserName = registerVM.Email,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            CreatedAt = DateTime.Now,
        };
        var result = await _userManager.CreateAsync(user, registerVM.Password);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(registerVM.Role))
            {
                await _userManager.AddToRoleAsync(user, registerVM.Role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            if (!string.IsNullOrEmpty(registerVM.RedirectUrl))
            {
                return LocalRedirect(registerVM.RedirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        registerVM.RoleList = _roleManager.Roles.Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Name
        });
        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.
                PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (!string.IsNullOrEmpty(loginVM.RedirectUrl))
                    {
                        return LocalRedirect(loginVM.RedirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Login Failed. Check your credentials and try again.");
        }
        return View(loginVM);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
