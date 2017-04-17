using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;
using Obiz.Models;

namespace Obiz.Controllers
{
    public class SalesReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesReport(Guid? ID)
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult ActivityReportSummary()
        {
            return View();
        }

        public ActionResult PercentageTypeOfActivity()
        {
            return View();
        }

        public ActionResult TopClients()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetReportPriviledge()
        {
            string serverResponse = "";

            var activityReportPriviledge = UniversalService.GetPriviledge("SalesReport/ActivityReportSummary", out serverResponse);

            var percentageActivityPriviledge = UniversalService.GetPriviledge("SalesReport/PercentageActivity", out serverResponse);

            var topClientPriviledge = UniversalService.GetPriviledge("SalesReport/TopClients", out serverResponse);

            return Json(new {
                activityReportPriviledge = activityReportPriviledge,
                percentageActivityPriviledge = percentageActivityPriviledge,
                topClientPriviledge = topClientPriviledge,
                errorMessage = serverResponse}); 
        }

        [HttpPost]
        public JsonResult GetTop10Clients(DateTime? startDate = null, DateTime? endDate = null)
        {
            string serverResponse = "";
            
            var top = SalesReportService.GetTop10Clients(out serverResponse, startDate, endDate);

            return Json(new { top = top });
        }

        [HttpPost]
        public JsonResult GetPercentageOfTypeOfActivity(DateTime? startDate = null, DateTime? endDate = null)
        {
            string serverResponse = "";

            var percentage = SalesReportService.GetPercentageOfTypeOfActivity(out serverResponse, startDate, endDate);

            return Json(new { percentage = percentage });
        }

        [HttpPost]
        public ActionResult GetReports(DateTime? startDate = null, DateTime? endDate = null)
        {
            string serverResponse = "";

            var AMProductivity = SalesReportService.GetAMProductivity(out serverResponse, startDate, endDate);

            return Json(new { AMProductivity = AMProductivity });
        }

        [HttpPost]
        public ActionResult GetDropdown()
        {
            string serverResponse = "";

            var clientDropDown = UniversalService.GetClientDropDown(out serverResponse);

            var accountManagerDropDown = UniversalService.GetAllAMUser(out serverResponse);

            return Json(new { ClientDropdown = clientDropDown, AccountManagerDropdown = accountManagerDropDown });
        }

        [HttpPost]
        public JsonResult InitNewSalesReport()
        {
            string serverResponse = "";

            var priviledge = UniversalService.GetPriviledge("SalesReport", out serverResponse);

            return Json(new { priviledge = priviledge });
        }

        [HttpPost]
        public JsonResult OpenSalesReport(Guid ID)
        {
            string serverResponse = "";

            var salesReport = SalesReportService.GetSingleSalesReport(ID, out serverResponse);

            var priviledge = UniversalService.GetPriviledge("SalesReport" ,out serverResponse);

            return Json(new { salesReport = salesReport, priviledge = priviledge });
        }

        [HttpPost]
        public JsonResult GetSalesReport(DateTime? startDate = null, DateTime? endDate = null, Guid? client = null,
            DateTime? startDueDate = null, DateTime? endDueDate = null)
        {
            string serverResponse = "";


            var list = SalesReportService.GetSalesReportList(out serverResponse, startDate, endDate, client, startDueDate, endDueDate);

            var priviledge = UniversalService.GetPriviledge("SalesReport", out serverResponse);

            return Json(new { list = list, message = serverResponse, priviledge = priviledge });
        }

        [HttpPost]
        public JsonResult SaveSalesReport(SalesReportModel salesReport)
        {
            string serverResponse = "";

            if(salesReport != null)
                SalesReportService.SaveSalesReport(salesReport, out serverResponse);

            return Json(serverResponse);
        }

    }
}