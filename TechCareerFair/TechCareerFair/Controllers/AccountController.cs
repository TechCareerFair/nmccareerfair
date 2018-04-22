using System;
using System.Collections.Generic;
using Facebook;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TechCareerFair.CustomAttributes;
using TechCareerFair.DAL.FieldDAL;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    //https://stackoverflow.com/questions/40540678/externalloginconfirmation-returns-null-after-facebook-succesful-login?rq=1
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            bool aService = new DAL.ApplicantDatabaseDataService().ReadIsActive(model.Email);
            bool bService = new DAL.BusinessDatabaseDataService().ReadIsActive(model.Email);
            bool adService = new DAL.AdminDAL.AdminDatabaseDataService().ReadIsActive(model.Email);
            string activeStatus = "";
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (result == SignInStatus.Success && !aService && !bService && !adService)
            {
                result = SignInStatus.Failure;
                activeStatus = "Your account is currently inactive.";
            }
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt." + activeStatus);
                        return View(model);
                }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            Session["Workaround"] = 0;
            ControllerContext.HttpContext.Session.RemoveAll();
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(LoginViewModel model,string returnUrl)
        {
            bool aService = new DAL.ApplicantDatabaseDataService().ReadIsActive(model.Email);
            bool bService = new DAL.BusinessDatabaseDataService().ReadIsActive(model.Email);
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("RegisterIndex", "Account");
            }
            //Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
             if (result == SignInStatus.Success && aService && bService)
            {
                return RedirectToLocal(returnUrl);
            }
            switch (result)
            {
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    //If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return RedirectToAction("RegisterIndex", "Account");
                    //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ExternalLoginCallbackRedirect(string returnUrl)
        {
            return RedirectPermanent("/Account/ExternalLoginCallback");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        public class FacebookBackChannelHandler : HttpClientHandler
        {
            protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                System.Threading.CancellationToken cancellationToken)
            {
                // Replace the RequestUri so it's not malformed
                if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
                {
                    request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region USER ROLE MANAGEMENT
        //author: John Velis
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult ViewUsersRoles(string accountType, string userName = null)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                List<string> userRoles;

                using (var context = new ApplicationDbContext())
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    var user = userManager.FindByEmail(userName);
                    if (user == null)
                        throw new Exception("User not found!");

                    var userRoleIds = (from r in user.Roles select r.RoleId);
                    userRoles = (from id in userRoleIds
                                 let r = roleManager.FindById(id)
                                 select r.Name).ToList();
                }

                ViewBag.UserName = userName;
                ViewBag.AccountType = accountType;
                ViewBag.RolesForUser = userRoles;
            }
            return View();
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult AddRoleToUser(string accountType, string userName = null)
        {
            List<string> roles;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roles = (from r in roleManager.Roles select r.Name).ToList();
            }

            ViewBag.Roles = new SelectList(roles);
            ViewBag.AccountType = accountType;
            ViewBag.UserName = userName;
            return View();
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string roleName, string userName, string accountType)
        {
            List<string> roles;
            ViewBag.AccountType = accountType;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindByName(userName);
                if (user == null)
                    throw new Exception("User not found!");

                var role = roleManager.FindByName(roleName);
                if (role == null)
                    throw new Exception("Role not found!");

                if (userManager.IsInRole(user.Id, role.Name))
                {
                    ViewBag.ErrorMessage = "This user already has the role specified !";

                    roles = (from r in roleManager.Roles select r.Name).ToList();
                    ViewBag.Roles = new SelectList(roles);

                    ViewBag.UserName = userName;

                    return View();
                }
                else
                {
                    userManager.AddToRole(user.Id, role.Name);
                    context.SaveChanges();

                    List<string> userRoles;
                    var userRoleIds = (from r in user.Roles select r.RoleId);
                    userRoles = (from id in userRoleIds
                                 let r = roleManager.FindById(id)
                                 select r.Name).ToList();

                    ViewBag.UserName = userName;
                    ViewBag.RolesForUser = userRoles;

                    return View("ViewUsersRoles");
                }
            }
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult DeleteRoleForUser(string accountType, string userName = null, string roleName = null)
        {
            ViewBag.AccountType = accountType;
            if ((!string.IsNullOrWhiteSpace(userName)) || (!string.IsNullOrWhiteSpace(roleName)))
            {
                List<string> userRoles;

                using (var context = new ApplicationDbContext())
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    var user = userManager.FindByName(userName);
                    if (user == null)
                        throw new Exception("User not found!");

                    if (userManager.IsInRole(user.Id, roleName))
                    {
                        userManager.RemoveFromRole(user.Id, roleName);
                        context.SaveChanges();
                    }

                    var userRoleIds = (from r in user.Roles select r.RoleId);
                    userRoles = (from id in userRoleIds
                                 let r = roleManager.FindById(id)
                                 select r.Name).ToList();
                }

                ViewBag.RolesForUser = userRoles;
                ViewBag.UserName = userName;

                return View("ViewUsersRoles");
            }
            else
            {
                return View("Index");
            }

        }

        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region Register

        //
        // GET: Account/Register
        [AllowAnonymous]
        public ActionResult RegisterIndex()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        // GET: Account/Create
        public ActionResult RegisterApplicant()
        {
            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;

            return View();
        }

        //
        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterApplicant(ApplicantViewModel model, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;
            ViewBag.ErrCheckFields = model.Fields;
            ViewBag.States = DAL.DataSettings.US_STATES;

            if (IsValidCaptcha())
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            UserManager.AddToRole(user.Id, "Applicant");
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                            // Send an email with this link
                            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                            if (fileUpload != null)
                            {
                                model.Resume = DAL.DatabaseHelper.UploadFile(DAL.DataSettings.RESUME_DIRECTORY, fileUpload, Server);
                            }
                            DAL.ApplicantRepository ar = new DAL.ApplicantRepository();
                            List<applicant> applicants = ar.SelectAll().ToList();

                            model.Active = true;
                            ar.Insert(ar.ToModel(model));

                            LoginViewModel loginModel = new LoginViewModel();
                            loginModel.Email = model.Email;

                            return RedirectToAction("LoginFromRegistration", "Account", loginModel);
                        }
                        catch (ArgumentException e)
                        {
                            ViewBag.Error = e.Message;
                            return View();
                        }
                    }
                    AddErrors(result);
                }
            }
            // If we got this far, something failed, redisplay form
            return View();
        }

        // GET: Account/Business
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterBusiness()
        {
            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;
            ViewBag.States = DAL.DataSettings.US_STATES;

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterBusiness(BusinessViewModel model, HttpPostedFileBase fileUpload, FormCollection collection)
        {

            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;
            ViewBag.ErrCheckFields = model.Fields;
            ViewBag.States = DAL.DataSettings.US_STATES;

            if (IsValidCaptcha())
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            UserManager.AddToRole(user.Id, "Business");
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                            // Send an email with this link
                            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                            if (fileUpload != null)
                            {
                                model.Photo = DAL.DatabaseHelper.UploadFile(DAL.DataSettings.BUSINESS_DIRECTORY, fileUpload, Server);
                            }
                            DAL.BusinessRepository br = new DAL.BusinessRepository();
                            List<business> businesses = br.SelectAll().ToList();

                            model.Approved = false;
                            model.Active = true;
                            br.Insert(br.ToModel(model));

                            LoginViewModel loginModel = new LoginViewModel();
                            loginModel.Email = model.Email;

                            return RedirectToAction("LoginFromRegistration", "Account", loginModel);
                        }
                        catch (ArgumentException e)
                        {
                            ViewBag.Error = e.Message;
                            return View();
                        }
                    }
                    AddErrors(result);
                }
            }
            // If we got this far, something failed, redisplay form
            return View();
        }
        
        private class CaptchaResult
        {
            public string success { get; set; }
        }

        public bool IsValidCaptcha()
        {
            //More on how this works at: https://theprogressiveviews.blogspot.com/2016/08/google-recaptcha-with-server-side.html 

            string responce = Request["g-recaptcha-response"];
            string secretKey = "6LcumlMUAAAAAOMAUWGdHMX972thSUuSkQe61tc0";
            var request = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=+" + secretKey + "&response=" + responce);

            using (WebResponse wResponse = request.GetResponse())
            {
                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();
                    JavaScriptSerializer js = new JavaScriptSerializer();

                    CaptchaResult data = js.Deserialize<CaptchaResult>(jsonResponse);

                    if (Convert.ToBoolean(data.success))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public ActionResult PostSignUp()
        {
            ViewBag.FullName = TempData["FullName"].ToString();
            return View("PostSignUp");
        }

        [AllowAnonymous]
        public ActionResult LoginFromRegistration(LoginViewModel model)
        {
            ViewBag.ReturnUrl = "Home/Index";
            return View(model);
        }
        #endregion
    }
}