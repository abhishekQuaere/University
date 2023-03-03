using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class DesignofNewCurricula
    {
        public string Title { get; set; }
        public string Level { get; set; }
        public int Courses { get; set; }
        public string Authority { get; set; }

        public string UploadDesignofCurricula { get; set; }
        public List<DesignofNewCurricula> DesignCurriculaList { get; set; }

        public long UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}