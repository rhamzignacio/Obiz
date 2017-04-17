using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using Obiz.Services;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Obiz
{
    public class UniversalHelper
    {
        public static MenuModel ObizMenu
        {
            get
            {
                MenuModel menu = new MenuModel();

                using (var db = new ObizEntities())
                {
                    var activityReport = db.UserPrivilege.FirstOrDefault(r => r.Module == "SalesReport" && r.View == "Y" && r.UserID == UniversalHelper.CurrentUser.ID);

                    var activityReportChart = db.UserPrivilege.FirstOrDefault(r => r.Module.Contains("SalesReport/") && r.View == "Y" && r.UserID == UniversalHelper.CurrentUser.ID);

                    var report = db.UserPrivilege.FirstOrDefault(r => r.Module == "Report" && r.View == "Y" && r.UserID == UniversalHelper.CurrentUser.ID);

                    var aefur = db.UserPrivilege.FirstOrDefault(r => r.Module == "AEFUR" && r.View == "Y" && r.UserID == UniversalHelper.CurrentUser.ID);

                    menu.ActivityReport = activityReport != null ? "Y" : "N";

                    menu.ActivityReportCharts = activityReportChart != null ? "Y" : "N";

                    menu.Report = report != null ? "Y" : "N";

                    menu.AEFUR = aefur != null ? "Y" : "N";
                }

                return menu;
            }
            set
            {

            }
        }

        public static UserAccountModel CurrentUser
        {
            get
            {
                UserAccountModel user = null;

                HttpCookie authCookie_obiz = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie_obiz != null)
                {
                    FormsAuthenticationTicket authTicket_Obiz = FormsAuthentication.Decrypt(authCookie_obiz.Value);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    PrincipalSerializeModel serializeModel = serializer.Deserialize<PrincipalSerializeModel>(authTicket_Obiz.UserData);

                    Principal newUser = new Principal(authTicket_Obiz.Name);

                    newUser.Username = serializeModel.Username;

                    newUser.SessionID = serializeModel.SessionID;

                    newUser.Status = serializeModel.Status;

                    HttpContext.Current.User = newUser;

                    using (var db = new ObizEntities())
                    {
                        var query = from u in db.UserAccount
                                    where u.Username == newUser.Username
                                    select new UserAccountModel
                                    {
                                        Username = u.Username,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        MiddleInitial = u.MiddleInitial,
                                        Status = u.Status,
                                        ID = u.ID,
                                        Department = u.Department
                                    };

                        user = query.FirstOrDefault();
                    }
                }

                return user;
            }
            set
            {

            }
        }
    }
}