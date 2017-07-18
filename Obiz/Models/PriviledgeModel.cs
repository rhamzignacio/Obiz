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
        public string Module { get; set; }

        private string _add;
        public string Add
        {
            get
            {
                if (_add == null)
                    return "N";
                else
                    return _add;
            }
            set
            {
                _add = value;
            }
        }

        private string _edit;
        public string Edit
        {
            get
            {
                if (_edit == null)
                    return "N";
                else
                    return _edit;
            }
            set
            {
                _edit = value;
            }
        }

        private string _delete;
        public string Delete
        {
            get
            {
                if (_delete == null)
                    return "N";
                else
                    return _delete;
            }
            set
            {
                _delete = value;
            }
        }

        private string _override;
        public string Override
        {
            get
            {
                if (_override == null)
                    return "N";
                else
                    return _override;
            }
            set
            {
                _override = value;
            }
        }

        private string _view;
        public string View
        {
            get
            {
                if (_view == null)
                    return "N";
                else
                    return _view;
            }
            set
            {
                _view = value;
            }
        }
    }
}