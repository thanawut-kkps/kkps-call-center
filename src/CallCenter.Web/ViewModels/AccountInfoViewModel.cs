using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phatra.CallCenter.Data;

namespace CallCenter.Web.ViewModels
{
    public class AccountInfoViewModel
    {
        public AccountInfoViewModel()
        {
            AccountInfoResult = new CallCenterGetAccountInfoResult();
            EdividendResult = new CallCenterGetEdividendResult();
            BankSettlementResult = new CallCenterGetAccountBankSettleResult();

            MutualFundResults = new List<CallCenterGetAccountMutualFundResult>();
            CashTranResults = new List<CallCenterGetCashTranResult>();
            EquNetBuySellResults = new List<CallCenterGetEquNetBuySellResult>();
            MfTranResults = new List<CallCenterGetMfTranResult>();
            LoginResults = new List<CallCenterGetLoginResult>();
        }

        public string AccountNo { get; set; }
        public string AccountType { get; set; }


        public CallCenterGetAccountInfoResult AccountInfoResult { get; set; }
        public CallCenterGetAccountBankSettleResult BankSettlementResult { get; set; }
        public CallCenterGetEdividendResult EdividendResult { get; set; }

        public List<CallCenterGetAccountMutualFundResult> MutualFundResults { get; set; }
        public List<CallCenterGetCashTranResult> CashTranResults { get; set; }        
        public List<CallCenterGetEquNetBuySellResult> EquNetBuySellResults { get; set; }
        public List<CallCenterGetMfTranResult> MfTranResults { get; set; }
        public List<CallCenterGetLoginResult> LoginResults { get; set; }
    }
}