using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class TeachingPedgogyModel
    {
        public long userId { get; set; }
        public string tTitle { get; set; }
        public string description { get; set; }
        public string Pedagogy { get; set; }
        public string level { get; set; }
        public string cECApproval { get; set; }
        public string attachment { get; set; }
        public string ip { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<TeachingPedgogyModel> lst { get; set; }
    }
}