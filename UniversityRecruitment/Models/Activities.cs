using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Activities
    {
        public string ActivityId { get; set; }

        public string Description { get; set; }
        public string Activity { get; set; }
        public string Institution { get; set; }
        public int AcademicYear { get; set; }
        public string UploadActivity { get; set; }


        public List<Activities>  AList { get; set; }

        public long UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}