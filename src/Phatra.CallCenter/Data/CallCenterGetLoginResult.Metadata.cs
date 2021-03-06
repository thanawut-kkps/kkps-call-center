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
    public partial class CallCenterGetLoginResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "User Name")]
            [StringLength(256, ErrorMessage = "User Name must be under 256 characters.")]
            public string UserName { get; set; }
            
            [Display(Name = "Pin Status")]
            public string PinStatus { get; set; }
            
            [Display(Name = "Login Status")]
            [StringLength(50, ErrorMessage = "Login Status must be under 50 characters.")]
            public string LoginStatus { get; set; }

            #endregion

    	}

        #endregion

    }
}
