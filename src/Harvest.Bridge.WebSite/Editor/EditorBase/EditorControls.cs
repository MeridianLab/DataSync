using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using System;
using System.Linq;

namespace Harvest.Bridge.WebSite.Editor.EditorBase
{
    public class EditorControls : System.Web.UI.UserControl
    {
        private SolutionModel _solutionModel;

        protected Guid SolutionId
        {
            get
            {
                if (Session["SolutionId"] == null)
                {
                    return Guid.Empty;
                }
                else
                {
                    return (Guid)Session["SolutionId"];
                }
            }
            set
            {
                Session["SolutionId"] = value;
            }
        }

        protected bool HasSelectedSolution
        {
            get
            {
                return Session["SolutionId"] != null;
            }
        }
        protected Guid CrntProjectId
        {
            get
            {
                if (Session["ProjectId"] == null)
                {
                    return Guid.Empty;
                }
                else
                {
                    return (Guid)Session["ProjectId"];
                }
            }
            set
            {
                Session["ProjectId"] = value;
            }
        }

        protected Guid CrntStepId
        {
            get
            {
                if (Session["ProjectStepId"] == null)
                {
                    return Guid.Empty;
                }
                else
                {
                    return (Guid)Session["ProjectStepId"];
                }
            }
            set
            {
                Session["ProjectStepId"] = value;
            }
        }

        protected bool ViewingGlobalVariables()
        {
            return CrntProjectId != null && CrntProjectId.ToString() == "11111111-1111-1111-1111-111111111111";
        }

        protected ProjectModel GetCurrentProjectModel()
        {
            return GetSolutionModel().Projects.FirstOrDefault(p => p.Id == CrntProjectId);
        }
        protected ProjectStepModel GetCurrentProjectStepModel()
        {
            return GetCurrentProjectModel().ProjectSteps.FirstOrDefault(s => s.Id == CrntStepId);
        }
        protected SolutionModel GetSolutionModel()
        {
            if (HasSelectedSolution)
            {
                if (_solutionModel == null)
                {
                    _solutionModel = new JSONStoreWorker().OpenSolutionFromDB("StagingDB", SolutionId);
                }
                return _solutionModel;
            }
            else
            {
                return null;
            }
        }

        protected bool IsInEditMode
        {
            get
            {
                bool retVal = false;
                if(Session["SolutionIsInEditMode"] != null)
                {
                    retVal = (bool)Session["SolutionIsInEditMode"];
                }
                return retVal;
            }
        }
    }
}