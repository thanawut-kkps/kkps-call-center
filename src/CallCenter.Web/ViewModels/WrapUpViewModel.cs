using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Phatra.Core.Web.Constants;

namespace CallCenter.Web.ViewModels
{
    public class WrapUpViewModel
    {
        [Display(Name = "Note Date")]
        [DataType(DateTimeConst.Name)]
        [Required(ErrorMessage = "Note Date is required.")]
        public DateTime? WrapupDatetime { get; set; }
        [Display(Name = "Wrapup Text")]
        [Required(ErrorMessage = "Wrapup Text is required.")]
        public string WrapupText { get; set; }
        [Display(Name = "Account No")]
        [StringLength(15, ErrorMessage = "Account No must be under 15 characters.")]	
        public string WrapupAccountNo { get; set; }

        public string CallSession { get; set; }
        public string AgentId { get; set; }
        public string Extension { get; set; }
        public string CustomerPhone { get; set; }
        public string OriginalAccountNo { get; set; }
        public decimal? OriginalClientId { get; set; }
        public decimal? ClientId { get; set; }
        public string CallStartTime { get; set; }
        public decimal? PoaId { get; set; }

        [Display(Name = "Reason")]
        [Required(ErrorMessage = "Reason is required.")]
        public string ReasonDesc { get; set; }

        [Display(Name = "Call Answer")]
        public bool IsCallbackSuccess { get; set; }
    }
}