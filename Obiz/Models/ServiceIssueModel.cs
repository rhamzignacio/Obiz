using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class ServiceIssueModel
    {
        public Guid ID { get; set; }
        public DateTime? ReportDate { get; set; }
        public string ShowReportDate
        {
            get
            {
                if (ReportDate != null)
                    return ReportDate.ToString();
                else
                    return "";
            }
        }

        public DateTime? IncidentDate { get; set; }
        public string ShowIncidentDate
        {
            get
            {
                if (IncidentDate != null)
                    return DateTime.Parse(IncidentDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

        public Guid? ReportedBy { get; set; }
        public string ReportedByName { get; set; }
        public Guid? ClientID { get; set; }
        public string ClientName { get; set; }
        public string BookerName { get; set; }
        public string TotalPax { get; set; }
        public string PaxName { get; set; }
        public Guid? HandlingTCID { get; set; }
        public string HandlingTCName { get; set; }
        public string Department { get; set; }
        public string SummaryNatureOfService { get; set; }
        public string Resolution { get; set; }
        public DateTime? ResolveDate { get; set; }
        public string ShowResolveDate
        {
            get
            {
                if (ResolveDate != null)
                    return DateTime.Parse(ResolveDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
        public string WasIRIssued { get; set; }
        public string WasMemoIssued { get; set; }
    }
}