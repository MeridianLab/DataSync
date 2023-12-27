using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Harvest.Bridge.Common.Models.Pathway;
using Microsoft.AspNet.Identity;

namespace Harvest.Bridge.WebSite
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfTokEN))-1";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserNAMe)))";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["Logoff"] != null)
            {
                Session.Abandon();
                Response.Redirect("~/");
            }
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenuBarDisplay();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private void SetMenuBarDisplay()
        {
            if(IsAuthenticated == false)
            {
                plhProjectMenuItems.Visible = false;
                plhAuthenticated.Visible = false;
                plhShowLogin.Visible = true;
            }
            else
            {
                plhProjectMenuItems.Visible = true;
                plhAuthenticated.Visible = true;
                plhShowLogin.Visible = false;
                lblUsername.Text = WebUser.UserName;
            }
        }

        private bool IsAuthenticated
        {
            get
            {
                return WebUser != null;
            }
        }

        private WebUserModel WebUser
        {
            get
            {
                if (Session["WebUser"] != null)
                {
                    return Session["WebUser"] as WebUserModel;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}