using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class ResearchPaper
    {
        public int ID { get; set; }
        public string PublicationTitle { get; set; }
        public string PublicationDate { get; set; }
        public string Journal { get; set; }
        public string ISBN { get; set; }
        public bool PeerReviewed { get; set; }
        public decimal ImpactFactor { get; set; }
        public int NoOfAuther { get; set; }
        public bool IsMainAuthor { get; set; }
        public bool ReferredJournal { get; set; }
        public bool IsUGClist { get; set; }
        public bool JournalIndex { get; set; }
        public string UGCSerialNo { get; set; }
        public string UploadAttachment { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public string RootPath { get; set; }
        public string DocumentPath { get; set; }
        public int ResponseCode { get; set; }
        public List<ResearchPaper> ResearchPaperList { get; set; }

    }
}