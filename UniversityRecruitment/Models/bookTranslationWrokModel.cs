using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class bookTranslationWrokModel
    {

        public long userId { get; set; }
        public string ip { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string category { get; set; }
        public string bookTitle { get; set; }   
        public string Date { get; set; }
        public string ISSN { get; set; }
    
        public string lang { get; set; }
        public string nameofP { get; set; }
        public string bchapter { get; set; }
        public string noOftranslater { get; set; }
        public int wheatherUMainAutor { get; set; }
     
        public string attachment { get; set; }
     
        public List<bookTranslationWrokModel> lst { get; set; }
    }
}
