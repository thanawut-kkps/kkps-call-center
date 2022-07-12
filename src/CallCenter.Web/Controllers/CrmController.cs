using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Core.Logging;
using Phatra.CallCenter.Managers;
using CallCenter.Web.ViewModels;
using Phatra.CallCenter.Data;
using Phatra.Core.Extensions;
using MvcPaging;
using System.Net.Http;
using System.Net.Http.Headers;
using Phatra.Core.Utilities;
using Phatra.CallCenter.Exceptions;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Net.Security;
using System.Collections;
using System.Threading;
using RestSharp;

namespace CallCenter.Web.Controllers
{
    public class CrmController : BaseCallCenterController
    {
        private ILog _log;
        private ICrmManager _crmManager;

        public CrmController(ILog log, ICrmManager crmManager)
        {
            this._log = log;
            this._crmManager = crmManager;
        }

        #region "  Client Info "

        //http://localhost:20352/Crm?client_id=85&accountno=85&session=sssss&agent_id=65221

        [HttpGet]
        public ActionResult Index(string session, string agent_id, string extension, string customer_phone,
            string call_starttime,  string client_id, string accountno, string nclientid, string naccountno, string prdct)
        {

            _log.Debug("Starting Index(session:'{0}', agent_id:'{1}', extension:'{2}', customer_phone:'{3}', call_starttime:'{4}', client_id:'{5}', accountno:'{6}', nclientid:'{7}', naccountno:'{8}', prdct:'{9}') ...",  
                                session,  agent_id,  extension,  customer_phone,
                                call_starttime,   client_id,  accountno,  nclientid,  naccountno,  prdct);

            if (string.IsNullOrWhiteSpace(agent_id))
            {
                this.Error("Agent ID is not sent by Cisco Finesse");
            }

            if (string.IsNullOrWhiteSpace(session))
            {
                this.Error("Session ID is not sent by Cisco Finesse");
            }

            decimal? clientId = null;
            decimal? nclientId = null;

            if (!string.IsNullOrWhiteSpace(nclientid) || (!string.IsNullOrWhiteSpace(client_id) && client_id != "-1"))
            {
                if (session.ToLower() == "init".ToLower())
                {                   
                    session = session.ToLower() + Guid.NewGuid().ToString();
                    _log.Debug("[INIT] => New Session:'{0}'", session);
                }
                else if (session.ToLower() == "outbound".ToLower())
                {
                    session = session.ToLower() + Guid.NewGuid().ToString();
                    _log.Debug("[OUTBOUND] => New Session:'{0}'", session);
                }


                if (!string.IsNullOrWhiteSpace(client_id))
                {
                    clientId = Convert.ToDecimal(client_id);
                }
                if (!string.IsNullOrWhiteSpace(nclientid))
                {
                    nclientId = Convert.ToDecimal(nclientid);
                }

                var viewModel = new ClientInfoViewModel()
                {
                    ClientId = nclientId ?? clientId,
                    AccountNo = !string.IsNullOrWhiteSpace(naccountno) ? naccountno : accountno,
                    AgentId = agent_id,
                    CallSession = session,
                    CallStartTime = call_starttime,
                    CustomerPhone = customer_phone,
                    Extension = extension,
                    OriginalAccountNo = accountno,
                    OriginalClientId = clientId,
                    Product = prdct
                };

                return View(viewModel);

            }
            else if (client_id == "-1" && !string.IsNullOrWhiteSpace(customer_phone)) 
            {
                object phoneNo = customer_phone;
                return View("ClientSelection", phoneNo);
            }
            else // if (string.IsNullOrWhiteSpace(client_id) && string.IsNullOrWhiteSpace(nclientid))
            {
                return View("SearchClient");
            }
        }

