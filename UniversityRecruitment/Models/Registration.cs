using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        [Required(ErrorMessage = "Please Enter Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please Enter Mother Name")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Please Enter Date of birth")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Please Enter Addhar Number")]
        public string AddharNo { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Select Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string PermanentAddress1 { get; set; }

        public string PermanentAddress2 { get; set; }

        [Required(ErrorMessage = "Please Select State")]
        public int PermanentStateId { get; set; }
        public string PermanentStateOther { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        public int PermanentCityId { get; set; }
        public string PermanentCityOther { get; set; }

        [Required(ErrorMessage = "Please Enter Pincode")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$&*]).{8,16})", ErrorMessage = "Your New Password must be of minimum 08 characters containing at least 01 Numeric, 01 Upper Case, 01 Lower Case & 01 Special Character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "New Password & Confirm Password must be same.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Captcha")]
        public string Captcha { get; set; }

        public string Ip { get; set; }

        public int ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string Name { get; set; }

    }

    public class EmailAndMobile
    {
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public int ApplicationId { get; set; }
    }

    public class Login
    {
        [Required(ErrorMessage = "Please Enter LoginId")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string DOB { get; set; }

        public string AddharNo { get; set; }

        public string Gender { get; set; }

        public string Category { get; set; }

        public string PermanentAddress1 { get; set; }

        public string PermanentAddress2 { get; set; }

        public int PermanentStateId { get; set; }

        public int PermanentCityId { get; set; }

        public string PinCode { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string IpAddress { get; set; }

        public int EmailVerified { get; set; }

        public int MobileVerified { get; set; }

    }
    public class changePasword
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public long userId { get; set; }

    }

    public class changePassRes
    {
        public int flag { get; set; }
        public string Password { get; set; }
        public string message { get; set; }
    }

}