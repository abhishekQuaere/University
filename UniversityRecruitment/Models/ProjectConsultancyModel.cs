using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class ProjectConsultancyModel
    {
        public long UserId { get; set; }
        public string titleOfPrfoject { get; set; }
        public string agencyFund { get; set; }
        public string dateOfSan { get; set; }
        public string grant_Amount { get; set; }
        public string project_Type { get; set; }
        public string roleInProject { get; set; }
        public string projectStatus { get; set; }
        public string attachment { get; set; }
        public string ip { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<ProjectConsultancyModel> lst { get; set; }
    }
}