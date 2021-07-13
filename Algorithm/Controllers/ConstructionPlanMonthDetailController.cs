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
    public class ConstructionPlanMonthDetailController : ControllerBase
    {
        private readonly IConstructionPlanMonthDetailService _service;

        public ConstructionPlanMonthDetailController(IConstructionPlanMonthDetailService service)
        {
            _service = service;
        }

        [HttpPost("{query}")]
        public ActionResult Create(int query)
        {
            var ret = _service.Create(query);
            return Ok(ret.Result);
        }

        [HttpGet]
        public ActionResult ListPage([FromQuery] ConstructionPlanMonthDetailParm parm)
        {
            var resp = _service.ListPage(parm);
            return Ok(resp.Result);
        }

        [HttpGet("{id}")]
        public ActionResult GetByID(int id)
        {
            var resp = _service.GetByID(id);
            return Ok(resp.Result);
        }

        [HttpPut]
        public ActionResult Update(ConstructionPlanMonthDetail obj)
        {
            var resp = _service.Update(obj);
            return Ok(resp.Result);
        }

    }
}



