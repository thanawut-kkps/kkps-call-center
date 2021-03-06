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
    public partial class CallCenterGetQuestionVerificationResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Purpose Id")]
            [Required(ErrorMessage = "Purpose Id is Required.")]
            public int PurposeId { get; set; }
            
            [Display(Name = "Question Id")]
            [Required(ErrorMessage = "Question Id is Required.")]
            public int QuestionId { get; set; }
            
            [Display(Name = "Seqno")]
            [Required(ErrorMessage = "Seqno is Required.")]
            public int Seqno { get; set; }
            
            [Display(Name = "Question Detail")]
            [StringLength(1024, ErrorMessage = "Question Detail must be under 1024 characters.")]
            public string QuestionDetail { get; set; }
            
            [Display(Name = "Map To Html")]
            public string MapToHtml { get; set; }
            
            [Display(Name = "Must Ask")]
            [Required(ErrorMessage = "Must Ask is Required.")]
            public bool MustAsk { get; set; }
            
            [Display(Name = "Is Poa Question")]
            [StringLength(1, ErrorMessage = "Is Poa Question must be under 1 characters.")]
            public string IsPoaQuestion { get; set; }

            #endregion

    	}

        #endregion

    }
}
