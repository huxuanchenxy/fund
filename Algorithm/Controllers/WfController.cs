using Microsoft.AspNetCore.Builder;
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

        private readonly IWorkTaskService _service;
        public WfController(IWorkTaskService service, ISchedulerFactory schedulerFactory)
        {
            _service = service;
            this._schedulerFactory = schedulerFactory;
        }

        [HttpGet("Algorithm")]
        public async Task<ActionResult<ApiResult>> Algorithm2(string s,string s1)
        {
            ApiResult reponse = await _service.Algorithm(s,s1);
            return reponse;
        }

    }
}
