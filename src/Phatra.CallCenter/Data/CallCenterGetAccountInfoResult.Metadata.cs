//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phatra.CallCenter.Data
{
    [MetadataType(typeof(Metadata))] 
    public partial class CallCenterGetAccountInfoResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Account No")]
            [StringLength(20, ErrorMessage = "Account No must be under 20 characters.")]
            public string AccountNo { get; set; }
            
            [Display(Name = "Account Type")]
            [StringLength(20, ErrorMessage = "Account Type must be under 20 characters.")]
            public string AccountType { get; set; }
            
            [Display(Name = "Joint Account")]
            [StringLength(1, ErrorMessage = "Joint Account must be under 1 characters.")]
            public string JointAccount { get; set; }
            
            [Display(Name = "Account Name T")]
            [StringLength(250, ErrorMessage = "Account Name T must be under 250 characters.")]
            public string AccountNameT { get; set; }
            
            [Display(Name = "Account Name E")]
            [StringLength(250, ErrorMessage = "Account Name E must be under 250 characters.")]
            public string AccountNameE { get; set; }
            
            [Display(Name = "Fcname T")]
            [StringLength(250, ErrorMessage = "Fcname T must be under 250 characters.")]
            public string FcnameT { get; set; }
            
            [Display(Name = "Fcname E")]
            [StringLength(250, ErrorMessage = "Fcname E must be under 250 characters.")]
            public string FcnameE { get; set; }
            
            [Display(Name = "Fc Offtel")]
            [StringLength(100, ErrorMessage = "Fc Offtel must be under 100 characters.")]
            public string FcOfftel { get; set; }
            
            [Display(Name = "Email Edoc")]
            [StringLength(400, ErrorMessage = "Email Edoc must be under 400 characters.")]
            public string EmailEdoc { get; set; }
            
            [Display(Name = "Fax Confirm")]
            [StringLength(100, ErrorMessage = "Fax Confirm must be under 100 characters.")]
            public string FaxConfirm { get; set; }
            
            [Display(Name = "Pid No")]
            [StringLength(20, ErrorMessage = "Pid No must be under 20 characters.")]
            public string PidNo { get; set; }
            
            [Display(Name = "Pid Type Desc")]
            [StringLength(100, ErrorMessage = "Pid Type Desc must be under 100 characters.")]
            public string PidTypeDesc { get; set; }
            
            [Display(Name = "Remark")]
            [StringLength(600, ErrorMessage = "Remark must be under 600 characters.")]
            public string Remark { get; set; }
            
            [Display(Name = "Email")]
            [StringLength(200, ErrorMessage = "Email must be under 200 characters.")]
            public string Email { get; set; }
            
            [Display(Name = "Open Date")]
            public Nullable<System.DateTime> OpenDate { get; set; }

            #endregion

    	}

        #endregion

    }
}
