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
using Quartz;

namespace MSS.Platform.Workflow.WebApi.Service
{
    public class FundJob : IJob//创建IJob的实现类，并实现Excute方法。
    {
        private readonly IWorkTaskRepo<TaskViewModel> _repo;
        public FundJob(IWorkTaskRepo<TaskViewModel> repo)
        {
            _repo = repo;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine(DateTime.Now);
                string codes = string.Empty;
                MyfundParm parm = new MyfundParm() { page = 1, rows = 1000, sort = "id", order = "asc" };
                var data = _repo.GetPageList(parm).Result;
                //foreach (var d in data.rows)
                //{
                //    codes += d.Code + ",";
                //}
                //string url = "https://api.doctorxiong.club/v1/fund?code=" + codes;
                //FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                foreach (var d in data.rows)
                {
                    try
                    {
                        string url = "https://api.doctorxiong.club/v1/fund?code=" + d.Code;
                        FundRetComm response = HttpClientHelper.GetResponse<FundRetComm>(url);
                        if (response.data != null)
                        {
                            var cur = response.data.Where(c => c.code == d.Code).FirstOrDefault();
                            if (cur != null)
                            {
                                _repo.UpdateExpectGrowth(new Myfund() { Id = d.Id, ExpectGrowth = cur.expectGrowth });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    
                }
            });
        }
    }


}
