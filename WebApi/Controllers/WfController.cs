﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MSS.API.Common;
using MSS.Platform.Workflow.WebApi.Model;
using MSS.Platform.Workflow.WebApi.Service;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSS.Platform.Workflow.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WfController : ControllerBase
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        private QuartzStart _quart;

        private readonly IWorkTaskService _service;
        public WfController(IWorkTaskService service, ISchedulerFactory schedulerFactory, QuartzStart quart)
        {
            _service = service;
            this._schedulerFactory = schedulerFactory;
            _quart = quart;
        }

        [HttpGet("QueryReadyTasks")]
        public async Task<ActionResult<ApiResult>> QueryReadyTasks([FromQuery] WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetReadyTasks(parm);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取当前用户待办任务数据失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("QueryFinishTasks")]
        public async Task<ActionResult<ApiResult>> QueryFinishTasks([FromQuery] WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetFinishTasks(parm);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取当前用户已办任务数据失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }


        [HttpGet("GetPageMyApply")]
        public async Task<ActionResult<ApiResult>> GetPageMyApply([FromQuery] WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetPageMyApply(parm);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取当前用户申请数据失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("GetPageActivityInstance")]
        public async Task<ActionResult<ApiResult>> GetPageActivityInstance([FromQuery] WorkQueryParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetPageActivityInstance(parm);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取当前用户参与的流转数据失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("StartProcess")]
        public async Task<ActionResult<WfRet>> StartProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.StartProcess(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "启动工作流失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("GetNextStepRoleUserTree")]
        public async Task<ActionResult<WfRet>> GetNextStepRoleUserTree(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.GetNextStepRoleUserTree(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "获取下一步信息失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("NextProcess")]
        public async Task<ActionResult<WfRet>> NextProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.NextProcess(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "转到下一步流程失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("WithdrawProcess")]
        public async Task<ActionResult<WfRet>> WithdrawProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.WithdrawProcess(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "撤销流程失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("SendBackProcess")]
        public async Task<ActionResult<WfRet>> SendBackProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.SendBackProcess(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "退回流程失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("GetProcessListSimple")]
        public async Task<ActionResult<WfRet>> GetProcessListSimple()
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.GetProcessListSimple();
            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "获取流程基本信息失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("ReverseProcess")]
        public async Task<ActionResult<WfRet>> ReverseProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.ReverseProcess(parm);

            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "返签流程失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("CancelProcess")]
        public async Task<ActionResult<WfRet>> CancelProcess(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.CancelProcess(parm);
            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "取消流程失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpPost("QueryReadyActivityInstance")]
        public async Task<ActionResult<WfRet>> QueryReadyActivityInstance(WfReq parm)
        {
            WfRet ret = new WfRet { Status = 1 };
            try
            {
                ret = await _service.QueryReadyActivityInstance(parm);
            }
            catch (System.Exception ex)
            {
                ret.Message = string.Format(
                    "获取当前流转到哪个节点失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 初始化，仅用一次
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMyfund")]
        public async Task<ActionResult<ApiResult>> GetMyfund()
        {
            ApiResult ret = new ApiResult();
            try
            {
                ret = await _service.InitData();
            }
            catch (System.Exception ex)
            {

            }
            return ret;
        }

        /// <summary>
        /// 更新最新净值和日增长，从接口到db
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMyfund2")]
        public async Task<ActionResult<ApiResult>> GetMyfund2()
        {
            ApiResult ret = new ApiResult();
            try
            {
                ret = await _service.InitData2V2();
            }
            catch (System.Exception ex)
            {

            }
            return ret;
        }

        [HttpGet("GetPageList")]
        public async Task<ActionResult<ApiResult>> GetPageList([FromQuery] MyfundParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetPageList(parm);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取分页数据Myfund失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("Update2")]
        public async Task<ActionResult<ApiResult>> Update2([FromQuery] Myfund obj)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.Update2(obj);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取分页数据Myfund失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("UpdateNewBalance")]
        public async Task<ActionResult<ApiResult>> UpdateNewBalance()
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.UpdateNewBalanceV2();

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取分页数据Myfund失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromQuery] Myfund obj)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetById(obj.Id);

            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取分页数据Myfund失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }

        [HttpGet("FundJob")]
        public async Task<string[]> FundJob()
        {
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器
            var trigger = TriggerBuilder.Create()
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(3).RepeatForever())//每两秒执行一次
                            .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create<FundJob>()
                            .WithIdentity("job", "group")
                            .Build();
            //5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return await Task.FromResult(new string[] { "value1", "value2" });
        }

        [HttpGet("StartJob")]
        public async Task<ActionResult<ApiResult>> StartJob()
        {
            ApiResult ret = new ApiResult { code = Code.Success };
            await _quart.Start();
            return ret;
        }

        [HttpGet("StopJob")]
        public async Task<ActionResult<ApiResult>> StopJob()
        {
            ApiResult ret = new ApiResult { code = Code.Success };
            await _quart.Stop();
            return ret;
        }

        [HttpGet("Algorithm")]
        public async Task<ActionResult<ApiResult>> Algorithm2(string s,string s1)
        {
            ApiResult reponse = await _service.Algorithm(s,s1);
            return reponse;
        }

    }
}
