using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Personalinfo
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string DOB { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string Adhar { get; set; }
        public string AdharImage { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; }
        public string MAddress1 { get; set; }
        public string MAddress2 { get; set; }
        public int MStateId { get; set; }
        public int MCityId { get; set; }
        public string MPincode { get; set; }
        public string Mobile { get; set; }
        public string AlternateMobile { get; set; }
        public string Email { get; set; }
        public string otherState { get; set; }
        public string otherCity { get; set; }
        public string SotherState { get; set; }
        public string SotherCity { get; set; }
        public bool SameMailingAddress { get; set; }
        public int ResponseCode { get; set; }

    }
}