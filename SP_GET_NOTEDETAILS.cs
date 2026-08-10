using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class SP_GET_NOTEDETAILS
    {
        private TutionCloudEntities _context = new TutionCloudEntities();
        private SP_GET_NOTEDETAILS_Result res;
        public SP_GET_NOTEDETAILS(SP_GET_NOTEDETAILS_Result obj) { res = obj; }

        public string TITLE { get { return res.TITLE; } }
        public string DETAILS { get { return res.DETAILS; } }

    }
}