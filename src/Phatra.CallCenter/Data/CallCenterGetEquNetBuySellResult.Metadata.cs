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
    public partial class CallCenterGetEquNetBuySellResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Account")]
            [Required(ErrorMessage = "Account is Required.")]
            [StringLength(15, ErrorMessage = "Account must be under 15 characters.")]
            public string Account { get; set; }
            
            [Display(Name = "Tradedate")]
            public Nullable<System.DateTime> Tradedate { get; set; }
            
            [Display(Name = "Settdate")]
            [Required(ErrorMessage = "Settdate is Required.")]
            public System.DateTime Settdate { get; set; }
            
            [Display(Name = "Net Desc")]
            [StringLength(50, ErrorMessage = "Net Desc must be under 50 characters.")]
            public string NetDesc { get; set; }
            
            [Display(Name = "Asset Type")]
            [StringLength(20, ErrorMessage = "Asset Type must be under 20 characters.")]
            public string AssetType { get; set; }
            
            [Display(Name = "Client Receive")]
            public Nullable<decimal> ClientReceive { get; set; }
            
            [Display(Name = "Client Pay")]
            public Nullable<decimal> ClientPay { get; set; }

            #endregion
    	}

        #endregion
    }
}
