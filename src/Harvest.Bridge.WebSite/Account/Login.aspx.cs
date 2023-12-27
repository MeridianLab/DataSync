using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Harvest.Bridge.WebSite.Models;
using Harvest.Bridge.Common.Models.Pathway;
using Harvest.Bridge.Bus.Pathway;

namespace Harvest.Bridge.WebSite.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                //ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //ApplicationSignInManager signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //// This doen't count login failures towards account lockout
                //// To enable password failures to trigger lockout, change to shouldLockout: true
                //SignInStatus result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);
                WebUserModel webUser = new WebUser().Authenticate(Username.Text, Password.Text);
                if (webUser != null && webUser.IsAdministrator)
                {
                    Session["WebUser"] = webUser;
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}