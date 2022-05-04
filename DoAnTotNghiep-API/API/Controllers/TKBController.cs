using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TKBController : ControllerBase
    {
        private ITKBBusiness _tkbBusiness;
        public TKBController(ITKBBusiness tkbBusiness)
        {
            _tkbBusiness = tkbBusiness;
        }

        [Route("create-tkb")]
        [HttpPost]
        public TKB CreateTKB([FromBody] TKB model)
        {
            _tkbBusiness.Create(model);
            return model;
        }
        [Route("delete-tkb")]
        [HttpPost]
        public IActionResult DeleteTKB([FromBody] Dictionary<string, object> formData)
        {
            int MaTKB = 0;
            if (formData.Keys.Contains("MaTKB") && !string.IsNullOrEmpty(Convert.ToString(formData["MaTKB"]))) { MaTKB = int.Parse(Convert.ToString(formData["MaTKB"])); }
            _tkbBusiness.Delete(MaTKB);
            return Ok();
        }

        [Route("update-tkb")]
        [HttpPost]
        public TKB UpdateTKB([FromBody] TKB model)
        {
            _tkbBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public TKB GetDatabyID(string id)
        {
            return _tkbBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<TKB> GetDatabAll()
        {
            return _tkbBusiness.GetDataAll();
        }
        [Route("get-by-lop/{MaLop}")]
        [HttpGet]
        public IEnumerable<TKB> GetDataLop(string MaLop)
        {
            return _tkbBusiness.GetDataLop(MaLop);
        }
        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string malop = "";
                if (formData.Keys.Contains("malop") && !string.IsNullOrEmpty(Convert.ToString(formData["malop"]))) { malop = Convert.ToString(formData["malop"]); }
                long total = 0;
                var data = _tkbBusiness.Search(page, pageSize, out total, malop);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
