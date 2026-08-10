using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TutionCloudWeb.Model.Quester;
using TutionCloudWeb.Models.Database;
using TutionCloudWeb.Models.Repository;

namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_Login_Repository : Quester_BaseRepository
    {
        internal Quester_ReturnMessage_Model Quester_register(Quester_Register model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {

                if (_context.Quester_User.Any(z => z.Username == model.username && z.IsActive && z.Webname == model.webname))
                {
                    returnModel.status = false;
                    returnModel.message = "Username already taken";
                }
                else
                {
                    if (_context.Quester_User.Any(z => z.email == model.Email))
                    {
                        returnModel.status = false;
                        returnModel.message = "Email id already exists";
                    }
                    else
                    {
                        var currentTime = System.DateTime.UtcNow;
                        var user = _context.Quester_User.Create();
                        user.AccountCreatedDate = currentTime;
                        user.Forename = model.forename ?? string.Empty;
                        user.IsActive = true;
                        user.IsSuspended = false;
                        user.Password = model.password;         //StringCipher.Encrypt(model.password, salt);
                        user.ProfileImage = string.Empty;
                        user.ProfileLastEditedDate = currentTime;
                        user.PushStatus = true;
                        user.Surname = model.surname ?? string.Empty;
                        user.UserGuid = Guid.NewGuid();
                        user.Username = model.username;
                        user.Webname = model.webname;
                        user.email = model.Email.Trim(); //Basheer on 05/03/2019
                        _context.Quester_User.Add(user);
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";

                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                    }
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }


        internal Quester_LoginReturn_Model Quester_login(Quester_Login_Model model)
        {
            var returnModel = new Quester_LoginReturn_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {                                
                var user = _context.Quester_User.FirstOrDefault(z => z.Username == model.username && z.Password == model.password && z.IsActive && z.IsSuspended == false && z.Webname == model.webname);
               
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                    returnModel.status = true;
                    returnModel.message = "success";
                    returnModel.userid = user.UserId;                    
                    Quester_SessionModel_Model.UserId = user.UserId;
                   //jibin on 10/9/2020
                    if(user.Islogged==null)
                    {
                        var uid = Convert.ToString(user.UserId);
                        var x = _context.UpdateQuesterUserByUserId(uid);
                        _context.SaveChanges();
                        returnModel.Islogged = false;
                    }
                    else
                    {
                        returnModel.Islogged = true;
                    }

                    //jibin on 10/9/2020
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid username /  password";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }



        internal Quester_User Quester_login_FullDetails(Quester_Login_Model model)
        {
            var returnModel = new Quester_LoginReturn_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.Username == model.username && z.Password == model.password && z.IsActive && z.IsSuspended == false && z.Webname == model.webname);
               
               
                return user;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }



    }
}