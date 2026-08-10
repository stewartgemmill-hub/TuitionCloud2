using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_BaseRepository
    {
        protected TutionCloudEntities _context = new TutionCloudEntities();
        protected string salt = System.Configuration.ConfigurationManager.AppSettings["salt"];

        public void Quester_Exception_Save(string Message, string ClassName, string FunctionName)
        {
            try
            {
                var data = _context.Quester_Exception.Create();
                data.Message = Message;
                data.ClassName = ClassName;
                data.FunctionName = FunctionName;
                data.IsActive = true;
                data.CreateDate = DateTime.Now;
                _context.Quester_Exception.Add(data);
                _context.SaveChanges();

            }
            catch(Exception ex)
            {

            }
        }


    }
}