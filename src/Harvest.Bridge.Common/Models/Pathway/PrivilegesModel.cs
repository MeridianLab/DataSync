using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models.Pathway
{
    [Serializable]
    public class PrivilegesModel
    {
        public Hashtable HashPrivileges;

        internal PrivilegesModel()
        {
        }

        public bool HasPrivilege(string name) => this.HashPrivileges.Contains((object)name);

        public bool IsAdministrator => this.HasPrivilege("Admin");

        public bool IsSuperUser => this.HasPrivilege("SuperUser");
    }
}
