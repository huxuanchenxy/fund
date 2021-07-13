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
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _service;

        public MaintenanceController(IMaintenanceService service)
        {
            _service = service;
        }
        #region 已弃用
        #region MaintenanceItem
        [HttpPost("SaveMItem")]
        public ActionResult SaveMItem(MaintenanceItem maintenanceItem)
        {
            var ret = _service.SaveMItem(maintenanceItem);
            return Ok(ret.Result);
        }
        #endregion

        #region MaintenanceModuleItem
        [HttpPost("SaveMMoudleItem")]
        public ActionResult SaveMMoudleItem(List<MaintenanceModuleItem> maintenanceModuleItem)
        {
            var ret = _service.SaveMMoudleItem(maintenanceModuleItem);
            return Ok(ret.Result);
        }
        #endregion

        #region MaintenanceModuleItemValue
        [HttpPost("SaveMMoudleItemValue")]
        public ActionResult SaveMMoudleItemValue(MaintenanceModuleItemValueParm parm)
        {
            var ret = _service.SaveMMoudleItemValue(parm);
            return Ok(ret.Result);
        }
        #endregion

        #region MaintenanceModule
        [HttpPost("SaveMModule")]
        public ActionResult SaveMModule(MaintenanceModule maintenanceModule)
        {
            var ret = _service.SaveMModule(maintenanceModule);
            return Ok(ret.Result);
        }
        [HttpGet("ListMModule/{id}/{isInit}")]
        public ActionResult ListMModule(int id,bool isInit)
        {
            var ret = _service.ListMModule(id,isInit);
            return Ok(ret.Result);
        }
        #endregion

        #region MaintenanceList
        [HttpPost("SaveMList")]
        public ActionResult SaveMList(MaintenanceList maintenanceList)
        {
            var ret = _service.SaveMList(maintenanceList);
            return Ok(ret.Result);
        }
        [HttpGet]
        public ActionResult ListPage([FromQuery] MaintenanceListParm parm)
        {
            var ret = _service.ListPage(parm);
            return Ok(ret.Result);
        }
        #endregion
        #endregion

        #region PMModule
        [HttpPost("Import")]
        //[RequestSizeLimit(52428800)]
        public ActionResult ImportModule([FromForm]PMModule pMModule,IFormFile file)
        {
            var ret = _service.ImportModule(pMModule,file);
            return Ok(ret.Result);
            //return Ok("");
        }
        [HttpGet("ListModulePage")]
        public ActionResult ListModulePage([FromQuery] PMModuleParm parm)
        {
            var ret = _service.ListModulePage(parm);
            return Ok(ret.Result);
        }
        [HttpGet("GetModuleByID/{id}")]
        public ActionResult GetModuleByID(int id)
        {
            var ret = _service.GetModuleByID(id);
            return Ok(ret.Result);
        }
        #endregion

        #region PMEntity
        [HttpPost("SavePMEntity")]
        public ActionResult SavePMEntity(PMEntity pmEntity)
        {
            var ret = _service.SavePMEntity(pmEntity);
            return Ok(ret.Result);
            //return Ok("");
        }
        [HttpPut("UpdatePMEntity")]
        public ActionResult UpdatePMEntity(PMEntity pmEntity)
        {
            var ret = _service.UpdatePMEntity(pmEntity);
            return Ok(ret.Result);
            //return Ok("");
        }
        [HttpPut("UpdatePMEntityStatus/{id}/{status}")]
        public ActionResult UpdatePMEntityStatus(int id,int status)
        {
            var ret = _service.UpdatePMEntityStatus(id,status);
            return Ok(ret.Result);
            //return Ok("");
        }
        [HttpGet("ListEntityPage")]
        public ActionResult ListEntityPage([FromQuery] PMEntityParm parm)
        {
            var ret = _service.ListEntityPage(parm);
            return Ok(ret.Result);
        }
        [HttpDelete("{ids}")]
        public ActionResult DelPMEntity(string ids)
        {
            var ret = _service.DelPMEntity(ids.Split(','));
            return Ok(ret.Result);
        }
        [HttpGet("GetEntityByID/{id}/{isUpdate}")]
        public ActionResult GetEntityByID(int id,bool isUpdate)
        {
            var ret = _service.GetEntityByID(id,isUpdate);
            return Ok(ret.Result);
        }
        #endregion

    }
}



