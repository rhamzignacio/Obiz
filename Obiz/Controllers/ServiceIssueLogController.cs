using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;
using Obiz.Models;

namespace Obiz.Controllers
{
    public class ServiceIssueLogController : Controller
    {
        // GET: ServiceIssueLog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OpenIssueLog(Guid? ID)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDropdown()
        {
            string serverResponse = "";

            var clientDropDown = UniversalService.GetClientDropDown(out serverResponse);

            var userDropDown = UniversalService.GetUserAccounts(out serverResponse);

            var priviledge = UniversalService.GetPriviledge("ServiceIssueLog", out serverResponse);

            return Json(new { clientDropDown = clientDropDown, userDropDown = userDropDown, priviledge = priviledge,
             serviceIssue = new ServiceIssueModel() });
        }

        [HttpPost]
        public JsonResult OpenServiceIssue(Guid? ID)
        {
            string serverResponse = "";

            var serviceIssue = ServiceIssueLogService.GetSingleServiceIssue(ID, out serverResponse);

            return Json(new { serviceIssue = serviceIssue });
        }

        [HttpPost]
        public JsonResult GetServiceIssue()
        {
            string serverResponse = "";

            var list = ServiceIssueLogService.GetServiceIssueLogs(out serverResponse);

            var clientDropDown = UniversalService.GetClientDropDown(out serverResponse);

            var userDropDown = UniversalService.GetUserAccounts(out serverResponse);

            var priviledge = UniversalService.GetPriviledge("ServiceIssueLog",out serverResponse);

            return Json(new { list = list, mesage = serverResponse, priviledge = priviledge, clientDropDown = clientDropDown,
                userDropDown = userDropDown });
        }

        [HttpPost]
        public JsonResult SaveServiceIssue(ServiceIssueModel logs)
        {
            string serverResponse = "";

            if (logs != null)
                ServiceIssueLogService.SaveServiceIssueLog(logs, out serverResponse);

            return Json(serverResponse);
        }
    }
}