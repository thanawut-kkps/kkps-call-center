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
    public partial class CallCenterGetEdividendResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Account No")]
            [StringLength(15, ErrorMessage = "Account No must be under 15 characters.")]
            public string AccountNo { get; set; }
            
            [Display(Name = "Dvd Bank Shortname")]
            [StringLength(10, ErrorMessage = "Dvd Bank Shortname must be under 10 characters.")]
            public string DvdBankShortname { get; set; }
            
            [Display(Name = "Dvd Bank Account No")]
            [StringLength(50, ErrorMessage = "Dvd Bank Account No must be under 50 characters.")]
            public string DvdBankAccountNo { get; set; }
            
            [Display(Name = "Dbd Bank Account Type Desc")]
            [StringLength(50, ErrorMessage = "Dbd Bank Account Type Desc must be under 50 characters.")]
            public string DbdBankAccountTypeDesc { get; set; }
            
            [Display(Name = "Dvd Branch Name")]
            [StringLength(100, ErrorMessage = "Dvd Branch Name must be under 100 characters.")]
            public string DvdBranchName { get; set; }

            #endregion

    	}

        #endregion

    }
}
