using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phatra.Core.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace Phatra.Core.Managers
{
    public class IfisManager : BaseManager
    {

        public string UpdateStockInIFIS(SqlTransaction transaction, string inRefNo)
        {
            string UpdateType;
            string Ac_No;
            string StockSym;
            string Stock_Type;
            decimal Quantity;
            decimal Cost_Price;
            string TTF_Flag;
            decimal Buy_CR;
            decimal Sell_CR;
            decimal Credit_Line;
            decimal Upper_Credit;
            string Flag;
            string Ref_No;
            string Result;
            string sResult = "OK";

            DataSet ds = null;

            if (transaction == null)
            {
                ds = SqlHelper.Instance.ExecuteDataset("USP_Front_UpdateStockAndCredit", inRefNo);
            }
            else
            {
                ds = SqlHelper.Instance.ExecuteDataset(transaction, "USP_Front_UpdateStockAndCredit", inRefNo);
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //clearOldTransaction();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UpdateType = dr["Update_Type"].ToString();
                    if (UpdateType.CompareTo("P") == 0)
                    {
                        Ac_No = dr["AC_NO"].ToString();
                        StockSym = dr["Sec_Code"].ToString();
                        Stock_Type = dr["Stock_Type"].ToString();
                        Quantity = Convert.ToDecimal(dr["Quantity_Update"]);
                        Cost_Price = Convert.ToDecimal(dr["Cost_Price"]);
                        TTF_Flag = dr["TTF_ID"].ToString();
                        Ref_No = dr["Upd_NO"].ToString();
                        Result = Store_Up_Position(transaction, Ac_No, StockSym, Stock_Type, Quantity, Cost_Price, TTF_Flag, Ref_No);
                    }
                    else
                    {
                        Ac_No = dr["AC_NO"].ToString();
                        Buy_CR = Convert.ToDecimal(dr["Buy_CR_Update"]);
                        Sell_CR = Convert.ToDecimal(dr["Sell_CR_Update"]);
                        Credit_Line = Convert.ToDecimal(dr["CR_Line_Update"]);
                        Upper_Credit = 0;
                        Flag = dr["Trading_Flag"].ToString();
                        Ref_No = dr["Upd_NO"].ToString();
                        Result = Store_Up_Credit_Maintain(transaction, Ac_No, Buy_CR, Sell_CR, Credit_Line, Upper_Credit, Flag, Ref_No);
                    }
                    if (Result != "OK") sResult = Result;
                }
            }
            return sResult;
        }

        public string UpdateStockInIFIS(string inRefNo)
        {
            return UpdateStockInIFIS(null, inRefNo);
        }

        public string Store_Up_Position(SqlTransaction transaction, string acNo, string stockSym, string stockType, decimal quantity, decimal costPrice, string ttfFlag, string refNo)
        {
            string cmd = string.Format("declare @Err int, @Msg varchar(255) EXEC SQLTRADESERVER.IFIS.dbo.USP_IFIS_SendPositionToIfis {0},{1},{2},{3},{4},{5},{6}, @Err output, @Msg output if isnull(@Err,0)<>0 raiserror(@Msg,16,1)",
                    SqlHelper._s(acNo),
                    SqlHelper._s(stockSym),
                    SqlHelper._s(stockType),
                    SqlHelper._s(quantity.ToString(), true, true),
                    SqlHelper._s(costPrice.ToString(), true, true),
                    SqlHelper._s(ttfFlag),
                    SqlHelper._s(refNo)
                );

            SqlHelper.Instance.ExecuteSqlcommand(transaction, cmd);

            return "OK";
        }

        public string Store_Up_Credit_Maintain(SqlTransaction transaction, string acNo, decimal buyCr, decimal sellCr, decimal creditLine, decimal upperCredit, string flag, string refNo)
        {
            string cmd = string.Format("declare @Err int, @Msg varchar(255) EXEC SQLTRADESERVER.IFIS.dbo.USP_IFIS_SendCreditToIfis {0},{1},{2},{3},{4},{5},{6}, @Err output, @Msg output if isnull(@Err,0)<>0 raiserror(@Msg,16,1)",
                SqlHelper._s(acNo),
                SqlHelper._s(buyCr.ToString(), true, true),
                SqlHelper._s(sellCr.ToString(), true, true),
                SqlHelper._s(creditLine.ToString(), true, true),
                SqlHelper._s(upperCredit.ToString(), true, true),
                SqlHelper._s(flag),
                SqlHelper._s(refNo)
            );

            SqlHelper.Instance.ExecuteSqlcommand(transaction, cmd);

            return "OK";
        }


    }
}
