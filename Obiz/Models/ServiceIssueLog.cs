//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Obiz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceIssueLog
    {
        public System.Guid ID { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public Nullable<System.DateTime> IncidentDate { get; set; }
        public Nullable<System.Guid> ReportedBy { get; set; }
        public Nullable<System.Guid> ClientID { get; set; }
        public string TotalPax { get; set; }
        public string PaxName { get; set; }
        public Nullable<System.Guid> HandlingTCID { get; set; }
        public string Department { get; set; }
        public string SummaryNatureOfServiceIssue { get; set; }
        public string Resolution { get; set; }
        public Nullable<System.DateTime> ResolveDate { get; set; }
        public string WasIRIssued { get; set; }
        public string WasMemoIssued { get; set; }
        public string BookerName { get; set; }
    }
}