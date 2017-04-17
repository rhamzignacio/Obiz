using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obiz.Models
{
    public class PriviledgeModel
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Add { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string Override { get; set; }
        public string View { get; set; }

        //public string ShowAdd
        //{
        //    get
        //    {
        //        if (Add)
        //            return "Y";
        //        else
        //            return "N";
        //    }
        //}

        //public string ShowEdit
        //{
        //    get
        //    {
        //        if (Edit)
        //            return "Y";
        //        else
        //            return "N";
        //    }
        //}

        //public string ShowDelete
        //{
        //    get
        //    {
        //        if (Delete)
        //            return "Y";
        //        else
        //            return "N";
        //    }
        //}
        //public string ShowOverride
        //{
        //    get
        //    {
        //        if (Override)
        //            return "Y";
        //        else
        //            return "N";
        //    }
        //}
    }
}