        [HttpPost]
        public ActionResult Index(string accountno, string session,
            string call_starttime, string customer_phone, string agent_id, 
            string extension, string client_id)
        {

            if (string.IsNullOrWhiteSpace(agent_id) )
            {
                this.Error("Agent ID is not sent by Cisco Finesse");
            }

            if (string.IsNullOrWhiteSpace(session))
            {
                this.Error("Session ID is not sent by Cisco Finesse");
            }

            decimal? clientId = null;
            decimal? nclientId = null;

            if (string.IsNullOrWhiteSpace(client_id))
            {
                return View("SearchClient");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(client_id))
                {
                    clientId = Convert.ToDecimal(client_id);
                }
            }

            var viewModel = new ClientInfoViewModel()
            {
                ClientId = nclientId ?? clientId,
                AccountNo = accountno,
                AgentId = agent_id,
                CallSession = session,
                CallStartTime = call_starttime,
                CustomerPhone = customer_phone,
                Extension = extension,
                OriginalAccountNo = accountno,
                OriginalClientId = clientId
            };

            return View(viewModel);
        }

        public ActionResult IndexCallbackPanelPartial(decimal? _originalClientId, decimal? _clientId, string _accountNo, string _callSession, 
            string _product, string _agentId, string _callStartTime)
        {
            var viewModel = new ClientInfoViewModel()
            {
                OriginalClientId = _originalClientId,
                ClientId = _clientId,
                AccountNo = _accountNo,
                CallSession = _callSession,
                Product = _product,
                AgentId = _agentId,
                CallStartTime = _callStartTime
            };

            return PartialView("Index.CallbackPanelPartial", viewModel);
        }

        public ActionResult IndexClientInfoPartial(decimal? _originalClientId ,decimal? _clientId, string _accountNo, string _product)
        {
            var profile = _crmManager.GetCallCenterGetProfileResult(_clientId) ?? new CallCenterGetProfileResult();
            var addresses = _crmManager.GetCallCenterGetAddrInfoResults(_clientId);
            var accounts = _crmManager.GetCallCenterGetAccountListResults(_clientId);
            var poas = _crmManager.GetCallCenterGetPoaResults(_clientId).OrderBy(c => c.POANameT).ToList();
            var bankingTab = _crmManager.GetCallCenterGetBankingTabStatusResult(_clientId);

            var viewModel = new ClientInfoViewModel()
            {
                OriginalClientId = _originalClientId,
                AccountNo = _accountNo,
                ClientId = _clientId,
                Product = _product,
                ProfileResult = profile,
                AddressResults = addresses,
                AccountResults = accounts,
                PoaResults = poas,
                BankingTabStatusResult = bankingTab
            };

            return PartialView("Index.ClientInfoPartial", viewModel);
        }

        //public ActionResult IndexProfilePartial(decimal? _clientId)
        //{
        //    var profile = _crmManager.GetCallCenterGetProfileResult(_clientId);

        //    //var viewModel = new ClientInfoViewModel()
        //    //{
        //    //    ClientId = _clientId,
        //    //    ProfileResult = profile
        //    //};
        //    return PartialView("Index.ProfilePartial", profile);
        //}

        public ActionResult IndexContactInformationPartial(decimal? _clientId)
        {
            var result = _crmManager.GetCallCenterGetProfileResult(_clientId);
            return PartialView("Index.ProfilePartial", result);
        }

