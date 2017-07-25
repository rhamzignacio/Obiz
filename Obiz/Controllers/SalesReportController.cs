using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obiz.Services;
using Obiz.Models;
using System.IO;

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

        public ActionResult Dashboard()
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
        public JsonResult FileUpload()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;

            HttpFileCollection uploadFiles = httpRequest.Files;

            var docFiles = new List<string>();

            string newFileName = "";

            string ext = "";

            List<SalesReportAttachmentModel> returnList = new List<SalesReportAttachmentModel>();

            if(httpRequest.Files.Count > 0)
            {
                for(int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];

                    ext = Path.GetExtension(postedFile.FileName);

                    newFileName = DateTime.Now.ToString("MMddyyyHHmmSS");

                    var filePath = Server.MapPath(@"\FileUploads\" + newFileName + ext);

                    postedFile.SaveAs(filePath);

                    using(var db = new ObizEntities())
                    {
                        SalesReportAttachmentModel newAttachment = new SalesReportAttachmentModel
                        {
                            FileName = postedFile.FileName,
                            FileSize = (postedFile.ContentLength / 1024).ToString(),
                            Extension = ext,
                            Path = newFileName + ext,
                            ModifiedBy = UniversalHelper.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        returnList.Add(newAttachment);
                    }
                }
            }


            return Json(new { uploadedFiles = returnList });
        }

        [HttpPost]
        public JsonResult DeleteAttachment(SalesReportAttachmentModel file)
        {
            string serverResponse = "";

            if (file != null)
            {
                file.Status = "X";

                SalesReportService.DeleteAttachments(file, out serverResponse);
            }

            return Json(serverResponse);
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
        public JsonResult GetTop10Clients(DateTime? startDate = null, DateTime? endDate = null, Guid? accountManager = null)
        {
            string serverResponse = "";
            
            var top = SalesReportService.GetTop10Clients(out serverResponse, startDate, endDate, accountManager);

            return Json(new { top = top });
        }

        [HttpPost]
        public JsonResult GetPercentageOfTypeOfActivity(DateTime? startDate = null, DateTime? endDate = null, Guid? accountManager = null)
        {
            string serverResponse = "";

            var percentage = SalesReportService.GetPercentageOfTypeOfActivity(out serverResponse, startDate, endDate, accountManager);

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

            return Json(new { salesReport = salesReport, priviledge = priviledge});
        }

        [HttpPost]
        public JsonResult GetSalesReport(DateTime? startDate = null, DateTime? endDate = null, Guid? client = null,
            DateTime? startDueDate = null, DateTime? endDueDate = null, Guid? accountManager = null, string typeOfActivity = null)
        {
            string serverResponse = "";


            var list = SalesReportService.GetSalesReportList(out serverResponse, startDate, endDate, client, startDueDate, endDueDate, accountManager, typeOfActivity);

            var priviledge = UniversalService.GetPriviledge("SalesReport", out serverResponse);

            return Json(new { list = list, message = serverResponse, priviledge = priviledge });
        }

        [HttpPost]
        public JsonResult SaveSalesReport(SalesReportModel salesReport)
        {
            string serverResponse = "";

            string temp = "";

            Guid ID = Guid.Empty;

            if (salesReport != null)
            {
                SalesReportService.SaveSalesReport(salesReport, out serverResponse, out ID);

                if(salesReport.Attachments != null)
                {
                    salesReport.Attachments.ForEach(item =>
                    {
                        SalesReportService.SaveAttachments(ID, item, out temp);
                    });
                }
            }


            return Json(new { message = serverResponse, ID = ID });
        }

    }
}