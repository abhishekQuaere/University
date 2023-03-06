using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Referee
    {
        public string refereeName { get; set; }
        public string address { get; set; }
        public string emailId { get; set; }
        public string Mobile { get; set; }
        public string landline { get; set; }
        public string Fax { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string IpAddress { get; set; }
        public long Id { get; set; }
        public IEnumerable<Referee> referee { get; set; }
        public bool Salary { get; set; }
        public string SalaryDetail { get; set; }
        public bool AcademicBreak { get; set; }
        public string AcademicBreakDetail { get; set; }
        public bool Law { get; set; }
        public string LawDetail { get; set; }
        public bool CaseFiled { get; set; }
        public string CaseDetail { get; set; }
        public string OtherInformation { get; set; }
        public string NocDocumentPath { get; set; }
        public string EwsDocumentPath { get; set; }
        public Acceptance acceptance { get; set; }
    }

    public class Acceptance
    {
        public long Id { get; set; }
        public bool Salary { get; set; }
        public string SalaryDetail { get; set; }
        public bool AcademicBreak { get; set; }
        public string AcademicBreakDetail { get; set; }
        public bool Law { get; set; }
        public string LawDetail { get; set; }
        public bool CaseFiled { get; set; }
        public string CaseDetail { get; set; }
        public string OtherInformation { get; set; }
        public string NocDocumentPath { get; set; }
        public string EwsDocumentPath { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string IpAddress { get; set; }
    }

    public class RefereeByIdModel
    {
        public long Id { get; set; }
    }

}