        public ActionResult IndexAccountInfoContainerPartial(string _accountNo, string _accountType)
        {
            _log.Debug("Starting to IndexAccountInfoContainerPartial({0}, {1})..", _accountNo, _accountType);
            //_accountNo = "20351052";
            //_accountType = "Single A/C";

            AccountInfoViewModel viewModel = null;

            if (!string.IsNullOrWhiteSpace(_accountNo))
            {

                var accountInfo = _crmManager.GetCallCenterAccountInfo(_accountNo, _accountType);

                viewModel = new AccountInfoViewModel()
                {
                    AccountNo = _accountNo,
                    AccountType = _accountType,
                    AccountInfoResult = accountInfo.CallCenterGetAccountInfoResult ?? new CallCenterGetAccountInfoResult(),
                    BankSettlementResult = accountInfo.CallCenterGetAccountBankSettleResults.SingleOrDefault() ?? new CallCenterGetAccountBankSettleResult(),
                    EdividendResult = accountInfo.CallCenterGetEdividendResults.SingleOrDefault() ?? new CallCenterGetEdividendResult(),
                    MutualFundResults = accountInfo.CallCenterGetAccountMutualFundResults,
                    CashTranResults = accountInfo.CallCenterGetCashTranResults,
                    EquNetBuySellResults = accountInfo.CallCenterGetEquNetBuySellResults,
                    MfTranResults = accountInfo.GetCallCenterGetMfTranResults,
                    LoginResults = accountInfo.GetCallCenterGetLoginResults
                };

                //AccountInfoViewModel viewModel = new AccountInfoViewModel()
                //{
                //    AccountNo = _accountNo,
                //    AccountType = _accountType,
                //    AccountInfoResult = _crmManager.GetCallCenterGetAccountInfoResult(_accountNo, _accountType) ?? new CallCenterGetAccountInfoResult(),
                //    BankSettlementResult = _crmManager.GetCallCenterGetAccountBankSettleResults(_accountNo).SingleOrDefault() ?? new CallCenterGetAccountBankSettleResult(),
                //    EdividendResult = _crmManager.GetCallCenterGetEdividendResults(_accountNo).SingleOrDefault() ?? new CallCenterGetEdividendResult(),
                //    MutualFundResults = _crmManager.GetCallCenterGetAccountMutualFundResults(_accountNo),
                //    CashTranResults = _crmManager.GetCallCenterGetCashTranResults(_accountNo),
                //    MfTranResults = _crmManager.GetCallCenterGetMfTranResults(_accountNo),
                //};

            }
            else
            {
                viewModel = new AccountInfoViewModel();
            }

            _log.Debug("End of IndexAccountInfoContainerPartial({0}, {1})..", _accountNo, _accountType);

            return PartialView("Index.AccountInfoContainerPartial", viewModel);
        }

        public ActionResult IndexUserNameDrilldownListPartialJson(string _userName)
        {
            var viewModel = _crmManager.GetCallCenterGetDrilldownLoginResults(_userName); 
            return Json(new { valid = true, message = this.RenderPartialViewToString("Index.UserNameDrilldownListPartial", viewModel) });
        }

        #region Wrap Up

        public ActionResult IndexWrapUpPartial(decimal? _clientId, string _accountNo,
            decimal? _originalClientId, string _agentId, string _callSession, string _callStartTime, 
            string _customerPhone, string _extension, string _originalAccountNo)
        {

            WrapUpViewModel viewModel = new WrapUpViewModel()
            {
                AgentId = _agentId,
                CallSession = _callSession,
                CallStartTime = _callStartTime,
                ClientId = _clientId,
                CustomerPhone = _customerPhone,
                Extension = _extension,
                OriginalClientId = _originalClientId,
                OriginalAccountNo = _originalAccountNo,
                WrapupDatetime = DateTime.Today,
                             
            };

            var model = _crmManager.GetCallCenterGetWrapUpInfoResult(_callSession);
            ViewData["ReasonList"] = _crmManager.GetCallCenterGetWrapupReasonResults()
                                        .Select(c => new SelectListItem() { Value = c.ReasonDesc, Text = c.ReasonDesc });

            if (model != null)
            {
                viewModel.WrapupText = model.WrapupText;
                viewModel.WrapupAccountNo = model.AccountNo;
                viewModel.PoaId = model.PoaId;
            }
            else 
            {
                viewModel.WrapupAccountNo = _accountNo;
                viewModel.WrapupText = "";
            }


            return PartialView("Index.WrapUpPartial", viewModel);
        }

