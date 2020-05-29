using MSS.API.Common;
using MSS.API.Common.Utility;
using MSS.Platform.Workflow.WebApi.Data;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using MSS.Common.Consul;
using System.Threading.Tasks;
using static MSS.API.Common.MyDictionary;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


// Coded By admin 2019/10/21 13:07:35
namespace MSS.Platform.Workflow.WebApi.Service
{
    public interface IConstructionPlanService
    {
        Task<ApiResult> GetPageList(ConstructionPlanParm parm);
        Task<ApiResult> Save(ConstructionPlan obj);
        Task<ApiResult> Update(ConstructionPlan obj);
        Task<ApiResult> Delete(string ids);
        Task<ApiResult> GetByID(int id);
    }

    public class ConstructionPlanService : IConstructionPlanService
    {
        private readonly IConstructionPlanRepo<ConstructionPlan> _repo;
        private readonly IAuthHelper _authhelper;
        private readonly int _userID;
        private readonly IServiceDiscoveryProvider _consulclient;

        public ConstructionPlanService(IConstructionPlanRepo<ConstructionPlan> repo, IAuthHelper authhelper, IServiceDiscoveryProvider consulclient)
        {
            _repo = repo;
            _authhelper = authhelper;
            _userID = _authhelper.GetUserId();
            _consulclient = consulclient;
        }

        public async Task<ApiResult> GetPageList(ConstructionPlanParm parm)
        {
            ApiResult ret = new ApiResult();
            try
            {
                parm.userId = _userID;
                //parm.UserID = 40;
                parm.processGUID = "c4c03c5e-63a3-47d6-913f-b8e08a51f5f8";
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

        public async Task<ApiResult> Save(ConstructionPlan obj)
        {
            ApiResult ret = new ApiResult();
            try
            {
                DateTime dt = DateTime.Now;
                obj.UpdatedTime = dt;
                obj.CreatedTime = dt;
                obj.UpdatedBy = _userID;
                obj.CreatedBy = _userID;
                ret.data = await _repo.Save(obj);
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

        public async Task<ApiResult> Update(ConstructionPlan obj)
        {
            ApiResult ret = new ApiResult();
            try
            {
                ConstructionPlan et = await _repo.GetByID(obj.Id);
                if (et != null)
                {
                    DateTime dt = DateTime.Now;
                    obj.UpdatedTime = dt;
                    obj.UpdatedBy = _userID;
                    ret.data = await _repo.Update(obj);
                    ret.code = Code.Success;
                }
                else
                {
                    ret.code = Code.DataIsnotExist;
                    ret.msg = "所要修改的数据不存在";
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
                return ret;
            }
        }

        public async Task<ApiResult> Delete(string ids)
        {
            ApiResult ret = new ApiResult();
            try
            {
                ret.data = await _repo.Delete(ids.Split(','), _userID);
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

        public async Task<ApiResult> GetByID(int id)
        {
            ApiResult ret = new ApiResult();
            try
            {
                ConstructionPlan obj = await _repo.GetByID(id);
                string orgurl = await _consulclient.GetServiceAsync("EqpService");
                orgurl = orgurl + "/api/v1/Upload/ListByEntity2/" + id + "/" + (int)SystemResource.ConstructionPlan;
                string orgret = HttpClientHelper.GetResponse(orgurl);
                ApiResult orgretobj = JsonConvert.DeserializeObject<ApiResult>(orgret);
                if (orgretobj.data != null)
                {
                    List<UploadFileModel> jdata = new List<UploadFileModel>();
                    JArray arr = (JArray)orgretobj.data;
                    foreach (var item in arr)
                    {
                        jdata.Add(new UploadFileModel()
                        {
                            ID = int.Parse(item["id"].ToString()),
                            FileName = item["fileName"].ToString(),
                            FilePath = item["filePath"].ToString(),
                            Type = int.Parse(item["type"].ToString()),
                            TypeName = item["typeName"].ToString(),
                            SystemResource = int.Parse(item["systemResource"].ToString()),
                            SystemResourceName = item["systemResourceName"].ToString(),
                            Entity = int.Parse(item["entity"].ToString())
                        });
                    }
                    if (jdata != null)
                    {
                        obj.FileIDs = JsonConvert.SerializeObject(UploadFileCommonHelper.ListShow(jdata));
                    }
                }



                ret.data = obj;
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


    }
}



