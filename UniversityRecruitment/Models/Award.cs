using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Award
    {
        public string Name { get; set; }
        public string DateOfAward { get; set; }
        public string Organization { get; set; }
        public string Level { get; set; }
        public string UploadAward { get; set; }
        public int score { get; set; }
         
        
        public List<Award> AwardList { get; set; }
         
        public long UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}