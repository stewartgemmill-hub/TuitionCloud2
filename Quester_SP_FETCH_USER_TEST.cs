using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class Quester_SP_FETCH_USER_TEST
    {
        private TutionCloudEntities _context = new TutionCloudEntities();
        private Quester_SP_FETCH_USER_TEST_Result res;
        public Quester_SP_FETCH_USER_TEST(Quester_SP_FETCH_USER_TEST_Result obj) { res = obj; }
        public long EXAMID { get { return res.EXAMID; } }
        public string TITLE { get { return res.TITLE; } }
        public System.DateTime CREATEDDATE { get { return res.CREATEDDATE; } }
        public decimal SCORE { get { return res.SCORE ?? 0; } }
    }
}