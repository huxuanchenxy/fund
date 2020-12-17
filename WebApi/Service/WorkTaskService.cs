﻿using Microsoft.Extensions.Caching.Distributed;
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
                //string fundcode = "001838,165513,008889,006031,008087,007574,519191,004854,519981,096001,206011,165520,040048,180003,161130,001092,118002,007824,000822,519171,164819,004317,008121,162411,161036,002938,002251,501012,003359,000995,005457,260109,160638,160630,502023,001230,006676,213008,165525,168204,168203,001628,161725,164908,161129,160141,160216,160222,050018,050024,002982,000179,003634,070001,160723,007216,160631,003194,163208,167301,539003,001552,001210,740001,005478,006105,200010,006128,006197,002345,519185,519196,161819,005033,005037,001261,006439,006438,160516,270042,450002,160218,160221,006817,001723,007164,519170,164824,164825,164402,161724,001404,161720,080002,350002,006282,006250,378546,005052,007280,160135,110025,161127,159941";
                string fundcode = string.Empty;
                //string fundcode2 = string.Empty;
                var dbdata = await _repo.GetPageList(new MyfundParm() { page = 1, rows = 1000, sort = "id", order = "asc" });
                int i = 0;
                //foreach (var d1 in dbdata.rows)
                //{
                //    if (d1.Balance != 0)
                //    {
                //        if (i % 2 == 0)
                //        {
                //            fundcode += d1.Code + ",";
                //        }
                //        else
                //        {
                //            fundcode2 += d1.Code + ",";
                //        }
                //        i++;

                //    }

                //}
                //string url = "https://api.doctorxiong.club/v1/fund?code=" + fundcode;
                //FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                //if (response.data != null)
                //{
                //    var data = response.data;
                //    foreach (var d in data)
                //    {
                //        var dd = await _repo.GetByCode(d.code);
                //        Myfund obj = new Myfund()
                //        {
                //            Id = dd.Id,
                //            Code = d.code,
                //            Name = d.name,
                //            Daygrowth = d.dayGrowth,
                //            Networth = d.netWorth,
                //            Totalworth = d.totalWorth,
                //            Updatetime = DateTime.Now,
                //            Worthdate = d.worthDate
                //        };
                //        await _repo.Update(obj);
                //    }
                //}

                //string url2 = "https://api.doctorxiong.club/v1/fund?code=" + fundcode2;
                //string url2 = $@"https://api.doctorxiong.club/v1/fund?code=008889,007574,519981,206011,118002,000822,164819,008121,161036,501012,000995,260109,160630,006676,165525,168203,161725,161129,160216,050018,002982,003634,160723,160631,163208,539003,001210,005478,200010,006197,519185,161819,005037,006439,160516,450002,160221,001723,519170,164825,161724,161720,350002,006250,005052,160135,161127,";
                //string[] url2arr = fundcode2.Split(",");




                var data = dbdata.rows;
                foreach (var d1 in data)
                {
                    string url2 = "https://api.doctorxiong.club/v1/fund?code=" + d1.Code;
                    FundRetComm ret2 = HttpClientHelper.GetResponse<FundRetComm>(url2);
                    if (ret2.data != null)
                    {
                        var d = ret2.data[0];
                        Myfund obj = new Myfund()
                        {
                            Id = d1.Id,
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



                ret.data = null;
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

        public async Task<ApiResult> InitData2V2()
        {
            ApiResult ret = new ApiResult();
            try
            {

                string fundcode = string.Empty;
                var dbdata = await _repo.GetPageList(new MyfundParm() { page = 1, rows = 1000, sort = "id", order = "asc" });

                var data = dbdata.rows;
                foreach (var d1 in data)
                {
                    try
                    {

                        string url = $@"https://fundmobapi.eastmoney.com/FundMApi/FundBaseTypeInformation.ashx?FCODE=" + d1.Code + "&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=9572315881384690&_=" + MathHelper.GetTimeStamp();
                        Root ret2 = HttpClientHelper.GetResponse<Root>(url);
                        if (ret2.ErrCode == 0)
                        {
                            var d = ret2.Datas;
                            Myfund obj = new Myfund()
                            {
                                Id = d1.Id,
                                Code = d1.Code,
                                Name = d1.Name,
                                Daygrowth = decimal.Parse(d.RZDF),
                                Networth = decimal.Parse(d.DWJZ),
                                Totalworth = decimal.Parse(d.LJJZ),
                                Updatetime = DateTime.Now,
                                Worthdate = Convert.ToDateTime(d.FSRQ)
                            };
                            await _repo.Update(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strex = ex.ToString();
                    }
                }



                ret.data = null;
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
                int num = 1;
                foreach (var d in data.rows)
                {
                    codes += d.Code + ",";
                    d.Num = num++;
                    d.Percent = Math.Round(d.Balance / d2.Balance * 100, 2);
                    d.PercentGrowth = d.Costavg > 0 ? Math.Round((d.Networth - d.Costavg) / d.Costavg * 100, 2) : 0;
                }
                //string url = "https://api.doctorxiong.club/v1/fund?code=" + codes;
                //FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                //foreach (var d in data.rows)
                //{
                //    if (response.data != null)
                //    {
                //        var cur = response.data.Where(c => c.code == d.Code).FirstOrDefault();
                //        if (cur != null)
                //        {
                //            d.ExpectGrowth = cur.expectGrowth;
                //        }

                //    }
                //}

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

        public async Task<ApiResult> UpdateNewBalanceV2()
        {
            ApiResult ret = new ApiResult();
            try
            {
                var data = await _repo.GetPageList(new MyfundParm() { page = 1, rows = 1000, order = "asc", sort = "id" });
                foreach (var d in data.rows)
                {
                    try
                    {
                        string url = $@"https://fundmobapi.eastmoney.com/FundMApi/FundBaseTypeInformation.ashx?FCODE=" + d.Code + "&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=9572315881384690&_=" + MathHelper.GetTimeStamp();
                        Root response = HttpClientHelper.GetResponse<Root>(url);
                        if (response.ErrCode == 0)
                        {
                            var cur = response.Datas;
                            if (d.Worthdate < Convert.ToDateTime(cur.FSRQ) && cur.RZDF != "0")//如果接口来的最新时间比db新，则更新最新balance
                            {
                                var newbalance = d.Balance * decimal.Parse(cur.RZDF) / 100 + d.Balance;
                                await _repo.Update2(new Myfund() { Id = d.Id, Balance = newbalance, Costavg = d.Costavg });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string exstr = ex.ToString();
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

        public async Task<ApiResult> Algorithm(string s, string s1)
        {
            ApiResult ret = new ApiResult();
            try
            {
                char[][] board = new char[9][];
                board[0] = new char[9] { '.', '.', '.', '.', '5', '.', '.', '1', '.' };
                board[1] = new char[9] { '.', '4', '.', '3', '.', '.', '.', '.', '.' };
                board[2] = new char[9] { '.', '.', '.', '.', '.', '3', '.', '.', '1' };
                board[3] = new char[9] { '8', '.', '.', '.', '.', '.', '.', '2', '.' };
                board[4] = new char[9] { '.', '.', '2', '.', '7', '.', '.', '.', '.' };
                board[5] = new char[9] { '.', '1', '5', '.', '.', '.', '.', '.', '.' };
                board[6] = new char[9] { '.', '.', '.', '.', '.', '2', '.', '.', '.' };
                board[7] = new char[9] { '.', '2', '.', '9', '.', '.', '.', '.', '.' };
                board[8] = new char[9] { '.', '.', '4', '.', '.', '.', '.', '.', '.' };
                ret.data = IsValidSudoku(board);
                ret.code = Code.Success;
                //ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }


        private bool IsValidSudoku(char[][] board)
        {
            bool ret = true;
            //横扫字典
            Dictionary<int, int> dic1 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            //竖扫字典
            Dictionary<int, int> dic2 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            //宫扫字典
            Dictionary<int, int> dic3 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            for (int i = 0; i < 9; i++)
            {
                //按宫扫先求下标，每个宫的第一个格的下标
                // 每个宫的第一个格子是 (036)(036)的9种组合，这个是按宫的外部循环
                //(0,0)(0,3)(0,6)
                //(3,0)(3,3)(3,6)
                //(6,0)(6,3)(6,6)
                int i1 = i - (i % 3);// 012 345 678 要转化成000 333 666，每个都是差0,1,2
                int j1 = (i % 3) * 3;// 012 345 678 要转化成036 036 036
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] != '.')//横扫描
                    {
                        int cur = int.Parse(board[i][j].ToString());
                        if (dic1[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic1[cur]++;
                        }
                    }
                    if (board[j][i] != '.')//竖扫描
                    {
                        int cur = int.Parse(board[j][i].ToString());
                        if (dic2[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic2[cur]++;
                        }
                    }

                    int gapi = j / 3;//012 345 678转化成加000 111 222 如(0,3)(0,4)(0,5)(1,3)(1,4)(1,5)(2,3)(2,4)(2,5)
                    int gapj = j % 3;//012 345 678转化成加012 012 012
                    int curi = i1 + gapi;
                    int curj = j1 + gapj;
                    if (board[curi][curj] != '.')//宫扫描
                    {
                        int cur = int.Parse(board[curi][curj].ToString());
                        if (dic3[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic3[cur]++;
                        }
                    }
                }
                //扫完一遍清空字典
                GetNewDic(dic1);
                GetNewDic(dic2);
                GetNewDic(dic3);
            }
            return ret;
        }

        private void GetNewDic(Dictionary<int, int> dic)
        {
            for (int i = 1; i <= 9; i++)
            {
                dic[i] = 0;
            }
        }

        public int StrStr(string haystack, string needle)
        {
            int ret = 0;
            if (string.IsNullOrEmpty(needle))
            {
                return ret;
            }
            int haylength = haystack.Length;
            int needlelength = needle.Length;
            for (int i = 0; i <= haylength - needlelength; i++)
            {
                string curchararr = haystack.Substring(i, needlelength);
                if (curchararr == needle)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 最长公共字符前缀
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            string ret = string.Empty;
            if (strs.Length == 0)
            {
                return ret;
            }

            int min = strs[0].Length;
            //先找出最短字符的长度
            for (int i = 1; i < strs.Length; i++)
            {
                string t = strs[i];
                if (t.Length < min)
                {
                    min = t.Length;
                }
            }

            for (int i = 0; i < min; i++)
            {
                char curchar = char.MinValue;
                bool flag = true;
                for (int j = 0; j < strs.Length; j++)
                {
                    string temp = strs[j];
                    if (j == 0)
                    {
                        curchar = temp[i];
                    }
                    else
                    {
                        if (temp[i] != curchar)
                        {
                            flag = false;
                            break;//该字符位置的字符有不一样的
                        }
                    }
                }
                if (!flag)
                {
                    break;//一旦有端的就全部退出
                }
                if (flag)
                {
                    ret += curchar;
                }
            }
            return ret;
        }
        /// <summary>
        /// 数组转链表
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private ListNode ConvertToNode(string[] arr)
        {
            ListNode nodes = new ListNode() { val = int.Parse(arr[0]) };
            ListNode p = nodes;
            for (int i = 1; i < arr.Length; i++)
            {
                p.next = new ListNode() { val = int.Parse(arr[i]) };
                p = p.next;
            }
            return nodes;
        }

        private string ConvertToString(ListNode nodes)
        {
            string ret = string.Empty;
            if (nodes == null)
            {
                return ret;
            }
            ListNode p = nodes;
            while (p != null)
            {
                int cur = p.val;
                ret = cur + ret;
                p = p.next;
            }
            return ret;
        }

        private string ReverseToString(string n)
        {
            string ret = string.Empty;
            for (int i = 0; i < n.Length; i++)
            {
                ret = n[i] + "," + ret;
            }
            ret = ret.TrimEnd(',');
            return ret;
        }

        private int RemoveElement(int[] nums, int val)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int length = nums.Length;
            //int i = 0;
            int j = 0;
            while (j < length)
            {
                //把后面不是val的提上来
                if (nums[j] == val)
                {
                    int havenotval = 0;//记录当前j后面的数字是否有和val不一样的，不一样说明有交换位置的操作
                    for (int k = j; k < length - 1; k++)
                    {
                        if (nums[k + 1] != val)
                        {
                            havenotval++;
                        }
                        int c = nums[k];
                        nums[k] = nums[k + 1];
                        nums[k + 1] = c;
                    }
                    j++;
                    if (nums[j - 1] == val && havenotval != 0)//如果当前交换后的前一个数字仍然是val并且确实交换过，则j回退，说明当前交换后第一个数字还是val，得再次逐个移动后面的数字向前。如果havenotval是0，说明后面的数字全是和val一样的，j不需要做回退操作，任然沿用上面的j++往后移。
                    {
                        j--;
                    }
                    //if (havenotval != 0)
                    //{
                    //    i++;
                    //}
                }
                else
                {
                    j++;//当前快指针如果不等于val则向后移
                }
            }
            int ret = length;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == val)
                {
                    ret--;
                }
            }
            return ret;
        }



        private ListNode AddTwoNumsV2(ListNode l1, ListNode l2)
        {
            ListNode ret = new ListNode();
            ListNode p = ret;
            int carry = 0;//进位
            while (l1 != null || l2 != null)
            {
                int cur1 = l1 != null ? l1.val : 0;
                int cur2 = l2 != null ? l2.val : 0;
                int curtmp = cur1 + cur2;//当前应该加起来等于几
                int cur = (curtmp + p.val) % 10;//当前个位数
                if ((curtmp + p.val) / 10 > 0)
                {
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
                p.val = cur;
                if ((l1 != null && l1.next != null) || (l2 != null && l2.next != null))
                {
                    p.next = new ListNode() { val = carry };
                    p = p.next;
                }

                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    l2 = l2.next;
                }
                if (l1 == null && l2 == null && carry == 1)
                {
                    p.next = new ListNode() { val = 1 };
                }

                //if (l1.next == null && l2.next == null)
                //{
                //    break;
                //}
            }
            return ret;
        }

        private bool IsValid(string s)
        {
            Stack<char> st = new Stack<char>();
            Dictionary<char, char> dict = new Dictionary<char, char>() { { ')', '(' }, { '}', '{' }, { ']', '[' } };
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsValue(s[i]))
                {
                    st.Push(s[i]);//凡是左括号的就直接入栈
                }
                else if (st.Count() == 0)
                {
                    return false;//如果第一个入的是右括号，则直接返回false,说明右括号不能在左括号之前出现
                }
                else if (dict[s[i]] != st.Pop())
                {
                    return false;//如果当前准备入栈的右括号先去通过dic匹配他的左括号和已经在栈里的第一个比较，如果不相等说明没有匹配成功
                }
            }
            return st.Count == 0;//如果栈为空，括号全部配对完，返回true
        }

        public void ReverseString(char[] s)
        {
            int n = s.Length;
            int middle = n / 2;
            for (int i = 0; i < middle;)
            {
                char m = s[i];
                s[i] = s[n - 1];
                s[n - 1] = m;
                i++;
                n--;
            }
        }

        public void reverseStringHelper(char[] s, int left, int right)
        {
            if (left >= right)
                return;
            swap(s, left, right);
            reverseStringHelper(s, ++left, --right);
        }

        private void swap(char[] array, int i, int j)
        {
            char temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public bool LemonadeChange(int[] bills)
        {
            int dic5 = 0;
            int dic10 = 0;
            for (int i = 0; i < bills.Length; i++)
            {
                if (bills[i] == 5)
                {
                    dic5 += 1;
                }
                else if (bills[i] == 10)
                {
                    if (dic5 > 0)
                    {
                        dic10 += 1;
                        dic5 -= 1;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (bills[i] == 20)
                {
                    if (dic10 > 0 && dic5 > 0)
                    {
                        dic5 -= 1;
                        dic10 -= 1;
                    }
                    else if (dic5 > 2)
                    {
                        dic5 = dic5 - 3;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        private int getNum(ListNode node)
        {
            int number = 0;
            int deep = 0;
            int cur = node.val;
            int curnum = deep * 10;
            if (curnum != 0)
            {
                number += curnum;
            }
            else
            {
                number = cur;
            }
            deep++;
            getNum(node.next);
            return number;
        }

    }


    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }


    public interface IWorkTaskService
    {
        Task<ApiResult> Algorithm(string s, string s1);
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
        Task<ApiResult> UpdateNewBalanceV2();
        Task<ApiResult> InitData2V2();
    }


}
