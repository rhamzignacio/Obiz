using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Obiz.Services
{
    public class UserService
    {
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