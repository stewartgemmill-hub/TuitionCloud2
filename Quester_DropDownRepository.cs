using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutionCloudWeb.Model.Quester;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_DropDownRepository 
    {
        protected static TutionCloudEntities _context = new TutionCloudEntities();

        public static List<SelectListItem> Quester_Category_UserBase(long userId)
        {
            var input = _context.Quester_Category.Where(x => x.IsActive == true && x.UserId == userId).OrderBy(x => x.CategoryName).ToList();
            return input.Select(x => new SelectListItem { Text = x.CategoryName, Value =  x.CategoryId.ToString() }).ToList();
        }

        public static List<SelectListItem> Quester_Selected_Folder(long userId)
        {
            try
            {
                string jsondata = _context.Quester_TreeView.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.TreeData).FirstOrDefault(); ;
                List<Quester_TreeView_Model> DeserializeData = JsonConvert.DeserializeObject<List<Quester_TreeView_Model>>(jsondata);

                var input = DeserializeData.Where(x => x.isParent == true).ToList();


                return input.Select(x => new SelectListItem { Text = x.name, Value = x.id }).ToList();
            }
            catch
            {
                return null;
            }
            
        }

    }
}