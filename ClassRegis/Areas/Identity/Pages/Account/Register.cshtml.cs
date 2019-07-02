using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Models;
using ClassRegis.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static ClassRegis.Models.ApplicationUser;

namespace ClassRegis.Areas.Identity.Pages.Account
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Role")]
            public string RoleUser { get; set; }



        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email
                                                ,
                    Name = Input.Name,
                    RoleUser = Input.RoleUser
                                                ,
                    PhoneNumber = Input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.AdminTeacher))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminTeacher));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.EndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.EndUser));
                    }

                    if (Input.RoleUser.Equals(SD.SuperAdminEndUser))
                    {
                        await _userManager.AddToRoleAsync(user, SD.SuperAdminEndUser);
                    }
                    else if (Input.RoleUser.Equals(SD.AdminTeacher))
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminTeacher);
                        Teachers teachers = new Teachers()
                        {
                            Name = user.Name,
                            Email = user.Email
                        };
                        _db.Add(teachers);
                        await _db.SaveChangesAsync();


                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.EndUser);
                        var students = new ClassRegis.Models.Students()
                        {
                            Name = user.Name,
                            Email = user.Email
                        };
                        _db.Add(students);
                        await _db.SaveChangesAsync();
                    }







                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (User.IsInRole(SD.SuperAdminEndUser) || User.IsInRole(SD.AdminTeacher))
                        return RedirectToAction("Index", "AdminUsers", new { area = "Admin" });

                    return RedirectToAction("Index", "Home", new { area = "Student" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
