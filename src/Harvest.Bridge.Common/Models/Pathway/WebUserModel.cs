using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models.Pathway
{
    [Serializable]
    public class WebUserModel
    {
        private bool _isAuthenticated;
        private int _userID;
        private string _userName;
        private bool _isAdministrator;
        private bool _limitToProvider;
        private string _providerIdList;
        private bool _passwordResetReq;
        private int _locationID = -1;
        private PrivilegesModel _privileges = new PrivilegesModel();
        public WebUserModel(DataRow dr)
        {
            _isAuthenticated = true;
            _userID = (int)dr["UserID"];
            _userName = dr["UserName"].ToString();
            _isAdministrator = false;
            if(dr["IsAdministrator"] is DBNull == false)
            {
                _isAdministrator = (bool)dr["IsAdministrator"];
            }
        }

        public bool IsAuthenticated => this._isAuthenticated;

        public int UserID => this._userID;

        public bool IsAdministrator => this._isAdministrator;
        public string UserName => this._userName;

        public bool LimitToProviderList => this._limitToProvider;

        public string ProviderIdList => this._providerIdList;

        public bool PasswordResetRequest => this._passwordResetReq;

        public PrivilegesModel Privileges => this._privileges;

        //public LocationDataCollection Locations => new LocationDataCollection(true)
        //{
        //    UserId = this.UserID
        //};

        public int CurrentLocationID
        {
            get => this._locationID;
            set => this._locationID = value;
        }

    }
}