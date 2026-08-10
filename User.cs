using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class User
    {
        public class Register
        {
            [Required(ErrorMessage = "required")]
            public string forename { get; set; }

            [Required(ErrorMessage = "required")]
            public string surname { get; set; }

            [Required(ErrorMessage = "required")]
            public string username { get; set; }

            [Required(ErrorMessage = "required")]
            [StringLength(255, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
            [DataType(DataType.Password)]
            public string password { get; set; }

            [Required(ErrorMessage = "required")]
            [StringLength(255, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Compare("password")]
            public string confirmPassword { get; set; }
            public string webname { get; set; }

            [Display(Name = "Email address")]
            [Required(ErrorMessage = "The email address is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
        }

        public class RegisterReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class Login
        {
            [Required(ErrorMessage = "required")]
            public string username { get; set; }

            [Required(ErrorMessage = "required")]
            [DataType(DataType.Password)]
            public string password { get; set; }
            public string webname { get; set; }
        }
        public class LoginReturn
        {
            public bool status { get; set; }
            public string message { get; set; }

            //Basheer on 04/01/2019 to change home page
            public long userid { get; set; }
        }
        public class Forgotpassword
        {
            public long userid { get; set; }

            [Display(Name = "Email address")]
            [Required(ErrorMessage = "The email address is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string reciever { get; set; }
            public string message { get; set; }
            public string guid { get; set; }
            public Guid queryguid { get; set; }

            [Required(ErrorMessage = "required")]
            [StringLength(255, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
            [DataType(DataType.Password)]
            public string newpassword { get; set; }

            [Required(ErrorMessage = "required")]
            [StringLength(255, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Compare("newpassword")]
            public string confirmpassword { get; set; }

            public string webname { get; set; }//jibin 11/19/2020


        }
        public class ForgotpasswordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

    }
}