using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class BookPublication
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string Publisher { get; set; }
        public string PublisherType { get; set; }
        public string Isbn { get; set; }
        public int NoOfCoAuthors { get; set; }
        public bool IsMainAuthor { get; set; }
        public string DocumentPath { get; set; }
        public string NameofBookEditor { get; set; }
        public string NoOfCoTranslator { get; set; }
        public string IpAddress { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public IEnumerable<BookPublication> bookPublications { get; set; }
    }
}