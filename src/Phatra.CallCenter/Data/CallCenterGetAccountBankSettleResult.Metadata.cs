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
    public partial class CallCenterGetAccountBankSettleResult
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
            
            [Display(Name = "Buy Pay Type")]
            public Nullable<int> BuyPayType { get; set; }
            
            [Display(Name = "Buy Pay Type Desc")]
            [StringLength(100, ErrorMessage = "Buy Pay Type Desc must be under 100 characters.")]
            public string BuyPayTypeDesc { get; set; }
            
            [Display(Name = "Ats Buy Bank Id")]
            public Nullable<int> AtsBuyBankId { get; set; }
            
            [Display(Name = "Bank Buy Sname")]
            [StringLength(10, ErrorMessage = "Bank Buy Sname must be under 10 characters.")]
            public string BankBuySname { get; set; }
            
            [Display(Name = "ATSBuy Account Name")]
            [StringLength(200, ErrorMessage = "ATSBuy Account Name must be under 200 characters.")]
            public string ATSBuyAccountName { get; set; }
            
            [Display(Name = "ATSBuy Bank Account")]
            [StringLength(300, ErrorMessage = "ATSBuy Bank Account must be under 300 characters.")]
            public string ATSBuyBankAccount { get; set; }
            
            [Display(Name = "ATSBuy Account Type")]
            [StringLength(1, ErrorMessage = "ATSBuy Account Type must be under 1 characters.")]
            public string ATSBuyAccountType { get; set; }
            
            [Display(Name = "ATSBuyBranchID")]
            public Nullable<int> ATSBuyBranchID { get; set; }
            
            [Display(Name = "Ats Buy Branch Name")]
            [StringLength(100, ErrorMessage = "Ats Buy Branch Name must be under 100 characters.")]
            public string AtsBuyBranchName { get; set; }
            
            [Display(Name = "Sell Pay Type Desc")]
            [StringLength(100, ErrorMessage = "Sell Pay Type Desc must be under 100 characters.")]
            public string SellPayTypeDesc { get; set; }
            
            [Display(Name = "Ats Sell Bank Id")]
            public Nullable<int> AtsSellBankId { get; set; }
            
            [Display(Name = "Bank Sell Sname")]
            [StringLength(10, ErrorMessage = "Bank Sell Sname must be under 10 characters.")]
            public string BankSellSname { get; set; }
            
            [Display(Name = "ATSSell Account Name")]
            [StringLength(200, ErrorMessage = "ATSSell Account Name must be under 200 characters.")]
            public string ATSSellAccountName { get; set; }
            
            [Display(Name = "ATSSell Bank Account")]
            [StringLength(300, ErrorMessage = "ATSSell Bank Account must be under 300 characters.")]
            public string ATSSellBankAccount { get; set; }
            
            [Display(Name = "ATSSell Account Type")]
            [StringLength(1, ErrorMessage = "ATSSell Account Type must be under 1 characters.")]
            public string ATSSellAccountType { get; set; }
            
            [Display(Name = "ATSSellBranchID")]
            public Nullable<int> ATSSellBranchID { get; set; }
            
            [Display(Name = "Ats Sell Branch Name")]
            [StringLength(100, ErrorMessage = "Ats Sell Branch Name must be under 100 characters.")]
            public string AtsSellBranchName { get; set; }
            
            [Display(Name = "Amc Code")]
            [StringLength(30, ErrorMessage = "Amc Code must be under 30 characters.")]
            public string AmcCode { get; set; }
            
            [Display(Name = "Fund Code")]
            [StringLength(30, ErrorMessage = "Fund Code must be under 30 characters.")]
            public string FundCode { get; set; }
            
            [Display(Name = "ATSBuy Account Type Desc")]
            [StringLength(100, ErrorMessage = "ATSBuy Account Type Desc must be under 100 characters.")]
            public string ATSBuyAccountTypeDesc { get; set; }
            
            [Display(Name = "Sell Pay Type")]
            public Nullable<int> SellPayType { get; set; }
            
            [Display(Name = "ATSSell Account Type Desc")]
            [StringLength(100, ErrorMessage = "ATSSell Account Type Desc must be under 100 characters.")]
            public string ATSSellAccountTypeDesc { get; set; }

            #endregion
    	}

        #endregion
    }
}
