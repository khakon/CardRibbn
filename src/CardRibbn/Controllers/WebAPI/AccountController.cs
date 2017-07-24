using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Models.AccountViewModels;
using CardRibbn.Core;
using CardRibbn.Models;
using Microsoft.AspNetCore.Identity;
using CardRibbn.Services;
using Microsoft.Extensions.Logging;
using CardRibbn.Filters;

namespace CardRibbn.Controllers.WebAPI
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class accountController : BaseAPIController
    {
        public accountController(
           UserManager<CardCraftUser> userManager,
           SignInManager<CardCraftUser> signInManager,
           IEmailSender emailSender,
           ISmsSender smsSender,
           ILoggerFactory loggerFactory)
            : base(userManager, signInManager, emailSender, smsSender, loggerFactory)
        {

        }

        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string apiStatus = "successful_login";
            string apiMessage = "Successful logged into CardCraft";
            CardCraftUser data = null;

            //Add service code to login in and authenticate against database

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await SignManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                data = await UserManager.FindByEmailAsync(model.Email);
                Logger.LogInformation(1, APILog(endPoint: "Login", statusCode: "200", apiStatus: apiStatus, apiMessage: apiMessage, data: data));
                return SuccessfulAPIResult(apiStatus, apiMessage, data);
            }
            if (result.RequiresTwoFactor)
            {
                apiStatus = "user_requires_2FA";
                apiMessage = "Please complete sign up process by checking link in email: " + model.Email;
                return SuccessfulAPIResult(apiStatus, apiMessage);
                //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                apiStatus = "user_account_locked";
                apiMessage = "Your user account is locked please check " + model.Email + "for details.";
                Logger.LogWarning(2, "User account locked out.");
                return SuccessfulAPIResult(apiStatus, apiMessage);
            }
            else
            {
                apiStatus = "invalid_login_attempt";
                apiMessage = "Error logging you in. Please check email address or password.";
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return ErrorAPIResult(apiStatus, apiMessage);
            }

            //apiStatus = "invalid_login_attempt";
            //apiMessage = "There was an issue logging you in. Please try again later or email help@cardcraft.io";
            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //return ErrorAPIResult(apiStatus, apiMessage, model);

        }

        [HttpPost]
        [Route("signup")]
        [ValidateModel]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            string apiStatus = "successful_signup";
            string apiMessage = "Successful sign up with CardCraft";
            CardCraftUser data = null;

            var user = new CardCraftUser { UserName = model.Email, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                data = await UserManager.FindByEmailAsync(model.Email);
                Logger.LogInformation(3, "User created a new account with password.");
                return SuccessfulAPIResult(apiStatus, apiMessage, data);
            }
            else
            {
                return IdentityResultLogicError(result);
            }
            //}
            //else
            //{
            //    apiStatus = "invalid_signup_attempt";
            //    apiMessage = "There was an issue signing you up. Please try again later or email help@cardcraft.io";
            //    ModelState.AddModelError(string.Empty, "Invalid signup attempt.");
            //    return ErrorAPIResult(apiStatus, apiMessage, model);
            //}
        }
    }
}