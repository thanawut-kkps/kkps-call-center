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
    public partial class CallCenterGetSearchClientResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Client Id")]
            public Nullable<decimal> ClientId { get; set; }
            
            [Display(Name = "Account No")]
            [StringLength(20, ErrorMessage = "Account No must be under 20 characters.")]
            public string AccountNo { get; set; }
            
            [Display(Name = "Product")]
            [StringLength(20, ErrorMessage = "Product must be under 20 characters.")]
            public string Product { get; set; }
            
            [Display(Name = "System Code")]
            [StringLength(20, ErrorMessage = "System Code must be under 20 characters.")]
            public string SystemCode { get; set; }
            
            [Display(Name = "Joint Account")]
            [StringLength(1, ErrorMessage = "Joint Account must be under 1 characters.")]
            public string JointAccount { get; set; }
            
            [Display(Name = "Account Name T")]
            [StringLength(250, ErrorMessage = "Account Name T must be under 250 characters.")]
            public string AccountNameT { get; set; }
            
            [Display(Name = "Account Name E")]
            [StringLength(250, ErrorMessage = "Account Name E must be under 250 characters.")]
            public string AccountNameE { get; set; }
            
            [Display(Name = "Trader")]
            [StringLength(452, ErrorMessage = "Trader must be under 452 characters.")]
            public string Trader { get; set; }
            
            [Display(Name = "Pid No")]
            [StringLength(20, ErrorMessage = "Pid No must be under 20 characters.")]
            public string PidNo { get; set; }

            #endregion
    	}

        #endregion
    }
}
