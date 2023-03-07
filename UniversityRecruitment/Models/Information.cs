using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Information
    {
        public string Detail { get; set; }
        
        public string UploadInformation { get; set; }
        public int serial { get; set; }


        public List<Information> InformationList { get; set; }

        public long UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}