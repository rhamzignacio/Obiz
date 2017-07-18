using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;
using Obiz.Models;

namespace Obiz.Controllers
{
    public class AEFURController : Controller
    {
        public ActionResult UnbilledMonitoring()
        {
            return View();
        }

        public ActionResult AEFURUnbilledReport()
        {
            return View();
        }

        public ActionResult ReportMenu()
        {
            return View();
        }

        public ActionResult InplantProductivity()
        {
            return View();
        }

        public ActionResult BillerDashBoard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InitBillerDashBoard()
        {
            string serverResponse = "";

            var biller = AEFURService.GetBillerDashboard(out serverResponse);

            return Json(new { biller = biller, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult GetAEFURUnbilledReport()
        {
            string serverResponse = "";

            var unbilled = AEFURService.GetAEFURUnbilledReport(out serverResponse);

            return Json(new { unbilled = unbilled, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult GetUnbilledList()
        {
            string serverResponse = "";

            List<AEFURUnbilledModel> unbilled = new List<AEFURUnbilledModel>();

            List<AEFURNoRecordModel> noRecord = new List<AEFURNoRecordModel>();

            if (UniversalHelper.CurrentUser.DepartmentHead == "N")
            {
                unbilled = AEFURService.GetUnbilledPerTC(out serverResponse);

                noRecord = AEFURService.GetNoRecordPerTC(out serverResponse);
            }
            else if(UniversalHelper.CurrentUser.DepartmentHead == "A")
            {
                unbilled = AEFURService.GetAllUnbilled(out serverResponse);

                noRecord = AEFURService.GetNoRecordPerTC(out serverResponse);
            }
            else if(UniversalHelper.CurrentUser.DepartmentHead == "Y")
            {
                unbilled = AEFURService.GetUnbilledPerDepartment(out serverResponse);

                noRecord = AEFURService.GetNoRecordPerTC(out serverResponse);
            }

            return Json(new { unbilled = unbilled, noRecord = noRecord, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult AEFURSubmit(string ticketNo)
        {
            string serverResponse = "";

            if(ticketNo != "")
                AEFURService.FreeFieldTick(ticketNo, out serverResponse);

            return (Json(serverResponse));
        }
    }
}