using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class DropDownModel
    {
    }

    public class AccountManagerDDModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
    }

    public class ClientDDModel
    {
        public Guid ID { get; set; }
        public string ClientName { get; set; }
        public string Status { get; set; }
    }
}