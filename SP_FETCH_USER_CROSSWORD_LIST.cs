using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class SP_FETCH_USER_CROSSWORD_LIST
    {
          private TutionCloudEntities _context = new TutionCloudEntities();
          private SP_FETCH_USER_CROSSWORD_LIST_Result res;
          public SP_FETCH_USER_CROSSWORD_LIST(SP_FETCH_USER_CROSSWORD_LIST_Result obj) { res = obj; }
          public long CrosswordId { get { return res.CrosswordId; } }
          public long USERID { get { return res.USERID; } }
          public System.DateTime CREATEDTIME { get { return res.CREATEDTIME; } }
          public bool ISDELETED { get { return res.ISDELETED; } }
          public string HTML { get { return res.HTML; } }
          public bool ISCOMPLETED { get { return res.ISCOMPLETED; } }
    }
}