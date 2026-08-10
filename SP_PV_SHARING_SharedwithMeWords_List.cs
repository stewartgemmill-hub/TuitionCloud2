using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class SP_PV_SHARING_SharedwithMeWords_List
    {
        //private TutionCloudEntities _context = new TutionCloudEntities();

        //private SP_PV_SHARING_SharedwithMeWords_List_Result res;
       
        //public SP_PV_SHARING_SharedwithMeWords_List(SP_PV_SHARING_SharedwithMeWords_List_Result obj) { res = obj; }

       

        public string WORDID { get; set; }
        public Nullable<System.Guid> WORDGUID { get; set; }
        public string WORD { get; set; }
        public string PRONOUNCIATION { get; set; }
        public string PHRASE { get; set; }
        public string HINT { get; set; }
        public string DEFINITION { get; set; }
        public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public Nullable<decimal> OVERALLSCORE { get; set; }
    }
}