using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Models;
using Obiz.Services;

namespace Obiz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AEFURReport()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        { 
            string serverResponse = "";

            UserAccountModel user = UserService.ValidateLogin(username, password, out serverResponse);

            if (user != null)
                UserService.LoginToSession(user, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            string serverResponse = "";

            UserService.LogoutFromSession(out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetCurrentUser()
        {
            return Json(new { user = UniversalHelper.CurrentUser, menu = UniversalHelper.ObizMenu });
        }
    }
}