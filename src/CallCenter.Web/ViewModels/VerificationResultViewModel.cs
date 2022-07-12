using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Web.ViewModels
{
    public class VerificationResultViewModel
    {
        public int PurposeId { get; set; }
        public int QuestionId { get; set; }
        public string Result { get; set; } 
    }
}