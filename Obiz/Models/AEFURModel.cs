using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class AEFURModel
    {
        
    }

    public class AEFURUnbilledReport
    {
        public string TCName { get; set; }
        public int? Count { get; set; }
        public string Department { get; set; }
    }

    public class AEFURNoRecordModel
    {
        public string TicketNumber { get; set; }
        public string RecordLocator { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Date { get; set; }
        public string BookingAmount { get; set; }
        public string PassengerName { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class AEFURUnbilledModel
    {
        public DateTime? BookingDate { get; set; }
        public string TicketingAgent { get; set; }
        public decimal InvoiceID { get; set; }
        public decimal? InvoiceDetailID { get; set; }
        public string CurrencyCode { get; set; }

        public DateTime? TransactionDate { get; set; }
        public string ShowTransactionDate
        {
            get
            {
                if (TransactionDate != null)
                    return DateTime.Parse(TransactionDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

        public string VendorNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string TicketNumber { get; set; }
        public byte? TransactionType { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ProfileName { get; set; }
        public string BookingAgentNumber { get; set; }
        public string PassengerName { get; set; }
        public decimal? GrossAmount { get; set; }
        public string RecordLocator { get; set; }
        public string FullName { get; set; }
        public string FreeFieldA { get; set; }
        public string Department { get; set; }
        public string TicketingAgentNumber { get; set; }
        public string AirlineCode { get; set; }
        public string Itinerary { get; set; }
        public string Status { get; set; }
    }
}