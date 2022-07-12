using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phatra.CallCenter.Data;

namespace CallCenter.Web.ViewModels
{
    public class ClientInfoViewModel
    {
        public ClientInfoViewModel()
        {
            AddressResults = new List<CallCenterGetAddrInfoResult>();
            AccountResults = new List<CallCenterGetAccountListResult>();
            PoaResults = new List<CallCenterGetPoaResult>();
        }

        //public string PidNo { get; set; }
        public decimal? ClientId { get; set; }
        public string AccountNo { get; set; }

        public string CallSession { get; set; }
        public string AgentId { get; set; }
        public string Extension { get; set; }
        public string CustomerPhone { get; set; }
        public string OriginalAccountNo { get; set; }
        public decimal? OriginalClientId { get; set; }
        public string CallStartTime { get; set; }
        public string Product { get; set; }

        public CallCenterGetProfileResult ProfileResult { get; set; }
        public List<CallCenterGetAddrInfoResult> AddressResults { get; set; }
        public List<CallCenterGetAccountListResult> AccountResults { get; set; }
        public List<CallCenterGetPoaResult> PoaResults { get; set; }

        public CallCenterGetBankingTabStatusResult BankingTabStatusResult { get; set; }


    }
}