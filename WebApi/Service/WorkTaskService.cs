using Microsoft.Extensions.Caching.Distributed;
using MSS.API.Common;
using MSS.API.Common.Utility;
using MSS.Common.Consul;
using MSS.Platform.Workflow.WebApi.Data;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using System.Net;
using System.Threading.Tasks;
using MSS.API.Common.DistributedEx;
using System.Collections.Generic;
using System.Linq;

namespace MSS.Platform.Workflow.WebApi.Service
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepo<TaskViewModel> _repo;
        private readonly IAuthHelper _authhelper;
        private readonly IWfprocessRepo<Wfprocess> _wfprocessRepo;
        private readonly IDistributedCache _cache;
        public WorkTaskService(IWorkTaskRepo<TaskViewModel> repo, IAuthHelper authhelper, IWfprocessRepo<Wfprocess> wfprocessRepo, IDistributedCache cache)
        {
            _repo = repo;
            _authhelper = authhelper;
            _wfprocessRepo = wfprocessRepo;
            _cache = cache;
        }

        /// <summary>
        /// 我的待办
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetReadyTasks(WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                //List<dynamic> pa = new List<dynamic>();
                //await _cache.HSetAsync("testhset", "testfield", "testvalue");
                //await _cache.SetAsync("testset1", new ApiResult() { code = Code.Success, data = 1, msg = "1" },null);
                //ApiResult pp = new ApiResult() { code = Code.Success, data = 2, msg = "2" };
                //ApiResult pp1 = new ApiResult() { code = Code.Success, data = 3, msg = "3" };
                //await _cache.HMSetAsync("testhmset",pp,pp1);

                //await _cache.HSetAsync("testobjset4", new ApiResult() { code = Code.Success, data = 4, msg = "4" });

                //var tmp = await _cache.HGetAsync<dynamic>("testobjset4","data");
                parm.ActivityState = 1;
                parm.AssignedToUserID = _authhelper.GetUserId();
                //parm.AssignedToUserID = 40;
                var data = await _repo.GetPageList(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        public async Task<ApiResult> GetFinishTasks(WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.ActivityState = 4;
                parm.AssignedToUserID = _authhelper.GetUserId();
                //parm.AssignedToUserID = 40;
                var data = await _repo.GetPageList(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 我的申请
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetPageMyApply(WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.AssignedToUserID = _authhelper.GetUserId();
                //parm.AssignedToUserID = 40;
                var data = await _repo.GetPageMyApply(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 流转日志
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetPageActivityInstance(WorkQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.UserID = _authhelper.GetUserId();
                //parm.UserID = 40;
                var data = await _repo.GetPageActivityInstance(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }



        public async Task<WfRet> StartProcess(WfReq parm)
        {
            WfRet ret = new WfRet();

            return ret;
        }


        public async Task<WfRet> GetNextStepRoleUserTree(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> NextProcess(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> WithdrawProcess(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> SendBackProcess(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> GetProcessListSimple()
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> ReverseProcess(WfReq parm)
        {
            WfRet ret = new WfRet();

            return ret;
        }
        public async Task<WfRet> CancelProcess(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> QueryReadyActivityInstance(WfReq parm)
        {
            WfRet ret = new WfRet();


            return ret;
        }

        public async Task<WfRet> QueryCompletedTasks(WfReq parm)
        {
            WfRet ret = new WfRet();

            return ret;
        }

        private async Task<WfReq> getWfCommonReq(WfReq parm)
        {
            parm.UserID = _authhelper.GetUserId();
            return parm;
        }

        public async Task<ApiResult> InitData()
        {
            ApiResult ret = new ApiResult();
            try
            {
                string fundcode = "001838,165513,008889,006031,008087,007574,519191,004854,519981,096001,206011,165520,040048,180003,161130,001092,118002,007824,000822,519171,164819,004317,008121,162411,161036,002938,002251,501012,003359,000995,005457,260109,160638,160630,502023,001230,006676,213008,165525,168204,168203,001628,161725,164908,161129,160141,160216,160222,050018,050024,002982,000179,003634,070001,160723,007216,160631,003194,163208,167301,539003,001552,001210,740001,005478,006105,200010,006128,006197,002345,519185,519196,161819,005033,005037,001261,006439,006438,160516,270042,450002,160218,160221,006817,001723,007164,519170,164824,164825,164402,161724,001404,161720,080002,350002,006282,006250,378546,005052,007280,160135,110025,161127,159941";

                string url = "https://api.doctorxiong.club/v1/fund?code=" + fundcode;
                FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                if (response.data != null)
                {
                    var data = response.data;
                    foreach (var d in data)
                    {
                        Myfund obj = new Myfund()
                        {
                            Code = d.code,
                            Name = d.name,
                            Daygrowth = d.dayGrowth,
                            Networth = d.netWorth,
                            Totalworth = d.totalWorth,
                            Updatetime = DateTime.Now,
                            Worthdate = d.worthDate
                        };
                        await _repo.Save2(obj);
                    }
                }
                ret.data = response;
                ret.code = Code.Success;
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
                return ret;
            }
        }

        public async Task<ApiResult> InitData2()
        {
            ApiResult ret = new ApiResult();
            try
            {
                //从天天基金网站上拔取
                string fundcode = "001838,165513,008889,006031,008087,007574,519191,004854,519981,096001,206011,165520,040048,180003,161130,001092,118002,007824,000822,519171,164819,004317,008121,162411,161036,002938,002251,501012,003359,000995,005457,260109,160638,160630,502023,001230,006676,213008,165525,168204,168203,001628,161725,164908,161129,160141,160216,160222,050018,050024,002982,000179,003634,070001,160723,007216,160631,003194,163208,167301,539003,001552,001210,740001,005478,006105,200010,006128,006197,002345,519185,519196,161819,005033,005037,001261,006439,006438,160516,270042,450002,160218,160221,006817,001723,007164,519170,164824,164825,164402,161724,001404,161720,080002,350002,006282,006250,378546,005052,007280,160135,110025,161127,159941";

                string url = "https://api.doctorxiong.club/v1/fund?code=" + fundcode;
                FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                if (response.data != null)
                {
                    var data = response.data;
                    foreach (var d in data)
                    {
                        var dd = _repo.GetByCode(d.code);
                        Myfund obj = new Myfund()
                        {
                            Id = dd.Id,
                            Code = d.code,
                            Name = d.name,
                            Daygrowth = d.dayGrowth,
                            Networth = d.netWorth,
                            Totalworth = d.totalWorth,
                            Updatetime = DateTime.Now,
                            Worthdate = d.worthDate
                        };
                        await _repo.Update(obj);
                    }
                }
                ret.data = response;
                ret.code = Code.Success;
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
                return ret;
            }
        }

        public async Task<ApiResult> GetPageList(MyfundParm parm)
        {
            ApiResult ret = new ApiResult();
            try
            {
                parm.page = 1;
                parm.rows = 1000;
                var data = await _repo.GetPageList(parm);
                var d2 = await _repo.GetAllBalance();
                string codes = string.Empty;
                foreach (var d in data.rows)
                {
                    codes += d.Code + ",";
                    d.Percent = Math.Round(d.Balance / d2.Balance * 100, 2);
                    d.PercentGrowth = d.Costavg > 0 ? Math.Round((d.Networth - d.Costavg) / d.Costavg * 100, 2) : 0;
                }
                string url = "https://api.doctorxiong.club/v1/fund?code=" + codes;
                FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                foreach (var d in data.rows)
                {
                    if (response.data != null)
                    {
                        var cur = response.data.Where(c => c.code == d.Code).FirstOrDefault();
                        if (cur != null)
                        {
                            d.ExpectGrowth = cur.expectGrowth;
                        }
                        
                    }
                }
                    
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        public async Task<ApiResult> Update2(Myfund obj)
        {
            ApiResult ret = new ApiResult();
            try
            {
                var data = await _repo.Update2(obj);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        public async Task<ApiResult> GetById(int id)
        {
            ApiResult ret = new ApiResult();
            try
            {
                var data = await _repo.GetById(id);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        public async Task<ApiResult> UpdateNewBalance()
        {
            ApiResult ret = new ApiResult();
            try
            {
                var data = await _repo.GetPageList(new MyfundParm() { page = 1, rows = 1000, order = "asc", sort = "id" });
                foreach (var d in data.rows)
                {
                    string url = "https://api.doctorxiong.club/v1/fund?code=" + d.Code;
                    FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                    if (response.data != null)
                    {
                        var cur = response.data[0];
                        if (d.Worthdate < cur.worthDate && cur.dayGrowth != 0)//如果接口来的最新时间比db新，则更新最新balance
                        {
                            var newbalance = d.Balance * cur.dayGrowth / 100 + d.Balance;
                            await _repo.Update2(new Myfund() { Id = d.Id, Balance = newbalance, Costavg = d.Costavg });
                        }
                    }
                }
                ret.code = Code.Success;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

    }

    public interface IWorkTaskService
    {
        Task<ApiResult> GetReadyTasks(WorkTaskQueryParm parm);
        Task<ApiResult> GetFinishTasks(WorkTaskQueryParm parm);
        Task<ApiResult> GetPageMyApply(WorkTaskQueryParm parm);
        Task<ApiResult> GetPageActivityInstance(WorkQueryParm parm);
        Task<WfRet> StartProcess(WfReq parm);
        Task<WfRet> GetNextStepRoleUserTree(WfReq parm);
        Task<WfRet> NextProcess(WfReq parm);
        Task<WfRet> WithdrawProcess(WfReq parm);
        Task<WfRet> SendBackProcess(WfReq parm);
        Task<WfRet> GetProcessListSimple();
        Task<WfRet> ReverseProcess(WfReq parm);
        Task<WfRet> CancelProcess(WfReq parm);
        Task<WfRet> QueryReadyActivityInstance(WfReq parm);
        Task<WfRet> QueryCompletedTasks(WfReq parm);
        Task<ApiResult> InitData();
        Task<ApiResult> InitData2();
        Task<ApiResult> GetPageList(MyfundParm parm);
        Task<ApiResult> Update2(Myfund obj);
        Task<ApiResult> GetById(int id);
        Task<ApiResult> UpdateNewBalance();
    }


}
