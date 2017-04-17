using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;

namespace Obiz.Services
{
    public class UniversalService
    {
        public static PriviledgeModel GetPriviledge(string module,out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from p in db.UserPrivilege
                                where p.UserID == UniversalHelper.CurrentUser.ID && p.Module == module
                                select new PriviledgeModel
                                {
                                    ID = p.ID,
                                    UserID = p.UserID,
                                    Add = p.Add,
                                    Edit = p.Edit,
                                    Delete = p.Delete,
                                    Override = p.Override,
                                    View = p.View
                                };

                    if (query == null)
                        message = "No priviledge Created";

                    return query.FirstOrDefault();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<AccountManagerDDModel> GetAllAMUser(out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from a in db.UserAccount
                                where a.Status == "Y" && a.Department == "AM"
                                select new AccountManagerDDModel
                                {
                                    ID = a.ID,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    MiddleInitial = a.MiddleInitial,
                                    Status = a.Status,
                                    FullName = a.FirstName + " " + a.LastName
                                };

                    return query.OrderBy(r => r.FullName).ToList();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<AccountManagerDDModel> GetUserAccounts(out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from a in db.UserAccount
                                where a.Status == "Y"
                                select new AccountManagerDDModel
                                {
                                    ID = a.ID,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    MiddleInitial = a.MiddleInitial,
                                    Status = a.Status,
                                    FullName = a.FirstName + " " + a.LastName
                                };

                    return query.OrderBy(r => r.FullName).ToList();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<ClientDDModel> GetClientDropDown(out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from c in db.ClientProfile
                                where c.Status == "Y"
                                select new ClientDDModel
                                {
                                    ID = c.ID,
                                    ClientName = c.ClientName,
                                    Status = c.Status
                                };

                    return query.OrderBy(r => r.ClientName).ToList();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }
    }
}