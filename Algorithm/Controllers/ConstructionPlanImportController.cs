using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSS.API.Common;
using MSS.Platform.Workflow.WebApi.Model;
using MSS.Platform.Workflow.WebApi.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

// Coded By admin 2019/9/27 9:54:54
namespace MSS.Platform.Workflow.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConstructionPlanImportController : ControllerBase
    {
        private readonly IConstructionPlanImportService _service;

        public ConstructionPlanImportController(IConstructionPlanImportService service)
        {
            _service = service;
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public ActionResult Save([FromForm]ConstructionPlanImportCommon importCommon, IFormFile file)
        {
            var ret = _service.Save(importCommon, file);
            return Ok(ret.Result);
            //return Ok("");
        }

        [HttpGet]
        public ActionResult ListPage([FromQuery] ConstructionPlanImportParm parm)
        {
            var resp = _service.ListPage(parm);
            return Ok(resp.Result);
        }
        [HttpGet("ListPageCommon")]
        public ActionResult ListPageCommon([FromQuery] ConstructionPlanCommonParm parm)
        {
            var resp = _service.ListPageCommon(parm);
            return Ok(resp.Result);
        }
    }
}



