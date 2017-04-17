using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using System.Data.Entity;

namespace Obiz.Services
{
    public class ServiceIssueLogService
    {
        public static ServiceIssueModel GetSingleServiceIssue(Guid? ID, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    if (ID != null)
                    {
                        var query = from logs in db.ServiceIssueLog
                                    join tc in db.UserAccount on logs.HandlingTCID equals tc.ID
                                    join clt in db.ClientProfile on logs.ClientID equals clt.ID
                                    join rpt in db.UserAccount on logs.ReportedBy equals rpt.ID
                                    where logs.ID == ID
                                    select new ServiceIssueModel
                                    {
                                        ID = logs.ID,
                                        BookerName = logs.BookerName,
                                        ClientID = clt.ID,
                                        ClientName = clt.ClientName,
                                        Department = logs.Department,
                                        HandlingTCID = tc.ID,
                                        HandlingTCName = tc.FirstName + " " + tc.LastName,
                                        IncidentDate = logs.IncidentDate,
                                        PaxName = logs.PaxName,
                                        ReportDate = logs.ReportDate,
                                        ReportedBy = rpt.ID,
                                        ReportedByName = rpt.FirstName + " " + rpt.LastName,
                                        Resolution = logs.Resolution,
                                        ResolveDate = logs.ResolveDate,
                                        SummaryNatureOfService = logs.SummaryNatureOfServiceIssue,
                                        TotalPax = logs.TotalPax,
                                        WasIRIssued = logs.WasIRIssued,
                                        WasMemoIssued = logs.WasMemoIssued,
                                    };

                        return query.FirstOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<ServiceIssueModel> GetServiceIssueLogs(out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    var query = from logs in db.ServiceIssueLog
                                join tc in db.UserAccount on logs.HandlingTCID equals tc.ID
                                join clt in db.ClientProfile on logs.ClientID equals clt.ID
                                join rpt in db.UserAccount on logs.ReportedBy equals rpt.ID
                                select new ServiceIssueModel
                                {
                                    ID = logs.ID,
                                    BookerName = logs.BookerName,
                                    ClientID = clt.ID,
                                    ClientName = clt.ClientName,
                                    Department = logs.Department,
                                    HandlingTCID = tc.ID,
                                    HandlingTCName = tc.FirstName + " " + tc.LastName,
                                    IncidentDate = logs.IncidentDate,
                                    PaxName = logs.PaxName,
                                    ReportDate = logs.ReportDate,
                                    ReportedBy = rpt.ID,
                                    ReportedByName = rpt.FirstName + " " + rpt.LastName,
                                    Resolution = logs.Resolution,
                                    ResolveDate = logs.ResolveDate,
                                    SummaryNatureOfService = logs.SummaryNatureOfServiceIssue,
                                    TotalPax = logs.TotalPax,
                                    WasIRIssued = logs.WasIRIssued,
                                    WasMemoIssued = logs.WasMemoIssued,
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

        public static void SaveServiceIssueLog(ServiceIssueModel logs, out string message)
        {
            try
            {
                message = "";

                using (var db = new ObizEntities())
                {
                    if(logs.ID == null || logs.ID == Guid.Empty) //NEW
                    {
                        message = "Saved";

                        ServiceIssueLog newLog = new ServiceIssueLog
                        {
                            ID = Guid.NewGuid(),
                            ClientID = logs.ClientID,
                            Department = logs.Department,
                            HandlingTCID = logs.HandlingTCID,
                            IncidentDate = logs.IncidentDate,
                            ReportDate = DateTime.Now,
                            ResolveDate = logs.ResolveDate,
                            PaxName = logs.PaxName,
                            ReportedBy =  UniversalHelper.CurrentUser.ID,
                            Resolution = logs.Resolution,
                            SummaryNatureOfServiceIssue = logs.SummaryNatureOfService,
                            TotalPax = logs.TotalPax,
                            WasIRIssued = logs.WasIRIssued,
                            WasMemoIssued = logs.WasMemoIssued,
                            BookerName = logs.BookerName
                        };

                        db.Entry(newLog).State = EntityState.Added;
                    }
                    else //UPDATE
                    {
                        message = "Updated";

                        var log = db.ServiceIssueLog.FirstOrDefault(r => r.ID == logs.ID);

                        if(log != null)
                        {
                            log.BookerName = logs.BookerName;

                            log.ClientID = logs.ClientID;

                            log.Department = logs.Department;

                            log.HandlingTCID = logs.HandlingTCID;

                            log.IncidentDate = logs.IncidentDate;

                            log.ResolveDate = logs.ResolveDate;

                            log.PaxName = logs.PaxName;

                            log.ReportedBy = logs.ReportedBy;

                            log.Resolution = logs.Resolution;

                            log.SummaryNatureOfServiceIssue = logs.SummaryNatureOfService;

                            log.TotalPax = logs.TotalPax;

                            log.WasIRIssued = logs.WasIRIssued;

                            log.WasMemoIssued = logs.WasMemoIssued;

                            db.Entry(log).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}