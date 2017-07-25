using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using System.Data.Entity;

namespace Obiz.Services
{
    public class SalesReportService
    {
        public static void SaveAttachments(Guid SalesID, SalesReportAttachmentModel attachment, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    SalesReportAttachment newAttachment = new SalesReportAttachment
                    {
                        ID = Guid.NewGuid(),
                        SalesReportID = SalesID,
                        Extension = attachment.Extension,
                        FileName = attachment.FileName,
                        FileSize = attachment.FileSize,
                        ModifiedBy = UniversalHelper.CurrentUser.ID,
                        ModifiedDate = DateTime.Now,
                        Path = attachment.Path
                    };

                    db.Entry(newAttachment).State = EntityState.Added;

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<SalesReportAttachmentModel> GetAttachments (Guid SalesID, out string message)
        {
            try
            {
                message = "";

                using(var db = new ObizEntities())
                {
                    var query = from a in db.SalesReportAttachment
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where a.SalesReportID == SalesID
                                select new SalesReportAttachmentModel
                                {
                                    ID = a.ID,
                                    Extension = a.Extension,
                                    FileName = a.FileName,
                                    FileSize = a.FileSize,
                                    Path = a.Path,
                                    Status = "Y",
                                    ModifiedBy = a.ModifiedBy,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName,
                                    ModifiedDate = a.ModifiedDate,
                                    SalesID = a.SalesReportID
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void DeleteAttachments(SalesReportAttachmentModel item, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    if(item.Status == "X")
                    {
                        var file = db.SalesReportAttachment.FirstOrDefault(r => r.ID == item.ID);

                        if (file != null)
                            db.Entry(file).State = EntityState.Deleted;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<TopClientReportModel> GetTop10Clients (out string message, DateTime? startDate = null,
            DateTime? endDate = null, Guid? accountManager = null)
        {
            try
            {
                if (startDate == null)
                    startDate = DateTime.Now.AddMonths(-3);

                if (endDate == null)
                    endDate = DateTime.Now;

                message = "";

                using (var db = new ObizEntities())
                {
                    List<TopClientReportModel> topClient = new List<TopClientReportModel>();

                    List<ClientCountModel> clientList = new List<ClientCountModel>();

                    var clt = db.ClientProfile.ToList();

                    clt.ForEach(item =>
                    {
                        var sales = db.SalesReport.Where(r => r.ClientID == item.ID && r.Date >= startDate
                            && r.Date <= endDate);

                        int salesCount = sales.Count();

                        if(accountManager != null)
                        {
                            sales = sales.Where(r => r.UserID == accountManager);

                            salesCount = sales.Count();
                        }

                        ClientCountModel temp = new ClientCountModel
                        {
                            ClietName = item.ClientName,
                            Count = salesCount
                        };

                        clientList.Add(temp);
                    });

                    clientList = clientList.OrderByDescending(r => r.Count).ToList();

                    for(int x = 0; x < 10; x++)
                    {
                        TopClientReportModel tempTop = new TopClientReportModel
                        {
                            ClientName = clientList[x].ClietName,
                            Count = clientList[x].Count,
                            No = x + 1
                        };

                        topClient.Add(tempTop);
                    }

                    return topClient;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static PercentageOfTypeOfActivty GetPercentageOfTypeOfActivity(out string message, DateTime? startDate = null,
            DateTime? endDate = null, Guid? accountManager = null)
        {
            try
            {
                if (startDate == null)
                    startDate = DateTime.Now.AddMonths(-3);

                if (endDate == null)
                    endDate = DateTime.Now;

                message = "";

                using (var db = new ObizEntities())
                {
                    PercentageOfTypeOfActivty pOfActivity = new PercentageOfTypeOfActivty();

                    pOfActivity.Count = new List<int>();

                    pOfActivity.TypeOfActivities.ForEach(item =>
                    {
                        var activityCount = db.SalesReport.Where(r => r.TypeOfActivity == item && r.Date >= startDate
                            && r.Date <= endDate);

                        if (accountManager != null)
                            activityCount = activityCount.Where(r => r.UserID == accountManager);

                        pOfActivity.Count.Add(activityCount.Count());
                    });

                    return pOfActivity;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static AMProductivityChart GetAMProductivity(out string message, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                message = "";

                //Default date parameter
                if (startDate == null)
                    startDate = DateTime.Now.AddMonths(-3);

                if (endDate == null)
                    endDate = DateTime.Now;

                using (var db = new ObizEntities())
                {
                    AMProductivityChart AMProd = new AMProductivityChart();

                    AMProd.Names = new List<string>();

                    AMProd.Productivities = new List<int>();

                    var users = db.UserAccount.Where(r => r.Department == "AM").ToList();

                    users.ForEach(user =>
                    {
                        var actReport = db.SalesReport.Where(r => r.UserID == user.ID && r.Date >= startDate
                        && r.Date <= endDate).ToList();

                        AMProd.Names.Add(user.FirstName + " " + user.LastName);

                        AMProd.Productivities.Add(actReport.Count);
                    });

                    return AMProd;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static SalesReportModel GetSingleSalesReport(Guid ID, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from obSales in db.SalesReport
                                join obUser in db.UserAccount on obSales.UserID equals obUser.ID
                                join tcClient in db.ClientProfile on obSales.ClientID equals tcClient.ID
                                join cby in db.UserAccount on obSales.CreatedBy equals cby.ID into qCby
                                from createdBy in qCby.DefaultIfEmpty()
                                join mby in db.UserAccount on obSales.ModifiedBy equals mby.ID into qMby
                                from modifiedBy in qMby.DefaultIfEmpty()
                                where obSales.ID == ID
                                select new SalesReportModel
                                {
                                    ID = obSales.ID,
                                    UserID = obSales.UserID,
                                    AccountManager = obUser.FirstName + " " + obUser.LastName,
                                    ClientID = obSales.ClientID,
                                    ClientName = tcClient.ClientName,
                                    AgendaIssueConcerns = obSales.AgendaIssueConcerns,
                                    BCDDeliverables = obSales.BCDDeliverables,
                                    CompanyUpdate = obSales.CompanyUpdate,
                                    Date = obSales.Date,
                                    DueDate = obSales.DueDate,
                                    Remarks = obSales.Remarks,
                                    ScopeAction = obSales.ScopeAction,
                                    TypeOfActivity = obSales.TypeOfActivity,
                                    ShowDate = obSales.Date.ToString(),
                                    ShowDueDate = obSales.DueDate.ToString(),
                                    CreatedBy = obSales.CreatedBy,
                                    CreatedDate = obSales.CreatedDate,
                                    ModifiedBy = obSales.ModifiedBy,
                                    ModifiedDate = obSales.ModifiedDate,
                                    Status = obSales.Status,
                                    Category = obSales.Category,
                                    ShowCreatedBy = createdBy.FirstName + " " +createdBy.LastName,
                                    ShowModifiedBy = modifiedBy.FirstName + " " + modifiedBy.LastName,
                                    SLA = obSales.SLA
                                };

                    var temp = query.FirstOrDefault();

                    temp.FileUploaded = GetAttachments(temp.ID, out message);
  
                    return temp;
                }

            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveSalesReport(SalesReportModel sales, out string message, out Guid ID)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var sale = db.SalesReport.FirstOrDefault(r => r.ID == sales.ID);

                    if(sale != null)
                    {
                        message = "Updated";

                        ID = sales.ID;

                        sale.Date = sales.Date;
                        sale.DueDate = sales.DueDate;
                        sale.AgendaIssueConcerns = sales.AgendaIssueConcerns;
                        sale.BCDDeliverables = sales.BCDDeliverables;
                        sale.ClientID = sales.ClientID;
                        sale.CompanyUpdate = sales.CompanyUpdate;
                        sale.Remarks = sales.Remarks;
                        sale.ScopeAction = sales.ScopeAction;
                        sale.TypeOfActivity = sales.TypeOfActivity;
                        sale.UserID = sales.UserID;
                        sale.ModifiedBy = UniversalHelper.CurrentUser.ID;
                        sale.ModifiedDate = DateTime.Now;
                        sale.Status = sales.Status;
                        sale.Category = sales.Category;
                        sale.SLA = sales.SLA;

                        db.Entry(sale).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        ID = new Guid();

                        SalesReport newSalesReport = new SalesReport
                        {
                            ID = ID,
                            Date = sales.Date,
                            DueDate = sales.DueDate,
                            AgendaIssueConcerns = sales.AgendaIssueConcerns,
                            BCDDeliverables = sales.BCDDeliverables,
                            ClientID = sales.ClientID,
                            CompanyUpdate = sales.CompanyUpdate,
                            Remarks = sales.Remarks,
                            ScopeAction = sales.ScopeAction,
                            TypeOfActivity = sales.TypeOfActivity,
                            UserID = sales.UserID,
                            CreatedBy = UniversalHelper.CurrentUser.ID,
                            CreatedDate = DateTime.Now,
                            Status = sales.Status,
                            Category = sales.Category,
                            SLA = sales.SLA
                        };

                        db.Entry(newSalesReport).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                ID = Guid.Empty;
            }
        }

       

        public static List<SalesReportModel> GetSalesReportList(out string message, DateTime? startDate = null, DateTime? endDate = null, Guid? Client = null,
            DateTime? startDueDate = null, DateTime? endDueDate = null, Guid? accountManager = null, string typeOfActivity = null)
        {
            try
            {
                message = "";

                if (startDate == null)
                    startDate = DateTime.Now.AddMonths(-2);

                if (endDate == null)
                    endDate = DateTime.Now;

                using (var dbObiz = new ObizEntities())
                {
                        var query = from obSales in dbObiz.SalesReport
                                    join obUser in dbObiz.UserAccount on obSales.UserID equals obUser.ID
                                    join tcClient in dbObiz.ClientProfile on obSales.ClientID equals tcClient.ID
                                    join cby in dbObiz.UserAccount on obSales.CreatedBy equals cby.ID into qCby
                                    from createdBy in qCby.DefaultIfEmpty()
                                    join mby in dbObiz.UserAccount on obSales.ModifiedBy equals mby.ID into qMby
                                    from modifiedBy in qMby.DefaultIfEmpty()
                                    where obSales.CreatedDate >= startDate && obSales.CreatedDate <= endDate
                                    select new SalesReportModel
                                    {
                                        ID = obSales.ID,
                                        UserID = obSales.UserID,
                                        AccountManager = obUser.FirstName + " " + obUser.LastName,
                                        ClientID = obSales.ClientID,
                                        ClientName = tcClient.ClientName,
                                        AgendaIssueConcerns = obSales.AgendaIssueConcerns,
                                        BCDDeliverables = obSales.BCDDeliverables,
                                        CompanyUpdate = obSales.CompanyUpdate,
                                        Date = obSales.Date,
                                        DueDate = obSales.DueDate,
                                        Remarks = obSales.Remarks,
                                        ScopeAction = obSales.ScopeAction,
                                        TypeOfActivity = obSales.TypeOfActivity,
                                        ShowDate = obSales.Date.ToString(),
                                        ShowDueDate = obSales.DueDate.ToString(),
                                        CreatedBy = obSales.CreatedBy,
                                        CreatedDate = obSales.CreatedDate,
                                        ModifiedBy = obSales.ModifiedBy,
                                        ModifiedDate = obSales.ModifiedDate,
                                        Status = obSales.Status,
                                        Category = obSales.Category,
                                        ShowCreatedBy = createdBy.FirstName + " " +createdBy.LastName,
                                        ShowModifiedBy = modifiedBy.FirstName + " " + modifiedBy.LastName,
                                        SLA = obSales.SLA
                                    };

                    if (query.ToList() != null)
                    {
                        List<SalesReportModel> returnList = query.ToList();

                        if (Client != null)
                            returnList = returnList.Where(r => r.ClientID == Client).ToList();

                        if (startDueDate != null)
                            returnList = returnList.Where(r => r.DueDate >= startDueDate).ToList();

                        if (endDueDate != null)
                            returnList = returnList.Where(r => r.DueDate <= endDueDate).ToList();

                        if (accountManager != null)
                            returnList = returnList.Where(r => r.UserID == accountManager).ToList();

                        if (typeOfActivity != null)
                            returnList = returnList.Where(r => r.TypeOfActivity == typeOfActivity).ToList();

                        if (returnList.AsQueryable().OrderByDescending(r => r.Date).ToList().Count > 0)
                            return returnList.AsQueryable().OrderByDescending(r => r.Date).ToList();
                        else
                            return new List<SalesReportModel>();
                    }
                    else
                        return new List<SalesReportModel>();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }
    }
}