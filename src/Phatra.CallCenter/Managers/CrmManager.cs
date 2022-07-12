using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Phatra.Core.Managers;
using Phatra.CallCenter.Data;
using Phatra.Core.Utilities;

using Phatra.Core;
using Phatra.Core.Logging;

namespace Phatra.CallCenter.Managers
{
    public class CrmManager : BaseManager, ICrmManager
    {
        private ILog _log;
        //private string _pId;
        public CrmManager(ILog log)
        {
            _log = log;
            //_pId = "1101401085161";
        }

        public CallCenterGetProfileResult GetCallCenterGetProfileResult(decimal? clientId)
        {
            CallCenterGetProfileResult item = null;

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_profile]",
                clientId);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new CallCenterGetProfileResult();
                    item.PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>();
                    item.PidTypeDesc = dt.Rows[i]["pid_type_desc"].ConvertTo<string>();
                    item.ClientNameT = dt.Rows[i]["client_name_t"].ConvertTo<string>();
                    item.ClientNameE = dt.Rows[i]["client_name_e"].ConvertTo<string>();
                    item.BirthDate = dt.Rows[i]["birth_date"].ConvertTo<DateTime?>();
                    item.NationCode = dt.Rows[i]["nation_code"].ConvertTo<string>();
                    item.NationDesc = dt.Rows[i]["nation_desc"].ConvertTo<string>();
                    item.TelHome = dt.Rows[i]["Tel_home"].ConvertTo<string>();
                    item.TelOffice = dt.Rows[i]["Tel_Office"].ConvertTo<string>();
                    item.Mobile = dt.Rows[i]["mobile"].ConvertTo<string>();
                    item.FatcaStatus = dt.Rows[i]["FatcaStatus"].ConvertTo<string>();
                    item.AddrRegister = dt.Rows[i]["addr_register"].ConvertTo<string>();
                    item.AddrMail = dt.Rows[i]["addr_mail"].ConvertTo<string>();
                    item.ContactNameT = dt.Rows[i]["contact_name_t"].ConvertTo<string>().Trim();
                    item.ContactNameE = dt.Rows[i]["contact_name_e"].ConvertTo<string>().Trim();
                    item.ContactRelationship = dt.Rows[i]["contact_relationship"].ConvertTo<string>();
                    item.ContactTelHome = dt.Rows[i]["Contact_Tel_Home"].ConvertTo<string>();
                    item.ContactTelOffice = dt.Rows[i]["Contact_Tel_Office"].ConvertTo<string>();
                    item.ContactMobile = dt.Rows[i]["Contact_Mobile"].ConvertTo<string>();
                    item.MasterPoa = dt.Rows[i]["master_poa"].ConvertTo<string>();
                    item.CardExpireDate = dt.Rows[i]["card_expire_date"].ConvertTo<DateTime?>();
                    item.CardExpireWithin = dt.Rows[i]["card_expire_within"].ConvertTo<string>();
                    item.CardExpireInfo = dt.Rows[i]["card_expire_info"].ConvertTo<string>();
                    item.FATCATypeDesc = dt.Rows[i]["FATCA_Type_Desc"].ConvertTo<string>();          
                    item.SSUFlag = dt.Rows[i]["SSU_Flag"].ConvertTo<string>();
                    item.AgreementType = dt.Rows[i]["agreement_type"].ConvertTo<string>();
                }
            }
            return item;
        }

        public List<CallCenterGetAddrInfoResult> GetCallCenterGetAddrInfoResults(decimal? clientId)
        {
            var list = new List<CallCenterGetAddrInfoResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_addrinfo]",
                //_pId, // pid
                clientId
                );

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAddrInfoResult();
                    item.PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>();
                    item.AddrSeq = dt.Rows[i]["addr_seq"].ConvertTo<int?>();
                    item.AddrDesc = dt.Rows[i]["addr_desc"].ConvertTo<string>().Trim();
                    item.TelHome = dt.Rows[i]["tel_home"].ConvertTo<string>().Trim();
                    item.TelOffice = dt.Rows[i]["tel_office"].ConvertTo<string>().Trim();
                    item.Mobile = dt.Rows[i]["mobile"].ConvertTo<string>().Trim();
                    item.Fax = dt.Rows[i]["fax"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetAccountListResult> GetCallCenterGetAccountListResults(decimal? clientId)
        {
            var list = new List<CallCenterGetAccountListResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_account_list]",
                //_pId, // pid
                clientId);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountListResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.Product = dt.Rows[i]["product"].ConvertTo<string>();
                    item.AccStatus = dt.Rows[i]["acc_status"].ConvertTo<string>();
                    item.JointAccount = dt.Rows[i]["Joint_Account"].ConvertTo<string>();
                    item.AccountNameT = dt.Rows[i]["account_name_t"].ConvertTo<string>();
                    item.AccountNameE = dt.Rows[i]["account_name_e"].ConvertTo<string>();
                    item.PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>();
                    item.SuitLevel = dt.Rows[i]["suit_level"].ConvertTo<int?>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetPoaResult> GetCallCenterGetPoaResults(decimal? clientId)
        {
            var list = new List<CallCenterGetPoaResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_POA]", clientId ?? -1);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetPoaResult()
                    {
                        ClientID = dt.Rows[i]["Client_ID"].ConvertTo<decimal>(),
                        POANameT = dt.Rows[i]["POA_NameT"].ConvertTo<string>(),
                        PoaId = dt.Rows[i]["poa_id"].ConvertTo<decimal>(),
                        POAPid = dt.Rows[i]["POA_Pid"].ConvertTo<string>(),
                        POATelNo = dt.Rows[i]["POA_TelNo"].ConvertTo<string>(),
                        ProductCode = dt.Rows[i]["Product_Code"].ConvertTo<string>(),
                        AccountType = dt.Rows[i]["Account_Type"].ConvertTo<string>(),
                        AccountNO = dt.Rows[i]["Account_NO"].ConvertTo<string>(),
                        Flag1 = dt.Rows[i]["flag1"].ConvertTo<string>(),
                        Flag2 = dt.Rows[i]["flag2"].ConvertTo<string>(),
                        Flag3 = dt.Rows[i]["flag3"].ConvertTo<string>(),
                        Flag4 = dt.Rows[i]["flag4"].ConvertTo<string>(),
                        Flag5 = dt.Rows[i]["flag5"].ConvertTo<string>(),
                        Flag6 = dt.Rows[i]["flag6"].ConvertTo<string>(),
                        Flag7 = dt.Rows[i]["flag7"].ConvertTo<string>(),
                        FlagOther = dt.Rows[i]["flag_other"].ConvertTo<string>(),
                        DocCompleted = dt.Rows[i]["doc_completed"].ConvertTo<string>(),
                        POANameE = dt.Rows[i]["POA_NameE"].ConvertTo<string>(),
                        Relationship = dt.Rows[i]["Relationship"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public CallCenterGetBankingTabStatusResult GetCallCenterGetBankingTabStatusResult(decimal? clientId)
        {
            CallCenterGetBankingTabStatusResult item = null;

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_bankingtab_status]", clientId);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new CallCenterGetBankingTabStatusResult();
                    item.ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal>();
                    item.GUIDStatus = dt.Rows[i]["GUIDStatus"].ConvertTo<string>();
                    item.Reason = dt.Rows[i]["reason"].ConvertTo<string>();
                    item.ConsentStatus = dt.Rows[i]["ConsentStatus"].ConvertTo<string>();                    
                }
            }
            return item;
        }

        public CallCenterGetAccountInfoResult GetCallCenterGetAccountInfoResult(string accountNo, string accountType)
        {
            CallCenterGetAccountInfoResult item = null;

            dynamic objAccountType = null;

            if (!string.IsNullOrWhiteSpace(accountType))
            {
                objAccountType = accountType;
            }

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_account_info]", accountNo, objAccountType);

            if (dt.Rows.Count > 0)
            {
                var i = 0;
                item = new CallCenterGetAccountInfoResult();
                item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                item.AccountType = dt.Rows[i]["account_type"].ConvertTo<string>();
                item.JointAccount = dt.Rows[i]["Joint_Account"].ConvertTo<string>();
                item.AccountNameT = dt.Rows[i]["account_name_t"].ConvertTo<string>();
                item.AccountNameE = dt.Rows[i]["account_name_e"].ConvertTo<string>();
                item.FcnameT = dt.Rows[i]["fcname_t"].ConvertTo<string>();
                item.FcnameE = dt.Rows[i]["fcname_e"].ConvertTo<string>();
                item.FcOfftel = dt.Rows[i]["fc_offtel"].ConvertTo<string>();
                item.EmailEdoc = dt.Rows[i]["email_edoc"].ConvertTo<string>();
                item.FaxConfirm = dt.Rows[i]["fax_confirm"].ConvertTo<string>();
                item.PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>();
                item.PidTypeDesc = dt.Rows[i]["pid_type_desc"].ConvertTo<string>();
                item.Remark = dt.Rows[i]["remark"].ConvertTo<string>();
                item.OpenDate = dt.Rows[i]["Open_Date"].ConvertTo<DateTime?>();
            }

            return item;
        }

        public List<CallCenterGetAccountBankSettleResult> GetCallCenterGetAccountBankSettleResults(string accountNo)
        {
            var list = new List<CallCenterGetAccountBankSettleResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_account_banksettle]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountBankSettleResult()
                    {
                        AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                        AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>(),

                        ATSBuyAccountName = dt.Rows[i]["ATS_Buy_Account_Name"].ConvertTo<string>(),
                        ATSBuyAccountType = dt.Rows[i]["ATS_Buy_Account_Type"].ConvertTo<string>(),
                        ATSBuyAccountTypeDesc = dt.Rows[i]["ATS_Buy_Account_Type_desc"].ConvertTo<string>(),
                        ATSBuyBankAccount = dt.Rows[i]["ATS_Buy_Bank_Account"].ConvertTo<string>(),
                        AtsBuyBankId = dt.Rows[i]["ats_buy_bank_id"].ConvertTo<int?>(),
                        ATSBuyBranchID = dt.Rows[i]["ATS_Buy_Branch_ID"].ConvertTo<int?>(),
                        AtsBuyBranchName = dt.Rows[i]["ats_buy_branch_name"].ConvertTo<string>(),

                        ATSSellAccountName = dt.Rows[i]["ATS_sell_Account_Name"].ConvertTo<string>(),
                        ATSSellAccountType = dt.Rows[i]["ATS_sell_Account_Type"].ConvertTo<string>(),
                        ATSSellAccountTypeDesc = dt.Rows[i]["ATS_sell_Account_Type_desc"].ConvertTo<string>(),
                        ATSSellBankAccount = dt.Rows[i]["ATS_sell_Bank_Account"].ConvertTo<string>(),
                        AtsSellBankId = dt.Rows[i]["ats_sell_bank_id"].ConvertTo<int?>(),
                        ATSSellBranchID = dt.Rows[i]["ATS_sell_Branch_ID"].ConvertTo<int?>(),
                        AtsSellBranchName = dt.Rows[i]["ats_sell_branch_name"].ConvertTo<string>(),

                        BankBuySname = dt.Rows[i]["bank_buy_sname"].ConvertTo<string>(),
                        BankSellSname = dt.Rows[i]["bank_sell_sname"].ConvertTo<string>(),
                        BuyPayType = dt.Rows[i]["buy_pay_type"].ConvertTo<int?>(),
                        BuyPayTypeDesc = dt.Rows[i]["buy_pay_type_desc"].ConvertTo<string>(),
                        FundCode = dt.Rows[i]["fund_code"].ConvertTo<string>(),
                        SellPayType = dt.Rows[i]["Sell_Pay_Type"].ConvertTo<int?>(),
                        SellPayTypeDesc = dt.Rows[i]["sell_pay_type_desc"].ConvertTo<string>()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetAccountMutualFundResult> GetCallCenterGetAccountMutualFundResults(string accountNo)
        {
            var list = new List<CallCenterGetAccountMutualFundResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_account_mf]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountMutualFundResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>();
                    item.HolderId = dt.Rows[i]["holder_id"].ConvertTo<string>();
                    item.HolderNameE = dt.Rows[i]["holder_name_e"].ConvertTo<string>();
                    item.HolderNameT = dt.Rows[i]["holder_name_t"].ConvertTo<string>();
                    item.StatusId = dt.Rows[i]["status_id"].ConvertTo<int?>();
                    item.StatusDesc = dt.Rows[i]["status_desc"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetCashTranResult> GetCallCenterGetCashTranResults(string accountNo)
        {
            var list = new List<CallCenterGetCashTranResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_cashtran]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetCashTranResult();
                    item.Account = dt.Rows[i]["account"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.Sharecodes = dt.Rows[i]["sharecodes"].ConvertTo<string>();
                    item.Trandesc = dt.Rows[i]["trandesc"].ConvertTo<string>();
                    item.Amt = dt.Rows[i]["amt"].ConvertTo<decimal?>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetEdividendResult> GetCallCenterGetEdividendResults(string accountNo)
        {
            var list = new List<CallCenterGetEdividendResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_edividend]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetEdividendResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.DvdBankShortname = dt.Rows[i]["dvd_bank_shortname"].ConvertTo<string>();
                    item.DvdBankAccountNo = dt.Rows[i]["dvd_bank_account_no"].ConvertTo<string>();
                    item.DbdBankAccountTypeDesc = dt.Rows[i]["dbd_bank_account_type_desc"].ConvertTo<string>();
                    item.DvdBranchName = dt.Rows[i]["dvd_branch_name"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetEquNetBuySellResult> GetCallCenterGetEquNetBuySellResults(string accountNo)
        {
            var list = new List<CallCenterGetEquNetBuySellResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_equ_netbuysell]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetEquNetBuySellResult();
                    item.Account = dt.Rows[i]["account"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.Settdate = dt.Rows[i]["settdate"].ConvertTo<DateTime>();
                    item.AssetType = dt.Rows[i]["AssetType"].ConvertTo<string>();
                    item.NetDesc = dt.Rows[i]["net_desc"].ConvertTo<string>();
                    item.ClientReceive = dt.Rows[i]["client_receive"].ConvertTo<decimal?>();
                    item.ClientPay = dt.Rows[i]["client_pay"].ConvertTo<decimal?>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetMfTranResult> GetCallCenterGetMfTranResults(string accountNo)
        {
            var list = new List<CallCenterGetMfTranResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_mftran]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetMfTranResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.HolderId = dt.Rows[i]["holder_id"].ConvertTo<string>();
                    item.RefNo = dt.Rows[i]["ref_no"].ConvertTo<string>();
                    item.AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>();
                    item.FundCode = dt.Rows[i]["fund_code"].ConvertTo<string>();
                    item.TrantypeCode = dt.Rows[i]["trantype_code"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.AmountUnit = dt.Rows[i]["amount_unit"].ConvertTo<decimal?>();
                    item.AmountBaht = dt.Rows[i]["amount_baht"].ConvertTo<decimal?>();
                    item.TranStatusId = dt.Rows[i]["tran_status_id"].ConvertTo<int?>();
                    item.TranStatusDesc = dt.Rows[i]["tran_status_desc"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetLoginResult> GetCallCenterGetLoginResults(string accountNo)
        {
            var list = new List<CallCenterGetLoginResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_login]", accountNo);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetLoginResult()
                    {
                        UserName = dt.Rows[i]["UserName"].ConvertTo<string>(),
                        PinStatus = dt.Rows[i]["PinStatus"].ConvertTo<string>(),
                        LoginStatus = dt.Rows[i]["LoginStatus"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public List<RefGetPurposeVerificationsResult> GetRefGetPurposeVerificationsResults()
        {
            var list = new List<RefGetPurposeVerificationsResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_ref_get_purpose_verifications]");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new RefGetPurposeVerificationsResult();
                    item.PurposeId = dt.Rows[i]["purpose_id"].ConvertTo<int>();
                    item.PurposeDesc = dt.Rows[i]["purpose_desc"].ConvertTo<string>();
                    item.MinAskQuest = dt.Rows[i]["min_ask_quest"].ConvertTo<int?>();
                    item.MinPassPersonQuest = dt.Rows[i]["min_pass_person_quest"].ConvertTo<int?>();
                    item.MinPassPoaQuest = dt.Rows[i]["min_pass_poa_quest"].ConvertTo<int?>();
                    item.Active = dt.Rows[i]["active"].ConvertTo<bool?>();
                    item.UpdatedDate = dt.Rows[i]["updated_date"].ConvertTo<DateTime?>();
                    item.UpdatedBy = dt.Rows[i]["updated_by"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetQuestionVerificationResult> GetCallCenterGetQuestionVerificationResults(int proposeId)
        {
            var list = new List<CallCenterGetQuestionVerificationResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_question_verification]", proposeId);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetQuestionVerificationResult()
                    {
                        PurposeId = dt.Rows[i]["purpose_id"].ConvertTo<int>(),
                        QuestionId = dt.Rows[i]["question_id"].ConvertTo<int>(),
                        Seqno = dt.Rows[i]["Seqno"].ConvertTo<int>(),
                        QuestionDetail = dt.Rows[i]["question_detail"].ConvertTo<string>(),
                        MapToHtml = dt.Rows[i]["map_to_html"].ConvertTo<string>(),
                        MustAsk = dt.Rows[i]["Must_ask"].ConvertTo<bool>(),
                        IsPoaQuestion = dt.Rows[i]["IsPoaQuestion"].ConvertTo<string>(),
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public void SaveVerificationsResult(string callSession, List<VerificationResult> verificationResults)
        {
            using (var con = SqlHelper.Instance.CreateConnection())
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    DateTime verifiedDate = DateTime.Now;
                    SqlHelper.Instance.ExecuteNonQuery(tran, "[dbo].[usp_cc_delete_verificaiton_result]", callSession);

                    foreach (var result in verificationResults)
                    {
                        SqlHelper.Instance.ExecuteNonQuery(tran, "[dbo].[usp_cc_save_verificaiton_result]",
                            result.QuestionId, result.PurposeId, result.CallSession,
                            result.ClientId, result.CustomerPhone, result.AgentId, verifiedDate, result.IsPassed);
                    }
                    tran.Commit();
                }
            }

        }

        public List<CallCenterGetReminderResult> GetCallCenterGetReminderResultsByClientId(decimal? clientId)
        {
            var list = new List<CallCenterGetReminderResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_reminder]", clientId ?? -1, null);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetReminderResult()
                    {
                        ReminderId = dt.Rows[i]["reminder_id"].ConvertTo<int>(),
                        CallSession = dt.Rows[i]["call_session"].ConvertTo<string>(),
                        CustomerPhone = dt.Rows[i]["customer_phone"].ConvertTo<string>(),
                        ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                        AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                        Details = dt.Rows[i]["details"].ConvertTo<string>(),
                        RemindDate = dt.Rows[i]["remind_date"].ConvertTo<DateTime?>(),
                        IsCancelled = dt.Rows[i]["is_cancelled"].ConvertTo<bool?>(),
                        CreatedDate = dt.Rows[i]["created_date"].ConvertTo<DateTime?>(),
                        CreatedUser = dt.Rows[i]["created_user"].ConvertTo<string>(),
                        UpdatedDate = dt.Rows[i]["updated_date"].ConvertTo<DateTime?>(),
                        UpdatedUser = dt.Rows[i]["updated_user"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public CallCenterGetReminderResult GetCallCenterGetReminderResultsById(int? reminderId)
        {
            CallCenterGetReminderResult item = null;

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_reminder]", null, reminderId ?? -1);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    throw new InvalidOperationException(string.Format("The reminder '{0}' result contains more than one element", reminderId));
                }

                int i = 0;
                item = new CallCenterGetReminderResult()
                {
                    ReminderId = dt.Rows[i]["reminder_id"].ConvertTo<int>(),
                    CallSession = dt.Rows[i]["call_session"].ConvertTo<string>(),
                    CustomerPhone = dt.Rows[i]["customer_phone"].ConvertTo<string>(),
                    ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                    AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                    Details = dt.Rows[i]["details"].ConvertTo<string>(),
                    RemindDate = dt.Rows[i]["remind_date"].ConvertTo<DateTime?>(),
                    IsCancelled = dt.Rows[i]["is_cancelled"].ConvertTo<bool?>(),
                    CreatedDate = dt.Rows[i]["created_date"].ConvertTo<DateTime?>(),
                    CreatedUser = dt.Rows[i]["created_user"].ConvertTo<string>(),
                    UpdatedDate = dt.Rows[i]["updated_date"].ConvertTo<DateTime?>(),
                    UpdatedUser = dt.Rows[i]["updated_user"].ConvertTo<string>()
                };

            }
            return item;
        }

        public CallCenterGetWrapUpInfoResult GetCallCenterGetWrapUpInfoResult(string callSession)
        {
            CallCenterGetWrapUpInfoResult item = null;

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_wrapup_info]", callSession);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    throw new InvalidOperationException(string.Format("The wrapup '{0}' result contains more than one element", callSession));
                }

                int i = 0;
                item = new CallCenterGetWrapUpInfoResult()
                {
                    CallSession = dt.Rows[i]["call_session"].ConvertTo<string>(),
                    AgentId = dt.Rows[i]["agent_id"].ConvertTo<string>(),
                    CustomerPhone = dt.Rows[i]["customer_phone"].ConvertTo<string>(),
                    OriginalAccountNo = dt.Rows[i]["original_account_no"].ConvertTo<string>(),
                    OriginalClientId = dt.Rows[i]["original_client_id"].ConvertTo<decimal?>(),
                    ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                    AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                    CallStarttime = dt.Rows[i]["call_starttime"].ConvertTo<DateTime>(),
                    WrapupText = dt.Rows[i]["wrapup_text"].ConvertTo<string>(),
                    WrapupDateTime = dt.Rows[i]["wrapup_dateTime"].ConvertTo<DateTime?>(),
                    PoaId = dt.Rows[i]["poa_id"].ConvertTo<decimal?>()
                };

            }
            return item;
        }

        public void SaveReminder(int? reminderId, CallCenterGetReminderResult model)
        {
            SqlHelper.Instance.ExecuteNonQuery("[dbo].[usp_cc_save_reminder]",
                    reminderId,
                    model.CallSession,
                    model.CustomerPhone,
                    model.ClientId,
                    model.AccountNo,
                    model.Details,
                    model.RemindDate,
                    model.IsCancelled,
                    model.UpdatedDate,
                    model.UpdatedUser);
        }

        public void CancelReminder(int? reminderId, DateTime updatedDate, string updatedUser)
        {
            SqlHelper.Instance.ExecuteNonQuery("[dbo].[usp_cc_cancel_reminder]",
                   reminderId,
                   updatedDate,
                   updatedUser);
        }

        public List<CallCenterGetHistoryWrapUpInfoResult> GetCallCenterGetHistoryWrapUpByClientId(decimal? clientId)
        {
            var list = new List<CallCenterGetHistoryWrapUpInfoResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_history_wrapup]", clientId ?? -1, null);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetHistoryWrapUpInfoResult()
                    {
                        CallSession = dt.Rows[i]["call_session"].ConvertTo<string>(),
                        AgentId = dt.Rows[i]["agent_id"].ConvertTo<string>(),
                        CustomerPhone = dt.Rows[i]["customer_phone"].ConvertTo<string>(),
                        OriginalAccountNo = dt.Rows[i]["original_account_no"].ConvertTo<string>(),
                        OriginalClientId = dt.Rows[i]["original_client_id"].ConvertTo<decimal?>(),

                        AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                        ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                        
                        CallStarttime = dt.Rows[i]["call_starttime"].ConvertTo<DateTime>(),
                        WrapupText = dt.Rows[i]["wrapup_text"].ConvertTo<string>(),
                        WrapupDateTime = dt.Rows[i]["wrapup_dateTime"].ConvertTo<DateTime>(),
                        
                        PoaId = dt.Rows[i]["poa_id"].ConvertTo<decimal?>(),
                        ReasonDesc = dt.Rows[i]["reason_desc"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }
        
        public CallCenterAccountInfo GetCallCenterAccountInfo(string accountNo, string accountType)
        {
            CallCenterAccountInfo accountInfo = new CallCenterAccountInfo();

            using (var con = SqlHelper.Instance.CreateConnection())
            {
                dynamic objAccountType = null;
                DataTable dt = null;

                if (!string.IsNullOrWhiteSpace(accountType))
                {
                    objAccountType = accountType;
                }

                try
                {
                    con.Open();

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_account_info]", accountNo, objAccountType);
                    accountInfo.CallCenterGetAccountInfoResult = FillCallCenterGetAccountInfoResult(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_account_banksettle]", accountNo);
                    accountInfo.CallCenterGetAccountBankSettleResults = GetCallCenterGetAccountBankSettleResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_account_mf]", accountNo);
                    accountInfo.CallCenterGetAccountMutualFundResults = GetCallCenterGetAccountMutualFundResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_cashtran]", accountNo, 'N');
                    accountInfo.CallCenterGetCashTranResults = GetCallCenterGetCashTranResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_edividend]", accountNo);
                    accountInfo.CallCenterGetEdividendResults = GetCallCenterGetEdividendResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_equ_netbuysell]", accountNo, 'N');
                    accountInfo.CallCenterGetEquNetBuySellResults = GetCallCenterGetEquNetBuySellResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_mftran]", accountNo, 'N');
                    accountInfo.GetCallCenterGetMfTranResults = GetCallCenterGetMfTranResults(dt);

                    dt = SqlHelper.Instance.ExecuteDataTable(con, "dbo.[usp_cc_get_login]", accountNo);
                    accountInfo.GetCallCenterGetLoginResults = GetCallCenterGetLoginResults(dt);
                }
                catch (Exception ex)
                {
                    _log.Error("{0}", ex.ToString());
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return accountInfo;
        }

        public List<CallCenterGetWrapupReasonResult> GetCallCenterGetWrapupReasonResults()
        {
            var list = new List<CallCenterGetWrapupReasonResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_wrapup_reason]");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetWrapupReasonResult()
                    {
                        ReasonCode = dt.Rows[i]["reason_code"].ConvertTo<string>(),
                        ReasonDesc = dt.Rows[i]["reason_desc"].ConvertTo<string>(),
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public void SaveWrapUp(string callSession, string agentId, string customerPhone,
                string originalAccountNo, decimal? originalClientId, decimal? clientId, string accountNo,
                DateTime callStartTime, string wrapUpText, DateTime? wrapUpDateTime, decimal? poaId, string reasonDesc)
        {
            SqlHelper.Instance.ExecuteNonQuery("dbo.[usp_cc_save_wrapup_info]", callSession, agentId, customerPhone,
                    originalAccountNo, originalClientId, clientId, accountNo, callStartTime, wrapUpText, wrapUpDateTime, poaId, reasonDesc);
        }

        public List<CallCenterGetSearchClientResult> GetCallCenterGetSearchClientResults(string searchText)
        {
            var list = new List<CallCenterGetSearchClientResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_search_client]", searchText);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetSearchClientResult()
                    {
                        ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                        AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                        Product = dt.Rows[i]["product"].ConvertTo<string>(),
                        SystemCode = dt.Rows[i]["system_code"].ConvertTo<string>(),
                        JointAccount = dt.Rows[i]["Joint_Account"].ConvertTo<string>(),
                        AccountNameT = dt.Rows[i]["account_name_t"].ConvertTo<string>(),
                        AccountNameE = dt.Rows[i]["account_name_e"].ConvertTo<string>(),
                        Trader = dt.Rows[i]["trader"].ConvertTo<string>(),
                        PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>(),
                        ClientTier = dt.Rows[i]["client_tier"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetAccountPortfolioInfoResult> GetCallCenterGetAccountPortfolioResults(string accountNumber)
        {
            var list = new List<CallCenterGetAccountPortfolioInfoResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_account_portfolio_info]", accountNumber, "CLASS");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountPortfolioInfoResult()
                    {
                        Account	=	dt.Rows[i]["account"].ConvertTo<string>(),
                        Turnoveracc	=	dt.Rows[i]["turnoveracc"].ConvertTo<string>(),
                        AssetLib = dt.Rows[i]["asset_lib"].ConvertTo<string>(),
                        Viewby = dt.Rows[i]["viewby"].ConvertTo<string>(),
                        Grpord = dt.Rows[i]["grpord"].ConvertTo<int?>(),
                        Grpname = dt.Rows[i]["grpname"].ConvertTo<string>(),
                        Classord = dt.Rows[i]["classord"].ConvertTo<int?>(),
                        Classcode = dt.Rows[i]["classcode"].ConvertTo<string>(),
                        Classdesc = dt.Rows[i]["classdesc"].ConvertTo<string>(),
                        Subgrp1 = dt.Rows[i]["subgrp1"].ConvertTo<string>(),
                        Grp1ord = dt.Rows[i]["grp1ord"].ConvertTo<int?>(),
                        Posdate = dt.Rows[i]["posdate"].ConvertTo<DateTime?>(),
                        Porttype = dt.Rows[i]["porttype"].ConvertTo<string>(),
                        Desk = dt.Rows[i]["desk"].ConvertTo<string>(),
                        AType = dt.Rows[i]["AType"].ConvertTo<string>(),
                        AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>(),
                        ShareCode = dt.Rows[i]["ShareCode"].ConvertTo<string>(),
                        ShareCodes = dt.Rows[i]["ShareCodes"].ConvertTo<string>(),
                        CUnitBal = dt.Rows[i]["cUnitBal"].ConvertTo<decimal?>(),
                        Cavgcost = dt.Rows[i]["cavgcost"].ConvertTo<double?>(),
                        Ctotcost = dt.Rows[i]["ctotcost"].ConvertTo<decimal?>(),
                        Mktprice = dt.Rows[i]["mktprice"].ConvertTo<decimal?>(),
                        Mktvalue = dt.Rows[i]["mktvalue"].ConvertTo<decimal?>(),
                        Ugl = dt.Rows[i]["ugl"].ConvertTo<decimal?>(),
                        UglPct = dt.Rows[i]["ugl_pct"].ConvertTo<decimal?>(),
                        Totmkt = dt.Rows[i]["totmkt"].ConvertTo<decimal?>(),
                        WeightPct = dt.Rows[i]["weight_pct"].ConvertTo<decimal?>(),
                        Flag = dt.Rows[i]["flag"].ConvertTo<string>(),
                        NoDec = dt.Rows[i]["no_dec"].ConvertTo<int?>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetDrilldownLoginResult> GetCallCenterGetDrilldownLoginResults(string login)
        {
            var list = new List<CallCenterGetDrilldownLoginResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_drilldown_login]", login);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetDrilldownLoginResult()
                    {
                        Username = dt.Rows[i]["Username"].ConvertTo<string>(),
                        AccountNo = dt.Rows[i]["AccountNo"].ConvertTo<string>(),
                        ClientNameT = dt.Rows[i]["client_name_t"].ConvertTo<string>(),
                        ClientNameE = dt.Rows[i]["client_name_e"].ConvertTo<string>(),
                        FrontTypeDesc = dt.Rows[i]["FrontTypeDesc"].ConvertTo<string>(),
                        CanTrade = dt.Rows[i]["CanTrade"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public List<CallCenterGetClientListFromPhonenoResult> GetCallCenterGetClientListFromPhonenoResults(string customerPhone)
        {
            var list = new List<CallCenterGetClientListFromPhonenoResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_clientlist_from_phoneno]", customerPhone);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetClientListFromPhonenoResult()
                    {
                        Phoneno = dt.Rows[i]["phoneno"].ConvertTo<string>(),
                        ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal>(),
                        PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>(),
                        PidTypeDesc = dt.Rows[i]["pid_type_desc"].ConvertTo<string>(),
                        ClientNameT = dt.Rows[i]["client_name_t"].ConvertTo<string>(),
                        ClientNameE = dt.Rows[i]["client_name_e"].ConvertTo<string>(),
                        BirthDate = dt.Rows[i]["birth_date"].ConvertTo<DateTime?>(),
                        ContactNameT = dt.Rows[i]["contact_name_t"].ConvertTo<string>(),
                        ContactNameE = dt.Rows[i]["contact_name_e"].ConvertTo<string>(),
                        ContactRelationship = dt.Rows[i]["contact_relationship"].ConvertTo<string>(),
                        TelHome = dt.Rows[i]["tel_home"].ConvertTo<string>(),
                        TelOffice = dt.Rows[i]["tel_office"].ConvertTo<string>(),
                        Mobile = dt.Rows[i]["mobile"].ConvertTo<string>(),
                        AddrMail = dt.Rows[i]["addr_mail"].ConvertTo<string>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }


        public List<CallCenterGetMissedCallListResult> GetCallCenterGetMissedCallListResults()
        {
            var list = new List<CallCenterGetMissedCallListResult>();

            DataTable dt = SqlHelper.Instance.ExecuteDataTable("dbo.[usp_cc_get_missedcall_list]");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetMissedCallListResult()
                    {
                        CustomerPhone = dt.Rows[i]["customer_phone"].ConvertTo<string>(),
                        ClientId = dt.Rows[i]["client_id"].ConvertTo<decimal?>(),
                        Priority = dt.Rows[i]["priority"].ConvertTo<int?>(),
                        CustomerName = dt.Rows[i]["customer_name"].ConvertTo<string>(),
                        CallStartTime = dt.Rows[i]["call_start_time"].ConvertTo<DateTime?>()
                    };

                    list.Add(item);
                }
            }
            return list;
        }

        public string SaveCallLogLnfo(CallLogInfoItem data)
        {
            String msg = "";
            try
            {
                var msgObj = SqlHelper.Instance.ExecuteScalar("dbo.usp_cc_save_call_log_info",
                                                                        data.call_session,
                                                                        data.call_starttime,
                                                                        data.customer_phone,
                                                                        data.client_id,
                                                                        data.account_no,
                                                                        data.priority,
                                                                        data.skill,
                                                                        data.agent_id,
                                                                        data.agent_first_name,
                                                                        data.agent_last_name,
                                                                        data.agent_extension,
                                                                        data.call_type);

                msg = msgObj != null ? msgObj.ToString() : "0";
                msg = msg == "0" ? "OK" : msg;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Error!") >= 0)
                    msg = ex.Message.Substring(ex.Message.IndexOf("Error!"));
                else
                    msg = ex.Message;
            }

            return msg;
        }  

        #region Private Methods

        private CallCenterGetAccountInfoResult FillCallCenterGetAccountInfoResult(DataTable dt)
        {
            CallCenterGetAccountInfoResult item = null;

            if (dt.Rows.Count > 0)
            {
                var i = 0;
                item = new CallCenterGetAccountInfoResult();
                item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                item.AccountType = dt.Rows[i]["account_type"].ConvertTo<string>();
                item.JointAccount = dt.Rows[i]["Joint_Account"].ConvertTo<string>();
                item.AccountNameT = dt.Rows[i]["account_name_t"].ConvertTo<string>();
                item.AccountNameE = dt.Rows[i]["account_name_e"].ConvertTo<string>();
                item.FcnameT = dt.Rows[i]["fcname_t"].ConvertTo<string>();
                item.FcnameE = dt.Rows[i]["fcname_e"].ConvertTo<string>();
                item.FcOfftel = dt.Rows[i]["fc_offtel"].ConvertTo<string>();
                item.Email = dt.Rows[i]["email"].ConvertTo<string>();
                item.EmailEdoc = dt.Rows[i]["email_edoc"].ConvertTo<string>();
                item.FaxConfirm = dt.Rows[i]["fax_confirm"].ConvertTo<string>();
                item.PidNo = dt.Rows[i]["pid_no"].ConvertTo<string>();
                item.PidTypeDesc = dt.Rows[i]["pid_type_desc"].ConvertTo<string>();
                item.Remark = dt.Rows[i]["remark"].ConvertTo<string>();
                item.OpenDate = dt.Rows[i]["Open_Date"].ConvertTo<DateTime?>();
            }

            return item;
        }

        private List<CallCenterGetAccountBankSettleResult> GetCallCenterGetAccountBankSettleResults(DataTable dt)
        {
            var list = new List<CallCenterGetAccountBankSettleResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountBankSettleResult()
                    {
                        AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>(),
                        AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>(),

                        ATSBuyAccountName = dt.Rows[i]["ATS_Buy_Account_Name"].ConvertTo<string>(),
                        ATSBuyAccountType = dt.Rows[i]["ATS_Buy_Account_Type"].ConvertTo<string>(),
                        ATSBuyAccountTypeDesc = dt.Rows[i]["ATS_Buy_Account_Type_desc"].ConvertTo<string>(),
                        ATSBuyBankAccount = dt.Rows[i]["ATS_Buy_Bank_Account"].ConvertTo<string>(),
                        AtsBuyBankId = dt.Rows[i]["ats_buy_bank_id"].ConvertTo<int?>(),
                        ATSBuyBranchID = dt.Rows[i]["ATS_Buy_Branch_ID"].ConvertTo<int?>(),
                        AtsBuyBranchName = dt.Rows[i]["ats_buy_branch_name"].ConvertTo<string>(),

                        ATSSellAccountName = dt.Rows[i]["ATS_sell_Account_Name"].ConvertTo<string>(),
                        ATSSellAccountType = dt.Rows[i]["ATS_sell_Account_Type"].ConvertTo<string>(),
                        ATSSellAccountTypeDesc = dt.Rows[i]["ATS_sell_Account_Type_desc"].ConvertTo<string>(),
                        ATSSellBankAccount = dt.Rows[i]["ATS_sell_Bank_Account"].ConvertTo<string>(),
                        AtsSellBankId = dt.Rows[i]["ats_sell_bank_id"].ConvertTo<int?>(),
                        ATSSellBranchID = dt.Rows[i]["ATS_sell_Branch_ID"].ConvertTo<int?>(),
                        AtsSellBranchName = dt.Rows[i]["ats_sell_branch_name"].ConvertTo<string>(),

                        BankBuySname = dt.Rows[i]["bank_buy_sname"].ConvertTo<string>(),
                        BankSellSname = dt.Rows[i]["bank_sell_sname"].ConvertTo<string>(),
                        BuyPayType = dt.Rows[i]["buy_pay_type"].ConvertTo<int?>(),
                        BuyPayTypeDesc = dt.Rows[i]["buy_pay_type_desc"].ConvertTo<string>(),
                        FundCode = dt.Rows[i]["fund_code"].ConvertTo<string>(),
                        SellPayType = dt.Rows[i]["Sell_Pay_Type"].ConvertTo<int?>(),
                        SellPayTypeDesc = dt.Rows[i]["sell_pay_type_desc"].ConvertTo<string>()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetAccountMutualFundResult> GetCallCenterGetAccountMutualFundResults(DataTable dt)
        {
            var list = new List<CallCenterGetAccountMutualFundResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetAccountMutualFundResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>();
                    item.HolderId = dt.Rows[i]["holder_id"].ConvertTo<string>();
                    item.HolderNameE = dt.Rows[i]["holder_name_e"].ConvertTo<string>();
                    item.HolderNameT = dt.Rows[i]["holder_name_t"].ConvertTo<string>();
                    item.StatusId = dt.Rows[i]["status_id"].ConvertTo<int?>();
                    item.StatusDesc = dt.Rows[i]["status_desc"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetCashTranResult> GetCallCenterGetCashTranResults(DataTable dt)
        {
            var list = new List<CallCenterGetCashTranResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetCashTranResult();
                    item.Account = dt.Rows[i]["account"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.Sharecodes = dt.Rows[i]["sharecodes"].ConvertTo<string>();
                    item.Trandesc = dt.Rows[i]["trandesc"].ConvertTo<string>();
                    item.Amt = dt.Rows[i]["amt"].ConvertTo<decimal?>();
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetEdividendResult> GetCallCenterGetEdividendResults(DataTable dt)
        {
            var list = new List<CallCenterGetEdividendResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetEdividendResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.DvdBankShortname = dt.Rows[i]["dvd_bank_shortname"].ConvertTo<string>();
                    item.DvdBankAccountNo = dt.Rows[i]["dvd_bank_account_no"].ConvertTo<string>();
                    item.DbdBankAccountTypeDesc = dt.Rows[i]["dbd_bank_account_type_desc"].ConvertTo<string>();
                    item.DvdBranchName = dt.Rows[i]["dvd_branch_name"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetEquNetBuySellResult> GetCallCenterGetEquNetBuySellResults(DataTable dt)
        {
            var list = new List<CallCenterGetEquNetBuySellResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetEquNetBuySellResult();
                    item.Account = dt.Rows[i]["account"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.Settdate = dt.Rows[i]["settdate"].ConvertTo<DateTime>();
                    item.AssetType = dt.Rows[i]["AssetType"].ConvertTo<string>();
                    item.NetDesc = dt.Rows[i]["net_desc"].ConvertTo<string>();
                    item.ClientReceive = dt.Rows[i]["client_receive"].ConvertTo<decimal?>();
                    item.ClientPay = dt.Rows[i]["client_pay"].ConvertTo<decimal?>();
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetMfTranResult> GetCallCenterGetMfTranResults(DataTable dt)
        {
            var list = new List<CallCenterGetMfTranResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetMfTranResult();
                    item.AccountNo = dt.Rows[i]["account_no"].ConvertTo<string>();
                    item.HolderId = dt.Rows[i]["holder_id"].ConvertTo<string>();
                    item.RefNo = dt.Rows[i]["ref_no"].ConvertTo<string>();
                    item.AmcCode = dt.Rows[i]["amc_code"].ConvertTo<string>();
                    item.FundCode = dt.Rows[i]["fund_code"].ConvertTo<string>();
                    item.TrantypeCode = dt.Rows[i]["trantype_code"].ConvertTo<string>();
                    item.Tradedate = dt.Rows[i]["tradedate"].ConvertTo<DateTime?>();
                    item.AmountUnit = dt.Rows[i]["amount_unit"].ConvertTo<decimal?>();
                    item.AmountBaht = dt.Rows[i]["amount_baht"].ConvertTo<decimal?>();
                    item.TranStatusId = dt.Rows[i]["tran_status_id"].ConvertTo<int?>();
                    item.TranStatusDesc = dt.Rows[i]["tran_status_desc"].ConvertTo<string>();
                    list.Add(item);
                }
            }
            return list;
        }

        private List<CallCenterGetLoginResult> GetCallCenterGetLoginResults(DataTable dt)
        {
            var list = new List<CallCenterGetLoginResult>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new CallCenterGetLoginResult()
                    {
                        UserName = dt.Rows[i]["UserName"].ConvertTo<string>(),
                        PinStatus = dt.Rows[i]["PinStatus"].ConvertTo<string>(),
                        LoginStatus = dt.Rows[i]["LoginStatus"].ConvertTo<string>()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        #endregion
    }

    public interface ICrmManager
    {
        CallCenterGetProfileResult GetCallCenterGetProfileResult(decimal? clientId);
        List<CallCenterGetAddrInfoResult> GetCallCenterGetAddrInfoResults(decimal? clientId);
        List<CallCenterGetAccountListResult> GetCallCenterGetAccountListResults(decimal? clientId);
        List<CallCenterGetPoaResult> GetCallCenterGetPoaResults(decimal? clientId);
        CallCenterGetBankingTabStatusResult GetCallCenterGetBankingTabStatusResult(decimal? clientId);


        CallCenterGetAccountInfoResult GetCallCenterGetAccountInfoResult(string accountNo, string accountType);
        List<CallCenterGetAccountBankSettleResult> GetCallCenterGetAccountBankSettleResults(string accountNo);
        List<CallCenterGetAccountMutualFundResult> GetCallCenterGetAccountMutualFundResults(string accountNo);
        List<CallCenterGetCashTranResult> GetCallCenterGetCashTranResults(string accountNo);
        List<CallCenterGetEdividendResult> GetCallCenterGetEdividendResults(string accountNo);
        List<CallCenterGetEquNetBuySellResult> GetCallCenterGetEquNetBuySellResults(string accountNo);
        List<CallCenterGetMfTranResult> GetCallCenterGetMfTranResults(string accountNo);
        List<CallCenterGetLoginResult> GetCallCenterGetLoginResults(string accountNo);

        List<RefGetPurposeVerificationsResult> GetRefGetPurposeVerificationsResults();
        List<CallCenterGetQuestionVerificationResult> GetCallCenterGetQuestionVerificationResults(int proposeId);
        void SaveVerificationsResult(string callSession, List<VerificationResult> verificationResults);


        List<CallCenterGetReminderResult> GetCallCenterGetReminderResultsByClientId(decimal? clientId);
        CallCenterGetReminderResult GetCallCenterGetReminderResultsById(int? reminderId);
        void SaveReminder(int? reminderId, CallCenterGetReminderResult model);
        void CancelReminder(int? reminderId, DateTime updatedDate, string updatedUser);

        List<CallCenterGetHistoryWrapUpInfoResult> GetCallCenterGetHistoryWrapUpByClientId(decimal? clientId);

        CallCenterAccountInfo GetCallCenterAccountInfo(string accountNo, string accountType);

        CallCenterGetWrapUpInfoResult GetCallCenterGetWrapUpInfoResult(string callSession);
        List<CallCenterGetWrapupReasonResult> GetCallCenterGetWrapupReasonResults();

        void SaveWrapUp(string callSession, string agentId, string customerPhone,
            string originalAccountNo, decimal? originalClientId, decimal? clientId, string accountNo, DateTime callStartTime,
            string wrapUpText, DateTime? wrapUpDateTime, decimal? poaId, string reasonDesc);


        List<CallCenterGetSearchClientResult> GetCallCenterGetSearchClientResults(string searchText);
        List<CallCenterGetAccountPortfolioInfoResult> GetCallCenterGetAccountPortfolioResults(string accountNumber);

        List<CallCenterGetDrilldownLoginResult> GetCallCenterGetDrilldownLoginResults(string login);
        List<CallCenterGetClientListFromPhonenoResult> GetCallCenterGetClientListFromPhonenoResults(string customerPhone);

        List<CallCenterGetMissedCallListResult> GetCallCenterGetMissedCallListResults();

        string SaveCallLogLnfo(CallLogInfoItem data); 

    }
}
