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
    public partial class CallCenterGetMissedCallListResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Customer Phone")]
            [StringLength(128, ErrorMessage = "Customer Phone must be under 128 characters.")]
            public string CustomerPhone { get; set; }
            
            [Display(Name = "Priority")]
            public Nullable<int> Priority { get; set; }
            
            [Display(Name = "Customer Name")]
            [Required(ErrorMessage = "Customer Name is Required.")]
            [StringLength(221, ErrorMessage = "Customer Name must be under 221 characters.")]
            public string CustomerName { get; set; }
            
            [Display(Name = "Call Start Time")]
            public Nullable<System.DateTime> CallStartTime { get; set; }
            
            [Display(Name = "Client Id")]
            public Nullable<decimal> ClientId { get; set; }

            #endregion

    	}

        #endregion

    }
}
