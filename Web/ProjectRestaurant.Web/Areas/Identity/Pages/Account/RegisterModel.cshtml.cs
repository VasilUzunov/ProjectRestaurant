﻿namespace ProjectRestaurant.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using ProjectRestaurant.Data.Models;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this._roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
            [EmailAddress (ErrorMessage = "Please enter valid email address")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your first name")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your last name")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your address")]
            [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your phone")]
            [Phone(ErrorMessage = "Please enter valid phone number")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    Address = this.Input.Address,
                    PhoneNumber = this.Input.PhoneNumber,
                };
                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    if (!await this._roleManager.RoleExistsAsync("Administrator"))
                    {
                        await this._roleManager.CreateAsync(new ApplicationRole("Administrator"));
                    }

                    if (!await this._roleManager.RoleExistsAsync("User"))
                    {
                        await this._roleManager.CreateAsync(new ApplicationRole("User"));
                    }

                    await this._userManager.AddToRoleAsync(user, "User");
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
