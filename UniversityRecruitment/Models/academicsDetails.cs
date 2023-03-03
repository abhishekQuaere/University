using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{

    public class academicQualification
    {
        //public int id { get; set; }
        public string qualification { get; set; }
        public string nameOfCourse { get; set; }
        public string specialization { get; set; }
        public string nameofBoard { get; set; }
        public string yearPassed { get; set; }
        public decimal cgpa { get; set; }
        public string divison { get; set; }
        public decimal perMarks { get; set; }
        public string subjectStudied { get; set; }
        public string attachment { get; set; }



    }

    public class ugcDetails
    {
        public string exam { get; set; }
        public string subject { get; set; }
        public string rollno { get; set; }
        public int year { get; set; }
        public string uDocument { get; set; }
        public List<ugcDetails> lst1 { get; set; }
    }

    public class academicsDetails
    {
        public string ip { get; set; }
        public long UserId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<academicQualification> lst { get; set; }
        public List<ugcDetails> lst1 { get; set; }

    }
}