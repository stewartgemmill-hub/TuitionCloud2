using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class Quester_SP_GET_NOTEDETAILS
    {
        private TutionCloudEntities _context = new TutionCloudEntities();
        private Quester_SP_GET_NOTEDETAILS_Result res;
        public Quester_SP_GET_NOTEDETAILS(Quester_SP_GET_NOTEDETAILS_Result obj) { res = obj; }

        public string TITLE { get { return res.TITLE; } }
        public string DETAILS { get { return res.DETAILS; }  }

    }
}