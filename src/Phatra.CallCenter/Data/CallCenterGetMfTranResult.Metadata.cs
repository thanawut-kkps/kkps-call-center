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
    public partial class CallCenterGetMfTranResult
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
            
            [Display(Name = "Holder Id")]
            [StringLength(30, ErrorMessage = "Holder Id must be under 30 characters.")]
            public string HolderId { get; set; }
            
            [Display(Name = "Ref No")]
            [StringLength(12, ErrorMessage = "Ref No must be under 12 characters.")]
            public string RefNo { get; set; }
            
            [Display(Name = "Amc Code")]
            [StringLength(15, ErrorMessage = "Amc Code must be under 15 characters.")]
            public string AmcCode { get; set; }
            
            [Display(Name = "Fund Code")]
            [StringLength(30, ErrorMessage = "Fund Code must be under 30 characters.")]
            public string FundCode { get; set; }
            
            [Display(Name = "Trantype Code")]
            [StringLength(20, ErrorMessage = "Trantype Code must be under 20 characters.")]
            public string TrantypeCode { get; set; }
            
            [Display(Name = "Tradedate")]
            public Nullable<System.DateTime> Tradedate { get; set; }
            
            [Display(Name = "Amount Unit")]
            public Nullable<decimal> AmountUnit { get; set; }
            
            [Display(Name = "Amount Baht")]
            public Nullable<decimal> AmountBaht { get; set; }
            
            [Display(Name = "Tran Status Id")]
            public Nullable<int> TranStatusId { get; set; }
            
            [Display(Name = "Tran Status Desc")]
            [StringLength(24, ErrorMessage = "Tran Status Desc must be under 24 characters.")]
            public string TranStatusDesc { get; set; }

            #endregion

    	}

        #endregion

    }
}
