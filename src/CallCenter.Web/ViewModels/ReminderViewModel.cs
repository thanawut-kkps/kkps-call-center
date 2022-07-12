using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Phatra.Core.Web.Constants;

namespace CallCenter.Web.ViewModels
{
    public class ReminderViewModel
    {
        [Display(Name = "Reminder Id")]
        [Required(ErrorMessage = "Reminder Id is Required.")]
        public int? ReminderId { get; set; }

        [Display(Name = "Call Session")]
        [StringLength(128, ErrorMessage = "Call Session must be under 128 characters.")]
        public string CallSession { get; set; }

        [Display(Name = "Customer Phone")]
        [StringLength(128, ErrorMessage = "Customer Phone must be under 128 characters.")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Client Id")]
        public Nullable<decimal> ClientId { get; set; }

        [Display(Name = "Account No")]
        [StringLength(15, ErrorMessage = "Account No must be under 15 characters.")]
        public string AccountNo { get; set; }

        [DataType(DateTimeConst.Name)]
        [Display(Name = "Remind Date")]
        [Required(ErrorMessage = "Remind Date is required.")]
        public Nullable<System.DateTime> RemindDate { get; set; }

        [Display(Name = "Details")]
        [Required(ErrorMessage = "Details is required.")]
        public string Details { get; set; }

        [DataType(DateTimeConst.Name)]
        [Display(Name = "Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name = "Created User")]
        [StringLength(20, ErrorMessage = "Created User must be under 20 characters.")]
        public string CreatedUser { get; set; }

        [DataType(DateTimeConst.Name)]
        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Display(Name = "Updated User")]
        [StringLength(20, ErrorMessage = "Updated User must be under 20 characters.")]
        public string UpdatedUser { get; set; }

        [Display(Name = "Is Cancelled")]
        public Nullable<bool> IsCancelled { get; set; }

        [Display(Name = "Agent Id")]
        [StringLength(20, ErrorMessage = "Agent Id must be under 20 characters.")]
        public string AgentId { get; set; }
    }
}