using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class ResearchDegree
    {
        public long Id { get; set; }
        public string Degree { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public decimal Marks { get; set; }
        public int YearOfAward { get; set; }
        public string University { get; set; }
        public string DocumentPath { get; set; }
        public string PhdAwarded { get; set; }
        public string PhdCertificatePath { get; set; }
        public string IpAddress { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public IEnumerable<ResearchDegree> researchDegree { get; set; }
    }
}