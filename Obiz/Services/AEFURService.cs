using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obiz.Models;
using System.Data.Entity;

namespace Obiz.Services
{
    public class AEFURService
    {
        public static AEFURUnbilledReport GetAEFURUnbilledReport(out string message)
        {
            try
            {
                message = "";

                using(var obizDB = new ObizEntities())
                using (var travDB = new TravComEntities())
                {
                    var unbilled = travDB.AEFURUnbilledCount.ToList();
                    var user = obizDB.UserAccount.Where(r=>r.Department == "BL" || r.Department == "MM" || r.Department == "MC").OrderBy(r=>r.FirstName).ToList();

                   AEFURUnbilledReport unbilledList = new AEFURUnbilledReport();

                    unbilledList.BLDUnbilled = new AEFURBarChartModel();
                    unbilledList.BLDUnbilled.TCName = new List<string>();
                    unbilledList.BLDUnbilled.Count = new List<int?>();

                    unbilledList.MMUnbilled = new AEFURBarChartModel();
                    unbilledList.MMUnbilled.TCName = new List<string>();
                    unbilledList.MMUnbilled.Count = new List<int?>();

                    unbilledList.MCUnbilled = new AEFURBarChartModel();
                    unbilledList.MCUnbilled.TCName = new List<string>();
                    unbilledList.MCUnbilled.Count = new List<int?>();

                    user.ForEach(item =>
                    {
                        var temp = unbilled.FirstOrDefault(r => r.BookingAgent.ToUpper() == (item.FirstName.ToUpper() + " " + item.LastName.ToUpper()));

                        if (temp != null)
                        {
                            if(item.Department == "BL")
                            {
                                unbilledList.BLDUnbilled.TCName.Add(temp.BookingAgent.ToUpper());

                                unbilledList.BLDUnbilled.Count.Add(temp.UnbilledCount);
                            }
                            else if(item.Department == "MM")
                            {
                                unbilledList.MMUnbilled.TCName.Add(temp.BookingAgent.ToUpper());

                                unbilledList.MMUnbilled.Count.Add(temp.UnbilledCount);
                            }
                            else if(item.Department == "MC")
                            {
                                unbilledList.MCUnbilled.TCName.Add(temp.BookingAgent.ToUpper());

                                unbilledList.MCUnbilled.Count.Add(temp.UnbilledCount);
                            }
                        }
                    });

                    return unbilledList;
                }
            }
           catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void FreeFieldTick(string ticketNo, out string message)
        {
            try
            {
                message = "";

                using (var db = new TravComEntities())
                {
                    var invoice = db.IfInvoiceDetails.Where(r => r.TicketNumber.Contains(ticketNo)).ToList();

                    invoice.ForEach(inv =>
                    {
                        string val = "";

                        var temp = inv.FreeFieldA;

                        string[] freeFields = new string[99];

                        if (temp != "")
                            freeFields = temp.Split('/');

                        for(int x = 0; x < freeFields.Count(); x++ )
                        {
                            if (x < 99)
                                val += freeFields[x] + "/";
                            else if(x == 99)
                                val += "AEFUR(" + DateTime.Now.ToString().Replace("/", "-") + ")";
                        }

                        if(freeFields.Count() < 100)
                        {
                            for(int x = freeFields.Count(); x < 100; x++)
                            {
                                if (x < 99)
                                    val += "/";

                                if(x == 99)
                                    val += "AEFUR(" + DateTime.Now.ToString().Replace("/", "-") + ")";
                            }
                        }

                        inv.FreeFieldA = val;

                        db.Entry(inv).State = EntityState.Modified;
                    });

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<AEFURNoRecordModel> GetNoRecordPerTC(out string message)
        {
            message = "";
            try
            {
                using (var aefurDB = new AEFUREntities())
                {
                    using (var obizDB = new ObizEntities())
                    {
                        var user = obizDB.UserAccount.FirstOrDefault(r => r.ID == UniversalHelper.CurrentUser.ID);

                        var query = from a in aefurDB.AEFURNoRecord
                                    where a.AgentCode == user.PartnerAgent || a.AgentCode == user.CebBizPartner
                                    && a.AgentCode != null
                                    select new AEFURNoRecordModel
                                    {
                                        TicketNumber = a.TicketNo,
                                        RecordLocator = a.RecordLocator,
                                        AgentCode = a.AgentCode,
                                        AgentName = a.AgentName,
                                        Date = a.DateRange,
                                        BookingAmount = a.BookingAmount,
                                        PassengerName = a.PassengerName,
                                        CurrencyCode = a.Currency
                                    };

                        return query.GroupBy(x => new { x.TicketNumber, x.RecordLocator }).Select(x => x.FirstOrDefault()).ToList();
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<AEFURUnbilledModel> GetUnbilledPerTC (out string message)
        {
            message = "";
            try
            {
                using (var travDB = new TravComEntities())
                {
                    using (var obizDB = new ObizEntities())
                    {
                        var user = obizDB.UserAccount.FirstOrDefault(r => r.ID == UniversalHelper.CurrentUser.ID);

                        var query = from u in travDB.AEFURUnbilled
                                    where u.BookingAgentNumber != null && !u.FreeFieldA.Contains("AEFUR") &&
                                    (u.BookingAgentNumber == user.AgentCode1 || u.BookingAgentNumber == user.AgentCode2 ||
                                     u.BookingAgentNumber == user.AgentCode3)
                                    select new AEFURUnbilledModel
                                    {
                                        BookingDate = u.BookingDate,
                                        TicketingAgent = u.TicketingAgent,
                                        InvoiceID = u.InvoiceID,
                                        InvoiceDetailID = u.InvoiceDetailID,
                                        CurrencyCode = u.CurrencyCode,
                                        TransactionDate = u.TransactionDate,
                                        VendorNumber = u.VendorNumber,
                                        InvoiceNumber = u.InvoiceNumber,
                                        TicketNumber = u.TicketNumber,
                                        TransactionType = u.TransactionType,
                                        InvoiceDate = u.InvoiceDate,
                                        ProfileName = u.ProfileName,
                                        BookingAgentNumber = u.BookingAgentNumber,
                                        PassengerName = u.PassengerName,
                                        GrossAmount = u.GrossAmount,
                                        RecordLocator = u.RecordLocator,
                                        FullName = u.FullName,
                                        FreeFieldA = u.FreeFieldA,
                                        Department = u.Department,
                                        TicketingAgentNumber = u.TicketingAgentNumber,
                                        Status = "Y",
                                        
                                    };

                        return query.GroupBy(x=> new { x.TicketNumber, x.RecordLocator }).Select(x=>x.FirstOrDefault()).ToList();
                    }
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