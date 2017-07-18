using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Data.Entity;

namespace Obiz.Services
{
    public class UserService
    {
        public static void SaveUser(UserAccountModel _user, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    if(_user.ID == Guid.Empty || _user.ID == null) //NEW USER
                    {

                        var username = db.UserAccount.FirstOrDefault(r => r.Username == _user.Username);

                        if (username != null)
                        {
                            message = "Username already exist";
                        }
                        else
                        {
                            UserAccount newUser = new UserAccount
                            {
                                ID = Guid.NewGuid(),
                                AgentCode1 = _user.AgentCode1,
                                AgentCode2 = _user.AgentCode2,
                                AgentCode3 = _user.AgentCode3,
                                CebBizPartner = _user.CebBizPartner,
                                DepartmentHead = _user.DepartmentHead,
                                Department = _user.Department,
                                Email = _user.Email,
                                FirstName = _user.FirstName,
                                MiddleInitial = _user.MiddleInitial,
                                LastName = _user.LastName,
                                PartnerAgent = _user.PartnerAgent,
                                Password = "philscan",
                                Status = "Y",
                                Username = _user.Username,
                                ClientMagicLogin = _user.ClientMagicLogin
                            };

                            db.Entry(newUser).State = EntityState.Added;

                            //Priviledge

                            //=====SALES REPORT=========
                            if(_user.AMActivityReport != null)
                            {
                                UserPrivilege AMActivityReportPriv = new UserPrivilege
                                {
                                    ID = Guid.NewGuid(),
                                    UserID = newUser.ID,
                                    Module = "SalesReport",
                                    Add = _user.AMActivityReport.Add,
                                    Edit = _user.AMActivityReport.Edit,
                                    Delete = _user.AMActivityReport.Delete,
                                    Override = _user.AMActivityReport.Override,
                                    View = _user.AMActivityReport.View
                                };

                                db.Entry(AMActivityReportPriv).State = EntityState.Added;
                            }
                            
                            //======AEFUR==========
                            if(_user.AEFUR != null)
                            {
                                UserPrivilege AEFURPriv = new UserPrivilege
                                {
                                    ID = Guid.NewGuid(),
                                    UserID = newUser.ID,
                                    Module = "AEFUR",
                                    Add = _user.AEFUR.Add,
                                    Edit = _user.AEFUR.Edit,
                                    Delete = _user.AEFUR.Delete,
                                    Override = _user.AEFUR.Override,
                                    View = _user.AEFUR.View
                                };

                                db.Entry(AEFURPriv).State = EntityState.Added;
                            }

                            //=====ACTIVITY REPORT SUMMARY=========
                            if(_user.ActivityReportSummary != null)
                            {
                                UserPrivilege ActivityReportSummaryPriv = new UserPrivilege
                                {
                                    ID = Guid.NewGuid(),
                                    UserID = newUser.ID,
                                    Module = "SalesReport/ActivityReportSummary",
                                    Add = _user.ActivityReportSummary.Add,
                                    Edit = _user.ActivityReportSummary.Edit,
                                    Delete = _user.ActivityReportSummary.Delete,
                                    Override = _user.ActivityReportSummary.Override,
                                    View = _user.ActivityReportSummary.View
                                };

                                db.Entry(ActivityReportSummaryPriv).State = EntityState.Added;
                            }

                            //=========TOP CLIENTS=========
                            if (_user.TopClients != null)
                            {
                                UserPrivilege TopClientsPriv = new UserPrivilege
                                {
                                    ID = Guid.NewGuid(),
                                    UserID = newUser.ID,
                                    Module = "SalesReport/TopClients",
                                    Add = _user.TopClients.Add,
                                    Edit = _user.TopClients.Edit,
                                    Delete = _user.TopClients.Delete,
                                    Override = _user.TopClients.Override,
                                    View = _user.TopClients.View
                                };

                                db.Entry(TopClientsPriv).State = EntityState.Added;
                            }

                            if(_user.PercentageActivity != null)
                            {
                                UserPrivilege PercentageActivity = new UserPrivilege
                                {
                                    ID = Guid.NewGuid(),
                                    UserID = newUser.ID,
                                    Module = "SalesReport/PercentageActivity",
                                    Add = _user.PercentageActivity.Add,
                                    Edit = _user.PercentageActivity.Edit,
                                    Delete = _user.PercentageActivity.Delete,
                                    Override = _user.PercentageActivity.Override,
                                    View = _user.PercentageActivity.View
                                };

                                db.Entry(PercentageActivity).State = EntityState.Added;
                            }
                        }
                    }
                    else //UPDATE
                    {
                        var user = db.UserAccount.FirstOrDefault(r => r.ID == _user.ID);

                        if(user != null)
                        {
                            user.AgentCode1 = _user.AgentCode1;
                            user.AgentCode2 = _user.AgentCode2;
                            user.AgentCode3 = _user.AgentCode3;
                            user.CebBizPartner = _user.CebBizPartner;
                            user.DepartmentHead = _user.DepartmentHead;
                            user.Department = _user.Department;
                            user.Email = _user.Email;
                            user.FirstName = _user.FirstName;
                            user.MiddleInitial = _user.MiddleInitial;
                            user.LastName = _user.LastName;
                            user.PartnerAgent = _user.PartnerAgent;
                            user.Status = _user.Status;
                            user.Username = _user.Username;
                            user.ClientMagicLogin = _user.ClientMagicLogin;

                            db.Entry(user).State = EntityState.Modified;

                            if (_user.AMActivityReport != null)
                            {
                                var AMActivityReportPriv = db.UserPrivilege.FirstOrDefault(r => r.Module == "SalesReport" && r.UserID == user.ID);

                                if (AMActivityReportPriv != null)
                                {
                                    AMActivityReportPriv.Add = _user.AMActivityReport.Add;
                                    AMActivityReportPriv.Edit = _user.AMActivityReport.Edit;
                                    AMActivityReportPriv.Delete = _user.AMActivityReport.Delete;
                                    AMActivityReportPriv.Override = _user.AMActivityReport.Override;
                                    AMActivityReportPriv.View = _user.AMActivityReport.View;

                                    db.Entry(AMActivityReportPriv).State = EntityState.Modified;
                                }
                                else
                                {
                                    UserPrivilege NewAMActivityReportPriv = new UserPrivilege
                                    {
                                        ID = Guid.NewGuid(),
                                        UserID = user.ID,
                                        Module = "SalesReport",
                                        Add = _user.AMActivityReport.Add,
                                        Edit = _user.AMActivityReport.Edit,
                                        Delete = _user.AMActivityReport.Delete,
                                        Override = _user.AMActivityReport.Override,
                                        View = _user.AMActivityReport.View
                                    };

                                    db.Entry(NewAMActivityReportPriv).State = EntityState.Added;
                                }
                            } //AMActivityReport

                            if(_user.AEFUR != null)
                            {
                                var AEFURPriv = db.UserPrivilege.FirstOrDefault(r => r.Module == "AEFUR" && r.UserID == user.ID);

                                if(AEFURPriv != null)
                                {
                                    AEFURPriv.Add = _user.AEFUR.Add;
                                    AEFURPriv.Edit = _user.AEFUR.Edit;
                                    AEFURPriv.Delete = _user.AEFUR.Delete;
                                    AEFURPriv.Override = _user.AEFUR.Override;
                                    AEFURPriv.View = _user.AEFUR.View;

                                    db.Entry(AEFURPriv).State = EntityState.Modified;
                                }
                                else
                                {
                                    UserPrivilege NewAEFURPriv = new UserPrivilege
                                    {
                                        ID = Guid.NewGuid(),
                                        UserID = user.ID,
                                        Module = "AEFUR",
                                        Add = _user.AEFUR.Add,
                                        Edit = _user.AEFUR.Edit,
                                        Delete = _user.AEFUR.Delete,
                                        Override = _user.AEFUR.Override,
                                        View = _user.AEFUR.View
                                    };

                                    db.Entry(NewAEFURPriv).State = EntityState.Added;
                                }
                            } //AEFUR

                            if(_user.ActivityReportSummary != null)
                            {
                                var ActivityReportSummaryPriv = db.UserPrivilege.FirstOrDefault(r => r.Module == "SalesReport/ActivityReportSummary" && r.UserID == user.ID);

                                if(ActivityReportSummaryPriv != null)
                                {
                                    ActivityReportSummaryPriv.Add = _user.ActivityReportSummary.Add;
                                    ActivityReportSummaryPriv.Edit = _user.ActivityReportSummary.Edit;
                                    ActivityReportSummaryPriv.Delete = _user.ActivityReportSummary.Delete;
                                    ActivityReportSummaryPriv.Override = _user.ActivityReportSummary.Override;
                                    ActivityReportSummaryPriv.View = _user.ActivityReportSummary.View;

                                    db.Entry(ActivityReportSummaryPriv).State = EntityState.Modified;
                                }
                                else
                                {
                                    UserPrivilege NewActivityReportSummaryPriv = new UserPrivilege
                                    {
                                        ID = Guid.NewGuid(),
                                        UserID = user.ID,
                                        Module = "SalesReport/ActivityReportSummary",
                                        Add = _user.ActivityReportSummary.Add,
                                        Edit = _user.ActivityReportSummary.Edit,
                                        Delete = _user.ActivityReportSummary.Delete,
                                        Override = _user.ActivityReportSummary.Override,
                                        View = _user.ActivityReportSummary.View
                                    };

                                    db.Entry(NewActivityReportSummaryPriv).State = EntityState.Added;
                                }
                            } //Activity Report Summary

                            if(_user.TopClients != null)
                            {
                                var TopClients = db.UserPrivilege.FirstOrDefault(r => r.Module == "SalesReport/TopClients" && r.UserID == user.ID);

                                if(TopClients != null)
                                {
                                    TopClients.Add = _user.TopClients.Add;
                                    TopClients.Edit = _user.TopClients.Edit;
                                    TopClients.Delete = _user.TopClients.Delete;
                                    TopClients.Override = _user.TopClients.Override;
                                    TopClients.View = _user.TopClients.View;

                                    db.Entry(TopClients).State = EntityState.Modified;
                                }
                                else
                                {
                                    UserPrivilege TopClientsPriv = new UserPrivilege
                                    {
                                        ID = Guid.NewGuid(),
                                        UserID = user.ID,
                                        Module = "SalesReport/TopClients",
                                        Add = _user.TopClients.Add,
                                        Edit = _user.TopClients.Edit,
                                        Delete = _user.TopClients.Delete,
                                        Override = _user.TopClients.Override,
                                        View = _user.TopClients.View
                                    };

                                    db.Entry(TopClientsPriv).State = EntityState.Added;
                                }
                            }//TOP CLIENTS

                            if(_user.PercentageActivity != null)
                            {
                                var PercentageActivity = db.UserPrivilege.FirstOrDefault(r => r.Module == "SalesReport/PercentageActivity" && r.UserID == user.ID);

                                if(PercentageActivity != null)
                                {
                                    PercentageActivity.Add = _user.PercentageActivity.Add;
                                    PercentageActivity.Edit = _user.PercentageActivity.Edit;
                                    PercentageActivity.Delete = _user.PercentageActivity.Delete;
                                    PercentageActivity.Override = _user.PercentageActivity.Override;
                                    PercentageActivity.View = _user.PercentageActivity.View;

                                    db.Entry(PercentageActivity).State = EntityState.Modified;
                                }
                                else
                                {
                                    UserPrivilege PercentageActivityPriv = new UserPrivilege
                                    {
                                        ID = Guid.NewGuid(),
                                        UserID = user.ID,
                                        Module = "SalesReport/PercentageActivity",
                                        Add = _user.PercentageActivity.Add,
                                        Edit = _user.PercentageActivity.Edit,
                                        Delete = _user.PercentageActivity.Delete,
                                        Override = _user.PercentageActivity.Override,
                                        View = _user.PercentageActivity.View
                                    };

                                    db.Entry(PercentageActivityPriv).State = EntityState.Added;
                                }
                            }//PERCENTAGE ACTIVITY
                        }//IF USER != NULL
                    }//ELSE FOR UPDATE

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        private static PriviledgeModel GetPriviledge(Guid userID, string module)
        {
            try
            {
                using (var db = new ObizEntities())
                {
                    var priv = from p in db.UserPrivilege
                               where p.UserID == userID && p.Module == module
                               select new PriviledgeModel
                               {
                                   ID = p.ID,
                                   UserID = p.UserID,
                                   Add = p.Add,
                                   Edit = p.Edit,
                                   Delete = p.Delete,
                                   View = p.View,
                                   Override = p.Override,
                                   Module = p.Module
                               };

                    return priv.FirstOrDefault();
                }
            }
            catch { return null; }
        }

        public static UserAccountModel GetSelectedUser(Guid ID ,out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var user = from u in db.UserAccount
                               where u.ID == ID
                               select new UserAccountModel
                               {
                                   ID = u.ID,
                                   FirstName = u.FirstName,
                                   MiddleInitial = u.MiddleInitial,
                                   LastName = u.LastName,
                                   Username = u.Username,
                                   Status = u.Status,
                                   Department = u.Department,
                                   Email = u.Email,
                                   AgentCode1 = u.AgentCode1,
                                   AgentCode2 = u.AgentCode2,
                                   AgentCode3 = u.AgentCode3,
                                   PartnerAgent = u.PartnerAgent,
                                   CebBizPartner = u.CebBizPartner,
                               };

                    var temp = user.FirstOrDefault();

                    temp.AMActivityReport = GetPriviledge(ID, "SalesReport");

                    temp.AEFUR = GetPriviledge(ID, "AEFUR");

                    temp.Report = GetPriviledge(ID, "Report");

                    temp.TopClients = GetPriviledge(ID, "SalesReport/TopClients");

                    temp.PercentageActivity = GetPriviledge(ID, "SalesReport/PercentageActivity");

                    temp.ActivityReportSummary = GetPriviledge(ID, "SalesReport/ActivityReportSummary");

                    return temp;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<UserAccountModel> GetAllClient(out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var userList = from u in db.UserAccount
                                   where u.ID != null
                                   select new UserAccountModel
                                   {
                                       ID = u.ID,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       MiddleInitial = u.MiddleInitial,
                                       Status = u.Status,
                                       //Password = u.Password,
                                       Username = u.Username,
                                       Department = u.Department,
                                   };

                    return userList.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static UserAccountModel ValidateLogin(string _username, string _password, out string message)
        {
            UserAccountModel userModel = null;

            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.Username == _username);

                    if (user != null)
                    {
                        if (user.Password == _password)
                        {
                            userModel = new UserAccountModel
                            {
                                Username = user.Username,
                                Password = user.Password,
                                SessionID = null,
                                Status = user.Status,
                                FirstName = user.FirstName,
                                MiddleInitial = user.MiddleInitial,
                                LastName = user.LastName,                       
                            };
                        }
                        else
                        {
                            message = "Invalid Password";
                        }
                    }
                    else
                        message = "Invalid Username";
                }

                   
            }
            catch(Exception error)
            {
                message = error.Message;
            }

            return userModel;
        }

        public static void LoginToSession (UserAccountModel userModel, out string message)
        {
            try
            {
                message = "";

                HttpContext.Current.Session["session_status"] = "online";

                PrincipalSerializeModel serializeModel = new PrincipalSerializeModel();

                serializeModel.Username = userModel.Username;

                serializeModel.Password = userModel.Password;

                serializeModel.SessionID = userModel.SessionID;

                serializeModel.Status = userModel.Status;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authenticationObiz = new FormsAuthenticationTicket
                    (1, userModel.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                string encryptedTicket = FormsAuthentication.Encrypt(authenticationObiz);

                HttpCookie authenticationCookie_Obiz = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpResponse response = HttpContext.Current.Response;

                response.Cookies.Add(authenticationCookie_Obiz);
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void LogoutFromSession(out string message)
        {
            try
            {
                message = "";

                FormsAuthentication.SignOut();
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}