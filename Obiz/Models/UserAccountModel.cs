using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class UserAccountModel
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string AgentCode1 { get; set; }
        public string AgentCode2 { get; set; }
        public string AgentCode3 { get; set; }
        public string PartnerAgent { get; set; }
        public string CebBizPartner { get; set; }
        public string DepartmentHead { get; set; }
        public string ClientMagicLogin { get; set; }

        public PriviledgeModel AMActivityReport { get; set; }
        public PriviledgeModel AEFUR { get; set; }
        public PriviledgeModel Report { get; set; }
        public PriviledgeModel ActivityReportSummary { get; set; }
        public PriviledgeModel TopClients { get; set; }
        public PriviledgeModel PercentageActivity { get; set; }

        public string SessionID { get; set; }
    }

}