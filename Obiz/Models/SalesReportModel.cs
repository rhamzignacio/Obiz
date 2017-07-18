using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class SalesReportAttachmentModel
    {
        public Guid ID { get; set; }
        public Guid? SalesID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string FileSize { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Status { get; set; }
        public string ShowModifiedBy { get; set; }
    }

    public class ClientCountModel
    {
        public string ClietName { get; set; }
        public int Count { get; set; }
    }

    public class TopClientReportModel
    {
        public int No { get; set; }
        public string ClientName { get; set; }
        public int Count { get; set; }    
    }

    public class SalesReportViewModel
    {
        public IEnumerable<SalesReportModel> SalesReports { get; set; }
        public Pager Pager { get; set; }
    }

    public class AMProductivityChart
    {
        public List<string> Names { get; set; }
        public List<int> Productivities { get; set; }
    }

    public class PercentageOfTypeOfActivty
    {
        public List<string> ShowTypeOfActivities
        {
            get
            {
                List<string> showtemp = new List<string>
                {
                    "Concall with BCD RAM or BCD GAM",
                    "Coordination|Management|Adhoc Meetings",
                    "Internal Training|Briefing with Servicing Team",
                    "Preparation for Travel Forums|Road Shows",
                    "Preparation for Proposal|Business Review",
                    "Trainings(Product|Service|Technical|Soft Skills)",
                    "Overseas Meetings",
                    "Concall with Client or Partner Supplier/Vendor",
                    "Business Review",
                    "Travel Forum|Travel Seminar|Road Show",
                    "Sales Visit|Meeting with Clients",
                    "Joint Sales Call with partner suppliers",
                    "Escort|Familiarization Trips"
                };

                return showtemp;
            }
        }
        public List<string> TypeOfActivities
        {
            get
            {
                List<string> temp = new List<string>
                {
                   "A",
                   "B",
                   "C",
                   "D",
                   "E",
                   "F",
                   "G",
                   "H",
                   "I",
                   "J",
                   "K",
                   "L",
                   "M",
                };

                return temp;
            }
        }
        public List<int> Count { get; set; }
    }

    public class SalesReportModel
    {
        public Guid ID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ClientID { get; set; }
        public DateTime? Date { get; set; }
        public string ShowDate { get; set; }
        public string TypeOfActivity { get; set; }
        public string ShowTypeOfActivity
        {
            get
            {
                if (TypeOfActivity == "A")
                    return "Concall with BCD RAM or BCD GAM";
                else if (TypeOfActivity == "B")
                    return "Coordination|Management|Adhoc Meetings";
                else if (TypeOfActivity == "C")
                    return "Internal Training|Briefing with Servicing Team";
                else if (TypeOfActivity == "D")
                    return "Preparation for Travel Forums|Road Shows";
                else if (TypeOfActivity == "E")
                    return "Preparation for Proposal|Business Review";
                else if (TypeOfActivity == "F")
                    return "Trainings(Product|Service|Technical|Soft Skills)";
                else if (TypeOfActivity == "G")
                    return "Overseas Meetings";
                else if (TypeOfActivity == "H")
                    return "Concall with Client or Partner Supplier/Vendor";
                else if (TypeOfActivity == "I")
                    return "Business Review";
                else if (TypeOfActivity == "J")
                    return "Travel Forum|Travel Seminar|Road Show";
                else if (TypeOfActivity == "K")
                    return "Sales Visit|Meeting with Clients";
                else if (TypeOfActivity == "L")
                    return "Joint Sales Call with partner suppliers";
                else if (TypeOfActivity == "M")
                    return "Escort|Familiarization Trips";
                else
                    return "";
            }
        }
        public string SLA { get; set; }
        public string AgendaIssueConcerns { get; set; }
        public string ScopeAction { get; set; }
        public string CompanyUpdate { get; set; }
        public string Remarks { get; set; }
        public string BCDDeliverables { get; set; }
        public DateTime? DueDate { get; set; }
        public string ShowDueDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string ShowCategory
        {
            get
            {
                if (Category != "" || Category != null)
                    return "(" + Category + ")";
                else
                    return "";
            }
        }

        public string ShowStatus
        {
            get
            {
                if (Status != "" || Status != null)
                    return "(" + Status + ")";
                else
                    return "";
            }
        }

        //Foreign Fields
        public string AccountManager { get; set; }
        public string ClientName { get; set; }

        //HistoryFields
        public Guid? CreatedBy { get; set; }
        public string ShowCreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ShowCreatedDate
        {
            get
            {
                if (CreatedDate != null)
                    return CreatedDate.ToString();
                else
                    return "";
            }
        }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
        List<SalesReportFileModel> Files { get; set; }
    }

    public class SalesReportFileModel
    {
        public Guid ID { get; set; }
        public Guid? SalesReportID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        //HistoryFields
        public Guid? CreatedBy { get; set; }
        public string ShowCreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ShowCreatedDate
        {
            get
            {
                if (CreatedDate != null)
                    return CreatedDate.ToString();
                else
                    return "";
            }
        }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }
}