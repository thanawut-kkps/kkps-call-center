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
    public partial class CallCenterGetProfileResult
    {
        #region Metadata
    
    	/// <summary>
    	/// Metadata internal class used for Data Annotations
    	/// </summary>
    	internal class Metadata
        {
            #region Primitive Properties
            
            [Display(Name = "Pid No")]
            [StringLength(20, ErrorMessage = "Pid No must be under 20 characters.")]
            public string PidNo { get; set; }
            
            [Display(Name = "Pid Type Desc")]
            [StringLength(100, ErrorMessage = "Pid Type Desc must be under 100 characters.")]
            public string PidTypeDesc { get; set; }
            
            [Display(Name = "Client Name T")]
            [StringLength(250, ErrorMessage = "Client Name T must be under 250 characters.")]
            public string ClientNameT { get; set; }
            
            [Display(Name = "Client Name E")]
            [StringLength(250, ErrorMessage = "Client Name E must be under 250 characters.")]
            public string ClientNameE { get; set; }
            
            [Display(Name = "Birth Date")]
            public Nullable<System.DateTime> BirthDate { get; set; }
            
            [Display(Name = "Nation Code")]
            [StringLength(10, ErrorMessage = "Nation Code must be under 10 characters.")]
            public string NationCode { get; set; }
            
            [Display(Name = "Nation Desc")]
            [StringLength(100, ErrorMessage = "Nation Desc must be under 100 characters.")]
            public string NationDesc { get; set; }
            
            [Display(Name = "Tel Home")]
            [StringLength(100, ErrorMessage = "Tel Home must be under 100 characters.")]
            public string TelHome { get; set; }
            
            [Display(Name = "Tel Office")]
            [StringLength(100, ErrorMessage = "Tel Office must be under 100 characters.")]
            public string TelOffice { get; set; }
            
            [Display(Name = "Mobile")]
            [StringLength(100, ErrorMessage = "Mobile must be under 100 characters.")]
            public string Mobile { get; set; }
            
            [Display(Name = "Fatca Status")]
            [StringLength(100, ErrorMessage = "Fatca Status must be under 100 characters.")]
            public string FatcaStatus { get; set; }
            
            [Display(Name = "Addr Mail")]
            [StringLength(400, ErrorMessage = "Addr Mail must be under 400 characters.")]
            public string AddrMail { get; set; }
            
            [Display(Name = "Contact Name T")]
            [StringLength(200, ErrorMessage = "Contact Name T must be under 200 characters.")]
            public string ContactNameT { get; set; }
            
            [Display(Name = "Contact Name E")]
            [StringLength(200, ErrorMessage = "Contact Name E must be under 200 characters.")]
            public string ContactNameE { get; set; }
            
            [Display(Name = "Contact Relationship")]
            [StringLength(250, ErrorMessage = "Contact Relationship must be under 250 characters.")]
            public string ContactRelationship { get; set; }
            
            [Display(Name = "Contact Tel Home")]
            [StringLength(100, ErrorMessage = "Contact Tel Home must be under 100 characters.")]
            public string ContactTelHome { get; set; }
            
            [Display(Name = "Contact Tel Office")]
            [StringLength(100, ErrorMessage = "Contact Tel Office must be under 100 characters.")]
            public string ContactTelOffice { get; set; }
            
            [Display(Name = "Contact Mobile")]
            [StringLength(100, ErrorMessage = "Contact Mobile must be under 100 characters.")]
            public string ContactMobile { get; set; }
            
            [Display(Name = "Master Poa")]
            [StringLength(1, ErrorMessage = "Master Poa must be under 1 characters.")]
            public string MasterPoa { get; set; }
            
            [Display(Name = "Card Expire Within")]
            [StringLength(20, ErrorMessage = "Card Expire Within must be under 20 characters.")]
            public string CardExpireWithin { get; set; }
            
            [Display(Name = "FATCAType Desc")]
            [StringLength(400, ErrorMessage = "FATCAType Desc must be under 400 characters.")]
            public string FATCATypeDesc { get; set; }
            
            [Display(Name = "Card Expire Date")]
            public Nullable<System.DateTime> CardExpireDate { get; set; }
            
            [Display(Name = "Card Expire Info")]
            [StringLength(20, ErrorMessage = "Card Expire Info must be under 20 characters.")]
            public string CardExpireInfo { get; set; }
            
            [Display(Name = "Addr Register")]
            [StringLength(400, ErrorMessage = "Addr Register must be under 400 characters.")]
            public string AddrRegister { get; set; }
            
            [Display(Name = "SSUFlag")]
            [StringLength(1, ErrorMessage = "SSUFlag must be under 1 characters.")]
            public string SSUFlag { get; set; }
            
            [Display(Name = "Agreement Type")]
            [StringLength(100, ErrorMessage = "Agreement Type must be under 100 characters.")]
            public string AgreementType { get; set; }

            #endregion

    	}

        #endregion

    }
}
