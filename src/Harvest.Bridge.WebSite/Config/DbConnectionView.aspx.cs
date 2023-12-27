using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.Config
{
    public partial class DbConnectionView : Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageRequiredAuthentication();

            PopulateDBConnectionView();
        }

        private void PopulateDBConnectionView()
        {
            StringBuilder sb = new StringBuilder();
            // foreach(var dbCon in ConfigurationManager.ConnectionStrings)
            for(int i=0;i<ConfigurationManager.ConnectionStrings.Count;i++)
            {
                string conStr = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                // Database=ReportPortal_QA;Server=MLAB-DB01n;Integrated Security=false;Password=odfSijr32$;User ID=pathwayqa;
                string[] parts = conStr.Split(';');
                if (parts.Length > 4)
                {
                    string databaseName = string.Empty;
                    string server = string.Empty;
                    string username = string.Empty;
                    foreach(string s in parts)
                    {
                        if(s.StartsWith("database", StringComparison.OrdinalIgnoreCase))
                        {
                            databaseName = s.Split('=')[1];
                        }
                        else if(s.StartsWith("user id", StringComparison.OrdinalIgnoreCase))
                        {
                            username = s.Split('=')[1];
                        }
                        else if(s.StartsWith("server", StringComparison.OrdinalIgnoreCase))
                        {
                            server = s.Split('=')[1];
                        }
                    }
                    sb.AppendLine($"<tr><td>{ConfigurationManager.ConnectionStrings[i].Name}</td><td>{server}</td><td>{databaseName}</td><td>{username}</td></tr>");                    
                }
            }

            ltrlDBViewTable.Text = sb.ToString();
        }
    }
}