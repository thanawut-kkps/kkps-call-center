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
    public partial class CallCenterGetWrapUpInfoResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Call Session")]
            [Required(ErrorMessage = "Call Session is Required.")]
            [StringLength(128, ErrorMessage = "Call Session must be under 128 characters.")]
            public string CallSession { get; set; }
            
            [Display(Name = "Agent Id")]
            [Required(ErrorMessage = "Agent Id is Required.")]
            [StringLength(128, ErrorMessage = "Agent Id must be under 128 characters.")]
            public string AgentId { get; set; }
            
            [Display(Name = "Customer Phone")]
            [StringLength(128, ErrorMessage = "Customer Phone must be under 128 characters.")]
            public string CustomerPhone { get; set; }
            
            [Display(Name = "Original Account No")]
            [StringLength(15, ErrorMessage = "Original Account No must be under 15 characters.")]
            public string OriginalAccountNo { get; set; }
            
            [Display(Name = "Original Client Id")]
            public Nullable<decimal> OriginalClientId { get; set; }
            
            [Display(Name = "Client Id")]
            public Nullable<decimal> ClientId { get; set; }
            
            [Display(Name = "Account No")]
            [StringLength(15, ErrorMessage = "Account No must be under 15 characters.")]
            public string AccountNo { get; set; }
            
            [Display(Name = "Call Starttime")]
            [Required(ErrorMessage = "Call Starttime is Required.")]
            public System.DateTime CallStarttime { get; set; }
            
            [Display(Name = "Wrapup Text")]
            public string WrapupText { get; set; }
            
            [Display(Name = "Wrapup Date Time")]
            public Nullable<System.DateTime> WrapupDateTime { get; set; }
            
            [Display(Name = "Poa Id")]
            public Nullable<decimal> PoaId { get; set; }

            #endregion
    	}

        #endregion
    }
}
