using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class SP_FETCH_USER_NOTELIST
    {
        private TutionCloudEntities _context = new TutionCloudEntities();
        private SP_FETCH_USER_NOTELIST_Result res;
        public SP_FETCH_USER_NOTELIST(SP_FETCH_USER_NOTELIST_Result obj) { res = obj; }
        public long NOTEID { get { return res.NOTEID; } }
        public System.Guid NOTEGUID { get { return res.NOTEGUID; } }
        public string TITLE { get { return res.TITLE; } }
        public string DESCRIPTION { get { return res.DESCRIPTION; } }
        public System.DateTime CREATEDDATE { get { return res.CREATEDDATE; } }
        public System.DateTime UPDATEDDATE { get { return res.UPDATEDDATE; } }
    }
}