using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.CallCenter.Data
{
    public class CallCenterAccountInfo
    {
        public CallCenterGetAccountInfoResult CallCenterGetAccountInfoResult { get; set; }
        public List<CallCenterGetAccountBankSettleResult> CallCenterGetAccountBankSettleResults { get; set; }
        public List<CallCenterGetAccountMutualFundResult> CallCenterGetAccountMutualFundResults { get; set; }
        public List<CallCenterGetCashTranResult> CallCenterGetCashTranResults { get; set; }
        public List<CallCenterGetEdividendResult> CallCenterGetEdividendResults { get; set; }
        public List<CallCenterGetEquNetBuySellResult> CallCenterGetEquNetBuySellResults { get; set; }
        public List<CallCenterGetMfTranResult> GetCallCenterGetMfTranResults { get; set; }
        public List<CallCenterGetLoginResult> GetCallCenterGetLoginResults { get; set; }

    }
}
