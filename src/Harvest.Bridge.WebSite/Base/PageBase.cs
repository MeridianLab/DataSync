using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Common.Models.Pathway;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Sync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Base
{
    public class PageBase : Page
    {
        private DataTable _dtConfiguration = null;
        private SolutionModel _solutionModel;
        protected void PageRequiredAuthentication()
        {
            if(IsUserAuthenticated == false)
            {
                Response.Redirect("~/");
            }
        }
        protected bool IsUserAuthenticated
        {
            get
            {
                return WebUser != null;
            }
        }
        protected WebUserModel WebUser
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
        protected string GetConfigValue(string name, string defaulValue = "")
        {
            if (_dtConfiguration == null)
            {
                DALConfiguration dalConfig = new DALConfiguration();
                _dtConfiguration = dalConfig.GetConfigurationData();
            }
            string retVal = defaulValue;
            DataRow[] drs = _dtConfiguration.Select("NAME='" + name + "'");
            if (drs.Length == 1)
            {
                retVal = drs[0]["Value"].ToString();
            }
            return retVal;
        }

        protected void UpdateConfigValue(string name, string value)
        {
            DALConfiguration dalConfig = new DALConfiguration();
            dalConfig.UpdateConfig(name, value);
            _dtConfiguration = null;
        }

        protected SolutionModel LoadSolution(Guid solutionId)
        {
            if (_solutionModel == null || _solutionModel.Id != solutionId)
            {
                _solutionModel = new JSONStoreWorker().OpenSolutionFromDB("StagingDB", solutionId);
            }
            return _solutionModel;
        }

        #region Show Messages
        internal static void ShowMessageDanger(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "danger", message);
        }
        internal static void ShowMessageSuccess(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "success", message);
        }
        internal static void ShowMessageInfo(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "info", message);
        }
        private static void ShowMessage(Literal ctrlMessage, string msgType, string message)
        {
            ctrlMessage.Text = $"<div class='alert alert-{msgType}' role='alert'>{message}</div>";
        }
        #endregion

    }
}