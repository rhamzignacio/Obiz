using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;

namespace Obiz.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult UserAccount()
        {
            return View();
        }

        public ActionResult OpenUserAccount()
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
    }
}