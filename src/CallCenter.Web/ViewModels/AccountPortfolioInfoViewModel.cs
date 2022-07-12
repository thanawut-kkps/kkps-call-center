﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phatra.CallCenter.Data;

namespace CallCenter.Web.ViewModels
{
    public class AccountPortfolioInfoViewModel
    {
        public string Account { get; set; }
        public string Turnoveracc { get; set; }
        public string AssetLib { get; set; }
        public string Viewby { get; set; }
        public Nullable<int> Grpord { get; set; }
        public string Grpname { get; set; }
        public Nullable<int> Classord { get; set; }
        public string Classcode { get; set; }
        public string Classdesc { get; set; }
        public string Subgrp1 { get; set; }
        public Nullable<int> Grp1ord { get; set; }
        public Nullable<System.DateTime> Posdate { get; set; }
        public string Porttype { get; set; }
        public string Desk { get; set; }
        public string AType { get; set; }
        public string AmcCode { get; set; }
        public string ShareCode { get; set; }
        public string ShareCodes { get; set; }
        public Nullable<decimal> CUnitBal { get; set; }
        public Nullable<double> Cavgcost { get; set; }
        public Nullable<decimal> Ctotcost { get; set; }
        public Nullable<decimal> Mktprice { get; set; }
        public Nullable<decimal> Mktvalue { get; set; }
        public Nullable<decimal> Ugl { get; set; }
        public Nullable<decimal> UglPct { get; set; }
        public Nullable<decimal> Totmkt { get; set; }
        public Nullable<decimal> WeightPct { get; set; }
        public string Flag { get; set; }
        public Nullable<int> NoDec { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> TotalValue { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }

        public Nullable<decimal> TotalUnrealizedGainLoss { get; set; }        
        public Nullable<decimal> TotalPortfolioUnrealizedGainLoss { get; set; }
        

        public Nullable<decimal> TotalPortfolioCost { get; set; }
        public Nullable<decimal> TotalPortfolioValue { get; set; }
        public Nullable<decimal> TotalPortfolioWeight { get; set; }
        
    }
}