using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CallCenter.Web.ViewModels
{
    public class VerificationViewModel
    {
        [Display(Name = "Purpose")]
        [Required(ErrorMessage = "Purpose is required.")]
        public int? PurposeId { get; set; }

    }
}