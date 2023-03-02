using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Experience
    {
        public string Designation { get; set; }
        public string GradeAgp { get; set; }
        public string NatureofPost { get; set; }
        public string NameAddress { get; set; }
        public string DesignationPeriodFrom { get; set; }
        public string DesignationPeriodTo { get; set; }
        public string DesignationImage { get; set; }
        public string Agency { get; set; }
        public string HostInstitution { get; set; }
        public string AgencyPeriodFrom { get; set; }
        public string AgencyPeriodTo { get; set; }
        public string AgencyImage { get; set; }
        public List<Experience> Designationlist { get; set; }
        public List<Experience> Agencylist { get; set; }
        public long id { get; set; }
    }
}