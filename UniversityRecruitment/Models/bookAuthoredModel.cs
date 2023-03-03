using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class bookAuthoredModel
    {

        public long userId { get; set; }
        public string category { get; set; }
        public string bookTitle { get; set; }
        public string Date { get; set; }
       
        public string nameOfPublisher { get; set; }
        public string publisherType { get; set; }
        public string ISSN { get; set; }
        public string NoOfCoAuthor { get; set; }
        public string wheatherUMainAutor { get; set; }
        public string attachment { get; set; }
        public string ip { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<bookAuthoredModel> lst { get; set; }
    }
}