        public ActionResult SaveWrapUp(WrapUpViewModel viewModel)
        {
            _log.Debug("Starting to SaveWrapUp() ...");
            var isValid = false;
            var returnMessage = string.Empty;

            try
            {

                DateTime callStartTime;
                if (!DateTime.TryParseExact(viewModel.CallStartTime, "HH:mm:ss tt M/d/yy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out callStartTime))
                {
                    callStartTime = DateTime.Now;
                }


                _log.Debug("crmManager.SaveWrapUp(callSession:{0}, AgentId:{1}, CustomerPhone:{2}, OriginalAccountNo:{3}, OriginalClientId:{4}, ClientId:{5}, WrapupText:'{6}') ...",
                        viewModel.CallSession, viewModel.AgentId, viewModel.CustomerPhone, viewModel.OriginalAccountNo, viewModel.OriginalClientId, viewModel.ClientId, viewModel.WrapupText);
                _crmManager.SaveWrapUp(viewModel.CallSession, viewModel.AgentId, viewModel.CustomerPhone,
                            viewModel.OriginalAccountNo, viewModel.OriginalClientId, viewModel.ClientId,
                            viewModel.WrapupAccountNo, callStartTime, viewModel.WrapupText, viewModel.WrapupDatetime, viewModel.PoaId, viewModel.ReasonDesc);


                if (viewModel.CallSession.ToLower().StartsWith("outbound"))
                { 
                    var callLog = new CallLogInfoItem()
                    {
                        agent_extension = viewModel.Extension,
                        agent_id  =  viewModel.AgentId,
                        call_session = viewModel.CallSession,
                        call_starttime = callStartTime,
                        call_type = viewModel.IsCallbackSuccess ? "4": "3",
                        client_id = viewModel.ClientId,
                        customer_phone = viewModel.CustomerPhone,
                        priority = 1
                    };
                    _crmManager.SaveCallLogLnfo(callLog);
                }

                //NotifyFinesseToFinishWrapUp(viewModel.AgentId);

                isValid = true;
            }
            catch (Exception ex)
            {
                isValid = false;
                returnMessage = ex.Message;
                _log.Error("{0}", ex.ToString());
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { valid = isValid, message = returnMessage });
            }
            else
            {
                return RedirectToAction("Index", new {
                                                accountno = viewModel.OriginalAccountNo,
                                                session = viewModel.CallSession,
                                                call_starttime = viewModel.CallStartTime,
                                                customer_phone = viewModel.CustomerPhone,
                                                agent_id = viewModel.AgentId,
                                                extension = viewModel.Extension,
                                                client_id = viewModel.ClientId
                                            });
            }
        }

        #endregion

        #region Reminder

        public ActionResult IndexCallbackPanelReminderListPartial(decimal? _clientId)
        {
            return PartialView("Index.CallbackPanelReminderListPartial", _clientId);
        }

        public ActionResult IndexReminderListPartial(decimal? _clientId)
        {
            var model = _crmManager.GetCallCenterGetReminderResultsByClientId(_clientId);
            return PartialView("Index.ReminderListPartial", model);
        }

        public ActionResult IndexReminderFormPartial(int? _reminderId)
        {
            var model = _crmManager.GetCallCenterGetReminderResultsById(_reminderId);

            ReminderViewModel viewModel = null;

            if (model != null)
            {
                viewModel = new ReminderViewModel()
                {
                    ReminderId = model.ReminderId,
                    AccountNo = model.AccountNo,
                    CallSession = model.CallSession,
                    ClientId = model.ClientId,
                    CreatedDate = model.CreatedDate,
                    CreatedUser = model.CreatedUser,
                    CustomerPhone = model.CustomerPhone,
                    Details = model.Details,
                    IsCancelled = model.IsCancelled,
                    RemindDate = model.RemindDate,
                    UpdatedDate = model.UpdatedDate,
                    UpdatedUser = model.UpdatedUser 
                };
            }
            else
            {
                viewModel = new ReminderViewModel();
                viewModel.RemindDate = DateTime.Now;
            }            

            if (Request.IsAjaxRequest())
            {
                return Json(new { valid = true, message = this.RenderPartialViewToString("Index.ReminderFormPartial", viewModel) });
            }
            return PartialView("Index.ReminderFormPartial", viewModel);
        }

        [HttpPost]
        public ActionResult SaveReminder(ReminderViewModel viewModel)
        {

            if (viewModel.RemindDate.HasValue)
            {
                if (viewModel.RemindDate.Value.Date < DateTime.Now.Date)
                {
                    throw new GeneralBusinessException("Remind date is not allowed less than today.");
                }
            }

            CallCenterGetReminderResult model = new CallCenterGetReminderResult()
            {
                AccountNo = viewModel.AccountNo,
                CallSession = viewModel.CallSession,
                ClientId = viewModel.ClientId,
                CustomerPhone = viewModel.CustomerPhone,
                Details = viewModel.Details,
                IsCancelled = false,
                RemindDate = viewModel.RemindDate,
                UpdatedDate = DateTime.Now,
                UpdatedUser = viewModel.AgentId
            };

            _crmManager.SaveReminder(viewModel.ReminderId,  model);

            if (Request.IsAjaxRequest())
            {
                return Json(new { valid = true, message = "" });
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CancelReminder(int? _reminderId, string _agentId)
        {
            if (!_reminderId.HasValue) 
            {
                throw new ArgumentNullException("_reminderId");
            }
            _crmManager.CancelReminder(_reminderId, DateTime.Now, _agentId);

            if (Request.IsAjaxRequest())
            {
                return Json(new { valid = true, message = "" });
            }
            return View("");
        }

        #endregion

        #region Verification

        public ActionResult IndexVerificationPartial(decimal? _clientId)
        {
            var viewModel = new VerificationViewModel();
            viewModel.PurposeId = 1;
            ViewData["PurposeList"] = _crmManager.GetRefGetPurposeVerificationsResults()
                    //.Select(c => new SelectListItem() 
                    //    { 
                    //        Text = c.PurposeDesc, 
                    //        Value = c.PurposeId.ToString() 
                    //    }).ToList()
                ;

            return PartialView("Index.VerificationPartial", viewModel);
        }

        public ActionResult IndexVerificationQuestionListCallbackPanelPartial(int? _purposeId)
        {
            return PartialView("Index.VerificationQuestionListCallbackPanelPartial", _purposeId);
        }

        public ActionResult IndexVerificationQuestionListPartial(int? _purposeId)
        {
            var viewModels = _crmManager.GetCallCenterGetQuestionVerificationResults(_purposeId ?? -1);

            return PartialView("Index.VerificationQuestionListPartial", viewModels);
        }

        public ActionResult SaveVerificationResult(List<VerificationResultViewModel> _verificationResults, 
            string _callSession, decimal? _clientId, string _customerPhone, string _agentId)
        {
            var isValid = false;
            var returnMessage = string.Empty;
            _log.Debug("Starting to SaveVerificationResult({0}, {1}, {2}, {3})..", _callSession, _clientId, _customerPhone, _agentId);
            try
            {

                List<VerificationResult> models = new List<VerificationResult>();

                foreach (var item in _verificationResults)
                {
                    var model = new VerificationResult();
                    model.AgentId = _agentId;
                    model.CallSession = _callSession;
                    model.ClientId = _clientId;
                    model.CustomerPhone = _customerPhone;
                    model.IsPassed = item.Result == "Passed";
                    model.PurposeId = item.PurposeId;
                    model.QuestionId = item.QuestionId;
                    models.Add(model);
                }
                _crmManager.SaveVerificationsResult(_callSession, models); 

                isValid = true;
            }
            catch (Exception ex)
            {
                isValid = false;
                returnMessage = ex.Message;
                _log.Error("{0}", ex.ToString());
            }

     
            return Json(new { valid = isValid, message = returnMessage });

        }

        #endregion

        #region Private Methods

        public void NotifyFinesseToFinishWrapUp(string agentId)
        {
            HttpClient client = new HttpClient();

            string agentSetStateUrl = ConfigurationHelper.GetAppSettingValue("AgentSetStateUrl");

            string Url = string.Format("{0}", agentSetStateUrl);
            string urlParameters = string.Format("?agent_id={0}&state=ready", agentId);

            client.BaseAddress = new Uri(Url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  
            if (response.IsSuccessStatusCode)
            {
                _log.Debug("Success to Notify Finesse To Finish WrapUp");
            }
            else
            {
                _log.Error("Fail to Notify Finesse To Finish WrapUp");
            }  
        }

        public JsonResult GetFinesseUserState(string _agentId)
        {
            _log.Debug("Starting to GetFinesseUserState('{0}')...", _agentId);

            var result = GetAgentState(_agentId);

            if (result.Contains("Service Unavailable") || result.Contains("Web Exception"))
            {
                _log.Warn("{0}", result);
                result = GetAgentState(_agentId);
            }

            _log.Debug("GetFinesseUserState('{0}') = '{1}'", _agentId, result);

            return Json(new { valid = true, message = result }, JsonRequestBehavior.AllowGet);
        }

        private string GetAgentState(string agentId)
        {
            //string agentGetStateUrl = ConfigurationHelper.GetAppSettingValue("AgentGetStateUrl");
            string result = string.Empty;

            var user = Phatra.Finesse.DesktopAPIs.UserApiHelper.Instance.GetUser(agentId);

            result = user.State;

            //try
            //{
            //    var request = (HttpWebRequest)WebRequest.Create(agentGetStateUrl);
            //    var postData = "agent_id=" + agentId;
            //    var data = Encoding.ASCII.GetBytes(postData);

            //    if (agentGetStateUrl.StartsWith("https"))
            //    {
            //        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
            //        (
            //           delegate { return true; }
            //        );
            //    }

            //    request.Method = "POST";
            //    request.ContentType = "application/x-www-form-urlencoded";
            //    request.ContentLength = data.Length;

            //    using (var stream = request.GetRequestStream())
            //    {
            //        stream.Write(data, 0, data.Length);
            //    }

            //    var response = (HttpWebResponse)request.GetResponse();

            //    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //    XmlDocument xmlDoc = new XmlDocument();
            //    xmlDoc.LoadXml(responseString);

            //    XmlNodeList elemlist = xmlDoc.GetElementsByTagName("RESULT");
            //    result = elemlist[0].InnerXml;
            //}
            //catch(Exception ex)
            //{
            //    _log.Error("ERROR GetAgentState('{0}'): {1}", agentId, ex.ToString());
            //}

            return result;
        }

        #endregion

        #region History Wrapup

        public ActionResult IndexCallbackPanelHistoryWrapupPartial(decimal? _clientId)
        {
            return PartialView("Index.CallbackPanelHistoryWrapupPartial", _clientId);
        }

        public ActionResult IndexHistoryWrapupPartial(decimal? _clientId)
        {            
            var model = _crmManager.GetCallCenterGetHistoryWrapUpByClientId(_clientId);
            return PartialView("Index.HistoryWrapupPartial", model);
        }        
        #endregion

        #endregion

        public ActionResult KKDepositBalance(string selectedAccountNo)
        {
            var KKPIBServiceURL = ConfigurationHelper.GetAppSettingValue("KKDepositUrl");
            var rclient = new RestClient(KKPIBServiceURL);
            var request = new RestRequest(Method.POST);
            request.AddParameter("AccountNo", selectedAccountNo);
            try
            {
                var rclientResponse = rclient.Execute(request);
            }
            catch (Exception)
            {
            }

            return null;
        }

        #region AccountPortfolio
        public JsonResult AccountPortfolioPartialJson(string selectedAccountNo, decimal? clientID)
        {                        
            var accountListInfoResult = _crmManager.GetCallCenterGetAccountListResults(clientID);
            var accountList = (from x in accountListInfoResult
                                    group x by x.AccountNo into y
                                    select new SelectListItem() 
                                                {
                                                    Text = y.Key,
                                                    Value = y.Key 
                                                }).ToList();

            if (selectedAccountNo == "" && accountList.Count > 0)
            {
                selectedAccountNo = accountList[0].Value;
            }

            var accountInfo = (from x in accountListInfoResult
                               where x.AccountNo == selectedAccountNo
                               orderby x.AccStatus descending, x.SuitLevel descending
                               select x).FirstOrDefault();

            if (accountInfo != null)
            {
                ViewData["accountName"] = accountInfo.AccountNameT;
                ViewData["suitLevel"] = accountInfo.SuitLevel;
                ViewData["accountStatus"] = accountInfo.AccStatus;
            }

            ViewData["accountList"] = accountList;
            ViewData["AccountPortfolioList"] = selectedAccountNo;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data["selectedAccountNo"] = selectedAccountNo;
            data["clientID"] = clientID;
            return Json(new { valid = true, message = this.RenderPartialViewToString("AccountPortfolio.Partial", data) });
        }

        public ActionResult AccountPortfolioCallbackPanelResultPartial(string selectedAccountNo, string clientID)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["selectedAccountNo"] = selectedAccountNo;
            data["clientID"] = clientID;

            return PartialView("AccountPortfolio.CallbackPanelResultPartial", data);
        }

        public ActionResult AccountPortfolioResultPartial(string _accountNumber, string _clientID, int page = 1, int pageSize = 20)
        {
            ViewData["_accountNumber"] = _accountNumber;
            ViewData["_clientID"] = _clientID;
            var allTxn = _crmManager.GetCallCenterGetAccountPortfolioResults(_accountNumber);

            var txnSumByClass = 
                            (from x in allTxn
                            group x by x.Classdesc into ClassGroup
                            select new
                            {
                                className = ClassGroup.Key,
                                totalCost = ClassGroup.Sum(x => x.Ctotcost),
                                totalValue = ClassGroup.Sum(x => x.Mktvalue),
                                totalWeight = ClassGroup.Sum(x => x.WeightPct),
                                totalUGL = ClassGroup.Sum(x => x.Ugl),
                            }).ToList();

            decimal? totalPortfolioCost;
            decimal? totalPortfolioValue;
            decimal? totalPortfolioWeight;
            decimal? totalPortfolioUGL;

            totalPortfolioCost = (from txn in allTxn select txn.Ctotcost).Sum();
            totalPortfolioValue = (from txn in allTxn select txn.Mktvalue).Sum();
            totalPortfolioWeight = (from txn in allTxn select txn.WeightPct).Sum();
            totalPortfolioUGL = (from txn in allTxn select txn.Ugl).Sum();
            
            var result = from x in txnSumByClass 
                     join y in allTxn 
                        on x.className equals y.Classdesc 
                    orderby
                        y.AssetLib,
                        y.Classord,
                        y.ShareCodes
                     select
                        new AccountPortfolioInfoViewModel
                        {
                            Account = y.Account,
                            Turnoveracc = y.Turnoveracc,
                            AssetLib = y.AssetLib,
                            Viewby = y.Viewby,
                            Grpord = y.Grpord,
                            Grpname = y.Grpname,
                            Classord = y.Classord,
                            Classcode = y.Classcode,
                            Classdesc = y.Classdesc,
                            Subgrp1 = y.Subgrp1,
                            Grp1ord = y.Grp1ord,
                            Posdate = y.Posdate,
                            Porttype = y.Porttype,
                            Desk = y.Desk,
                            AType = y.AType,
                            AmcCode = y.AmcCode,
                            ShareCode = y.ShareCode,
                            ShareCodes = y.ShareCodes,
                            CUnitBal = y.CUnitBal,
                            Cavgcost = y.Cavgcost,
                            Ctotcost = y.Ctotcost,
                            Mktprice = y.Mktprice,
                            Mktvalue = y.Mktvalue,
                            Ugl = y.Ugl,
                            UglPct = y.UglPct,
                            Totmkt = y.Totmkt,
                            WeightPct = y.WeightPct,
                            Flag = y.Flag,
                            NoDec = y.NoDec,
                            TotalCost = x.totalCost,
                            TotalValue = x.totalValue,
                            TotalWeight = x.totalWeight,
                            TotalUnrealizedGainLoss = x.totalUGL,
                            TotalPortfolioCost = totalPortfolioCost,
                            TotalPortfolioValue = totalPortfolioValue,
                            TotalPortfolioWeight = totalPortfolioWeight,
                            TotalPortfolioUnrealizedGainLoss = totalPortfolioUGL
                        };

            return PartialView("AccountPortfolio.ResultPartial", result.ToPagedList(page, pageSize));
        }
        #endregion

        #region Search Client

        public JsonResult SearchClientPartialJson()
        {
            string defualtSearchText = "";
            return Json(new { valid = true, message = this.RenderPartialViewToString("SearchClient.Partial", defualtSearchText) });
        }        
        
        public ActionResult SearchClientPartial(string session,
                        string callTime, string telephone, string agentno,
                        string agent_id)
        {
            string defualtSearchText = "";

            return PartialView("SearchClient.Partial", defualtSearchText);
        }

        public ActionResult SearchClientCallbackPanelResultPartial(string _searchText)
        {
            return PartialView("SearchClient.CallbackPanelResultPartial", _searchText);
        }

        public ActionResult SearchClientResultPartial(string _searchText, int page = 1, int pageSize = 10)
        {
            ViewData["SearchText"] = _searchText;

            var viewModel = _crmManager.GetCallCenterGetSearchClientResults(_searchText); 


            //var results = new List<SearchClientItemResultViewModel>();

            //for (int i = 0; i < 401; i++)
            //{
            //    results.Add(new SearchClientItemResultViewModel()
            //    {
            //        AccountNo = "20351052",
            //        ClientId = i,
            //        EngName = string.Format("EngName#{0}", i),
            //        PidNo =  i.ToString().PadLeft(13,'0'),
            //        ThaiName = string.Format("ThaiName#{0}", i),
            //        Trader = "Trader#1"
            //    });
            //}

            return PartialView("SearchClient.ResultPartial", viewModel.ToPagedList(page, pageSize));
        }

        public ActionResult ClientSelectionClientListPartial(string _custormerPhone)
        {
            var viewModel = _crmManager.GetCallCenterGetClientListFromPhonenoResults(_custormerPhone);
            return PartialView("ClientSelection.ClientListPartial", viewModel);
        }

        public ActionResult SearchClientWrapUpParial(decimal? _clientId, string _accountNo,
            decimal? _originalClientId, string _agentId, string _callSession, string _callStartTime,
            string _customerPhone, string _extension, string _originalAccountNo)
        {

            WrapUpViewModel viewModel = new WrapUpViewModel()
            {
                AgentId = _agentId,
                CallSession = _callSession,
                CallStartTime = _callStartTime,
                ClientId = _clientId,
                CustomerPhone = _customerPhone,
                Extension = _extension,
                OriginalClientId = _originalClientId,
                OriginalAccountNo = _originalAccountNo,
                WrapupDatetime = DateTime.Today,

            };

            if (Request.IsAjaxRequest())
            {
                return Json(new { valid = true, message = this.RenderPartialViewToString("SearchClient.WrapUpParial", viewModel) });
            }
            return PartialView("SearchClient.WrapUpParial", viewModel);
        }

        #endregion

        #region Missed Call

        public ActionResult MissedCall(string agent_id)
        {
            var viewModel = new MissedCallViewModel()
            {
                AgentId = agent_id
            };

            return View(viewModel);
        }

        public ActionResult MissedCallListPartial()
        {
            var viewModel = _crmManager.GetCallCenterGetMissedCallListResults();
            return PartialView("MissedCall.ListPartial", viewModel);
        }

        #endregion

    }
}
