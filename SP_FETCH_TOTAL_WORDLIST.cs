using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class SP_FETCH_TOTAL_WORDLIST
    {
        private TutionCloudEntities _context = new TutionCloudEntities();
        private SP_FETCH_TOTAL_WORDLIST_Result res;
        public SP_FETCH_TOTAL_WORDLIST(SP_FETCH_TOTAL_WORDLIST_Result obj) { res = obj; }
        public long WORDID { get { return res.WORDID; } }
        public System.Guid WORDGUID { get { return res.WORDGUID; } }
        public string WORD { get { return res.WORD; } }
        public string PRONOUNCIATION { get { return res.PRONOUNCIATION; } }
        public string PHRASE { get { return res.PHRASE; } }
        public string HINT { get { return res.HINT; } }
        public string DEFINITION { get { return res.DEFINITION; } }
        public System.DateTime CREATEDDATE { get { return res.CREATEDDATE; } }
        public System.DateTime UPDATEDDATE { get { return res.UPDATEDDATE; } }
        public decimal OVERALLSCORE { get { return res.OVERALLSCORE ?? 0; } }
    }
}