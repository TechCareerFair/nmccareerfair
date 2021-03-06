﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.LinkedIn;
using Owin.Security.Providers.Yahoo;
using TechCareerFair.Models;
using static TechCareerFair.Controllers.AccountController;

namespace TechCareerFair
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                ExpireTimeSpan = TimeSpan.FromMinutes(5.0),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

              //app.UseTwitterAuthentication(new TwitterAuthenticationExtensions()
              //{ consumerKey: "MmB1lal6cGkHUzcXo0fqhhOwD",
              //consumerSecret: "	sJPh7us5fAR7QsjZgz3h6gUC107N4WL7LYBrCugCrDbHffekIX",
              // BackchannelCertificateValidator = new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(new[]
              //    {

              //      "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
              //      "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
              //      "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary CA - G5
              //      "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
              //      "‎add53f6680fe66e383cbac3e60922e3b4c412bed", // Symantec Class 3 EV SSL CA - G3
              //      "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
              //      "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
              //      "B13EC36903F8BF4701D498261A0802EF63642BC3"  // DigiCert High Assurance EV Root CA
              //    })
              //});

            //var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            //{
            //    AppId = "364846563994378",
            //    AppSecret = "f26a0cabce9d9272734be06847747553",
            //    UserInformationEndpoint = "https://graph.facebook.com/v2.8/me?fields=id,name,email,first_name,last_name",
            //    BackchannelHttpHandler = new FacebookBackChannelHandler(),
            //    CallbackPath = new PathString("/oauth-redirect/facebook")
            //};
            //facebookOptions.Scope.Add("email");
            //app.UseFacebookAuthentication(facebookOptions);

            //var gProvider = new GoogleOAuth2AuthenticationProvider { OnAuthenticated = context => Task.FromResult(0) };
            //var gOptions = new GoogleOAuth2AuthenticationOptions { Provider = gProvider, SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie };
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "514477754818-a761lbmu5c25jvmp7baajmi3die5a41o.apps.googleusercontent.com",
                ClientSecret = "t5c7KbIul097s0JQH2PGZLOm",
                CallbackPath = new PathString("/signin-google")
            });
            var linkedInOptions = new LinkedInAuthenticationOptions();

            linkedInOptions.ClientId = "86lcyi8hwtnxkg";
            linkedInOptions.ClientSecret = "1Hq8DRQNEhF56imD";

            linkedInOptions.Scope.Add("r_emailaddress");

            linkedInOptions.Provider = new LinkedInAuthenticationProvider()
            {
                OnAuthenticated = async context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("LinkedIn_AccessToken", context.AccessToken));
                }
            };

            linkedInOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;

            app.UseLinkedInAuthentication(linkedInOptions);

        }
    }
}