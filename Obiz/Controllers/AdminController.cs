using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;
using Obiz.Models;

namespace Obiz.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult UserAccount()
        {
            return View();
        }

        public ActionResult OpenUserAccount(Guid? ID)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUsers()
        {
            string serverResponse = "";

            var users = UserService.GetAllClient(out serverResponse);

            return Json(new { users = users, message = serverResponse });
        }

        [HttpPost]
        public JsonResult Save(UserAccountModel user)
        {
            string serverResponse = "";

            if (user != null)
                UserService.SaveUser(user, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetSelectedUser(Guid ID)

        {
            string serverResponse = "";

            var user = UserService.GetSelectedUser(ID, out serverResponse);

            return Json(new { user = user, errorMessage = serverResponse });
        }
    }
}