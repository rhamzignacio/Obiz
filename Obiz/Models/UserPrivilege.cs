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
    
    public partial class UserPrivilege
    {
        public System.Guid ID { get; set; }
        public string Module { get; set; }
        public System.Guid UserID { get; set; }
        public string Add { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string Override { get; set; }
        public string View { get; set; }
    }
}