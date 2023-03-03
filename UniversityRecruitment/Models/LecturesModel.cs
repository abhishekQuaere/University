using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class LecturesModel
    {
        public int id { get; set; }
        public string titleLectures { get; set; }
        public string programDate { get; set; }
        public string type { get; set; }
        public string level { get; set; }
        public string organizingBody { get; set; }
        public string attachment { get; set; }
        public string ip { get; set; }
        public long UserId { get; set; }

        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<LecturesModel> lst { get; set; }
    }
}