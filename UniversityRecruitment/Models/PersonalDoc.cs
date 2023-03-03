using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class PersonalDoc
    {
        public int ID { get; set; }
        [Required]
        public  string Signature { get; set; }
        [Required]
        public string Photo { get; set; }
        public string message { get; set; }
        public int ResponseCode { get; set; }
    }
}