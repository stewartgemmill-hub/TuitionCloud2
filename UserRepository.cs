using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TutionCloudWeb.Model.Wordutopia;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Wordutopia
{
    public class UserRepository
    {
        TutionCloudEntities TutionCloudEntities = new TutionCloudEntities();
        public async Task<bool> VerifyEmail(UserModel model)
        {
            bool values = false;
            try
            {
                var results =  TutionCloudEntities.tUsers.Where(x => x.email == model.Email).FirstOrDefault();
                if (results != null)
                {
                    values = true;
                }
            }
            catch(Exception ex){

            }
            //var result = await UserRepository.VerifyEmail(model);
            return values;
        }

        //public async Task<bool> SaveVerifyEmailDB(UserModel model)
        //{
        //    bool values = false;
        //    try
        //    {
        //        var results = TutionCloudEntities.tUsers.Where(x => x.email == model.Email).FirstOrDefault();
        //        if (results != null)
        //        {
        //            values = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    //var result = await UserRepository.VerifyEmail(model);
        //    return values;
        //}
    }
}