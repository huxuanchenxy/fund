using Microsoft.AspNetCore.Mvc;
using MSS.API.Common;
using MSS.Platform.Workflow.WebApi.Model;
using MSS.Platform.Workflow.WebApi.Service;
using System.Threading.Tasks;

namespace MSS.Platform.Workflow.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WfController : ControllerBase
    {
        private readonly IWorkTaskService _service;
        public WfController(IWorkTaskService service)
        {
            _service = service;
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

    }
}
