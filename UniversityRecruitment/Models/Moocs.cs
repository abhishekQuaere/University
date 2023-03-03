using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Moocs
    {
        
        public string LevelofMoocs { get; set; }
        public string NoOfModule { get; set; }
        public string Description { get; set; }
        public string NoOfCredit { get; set; }
         
        public string UploadMoocs { get; set; } 
        public List<Moocs> MoocsList { get; set; }

        public long UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}