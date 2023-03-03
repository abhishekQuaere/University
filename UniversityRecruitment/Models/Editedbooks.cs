using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class Editedbooks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string Category { get; set; }
        public string PublisherName { get; set; }
        public string publisherType { get; set; }
        public string ISBNNo { get; set; }
        public int NoOfCoAuther { get; set; }
        public bool IsMainAuther { get; set; }
        public string UploadAttachment { get; set; }
        public string Ipaddress { get; set; }
        public string ResponseMessage { get; set; }
        public string RootPath { get; set; }
        public string DocumentPath { get; set; }
        public int ResponseCode { get; set; }
        public List<Editedbooks> EditedBookList { get; set; }

    }
}