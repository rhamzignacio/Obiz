﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;

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

        [HttpPost]
        public JsonResult GetAEFURUnbilledReport()
        {
            string serverResponse = "";

            var unbilled = AEFURService.GetAEFURUnbilledReport(out serverResponse);

            var unbilledBLD = unbilled.Where(r => r.Department == "BL");

            var unbilledMM = unbilled.Where(r => r.Department == "MM");

            var unbilledMC = unbilled.Where(r => r.Department == "MC");

            return Json(new { unbilledBLD = unbilledBLD, unbilledMC = unbilledMC,
                unbilledMM = unbilledMM, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult GetUnbilledList()
        {
            string serverResponse = "";

            var unbilled = AEFURService.GetUnbilledPerTC(out serverResponse);

            var noRecord = AEFURService.GetNoRecordPerTC(out serverResponse);

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