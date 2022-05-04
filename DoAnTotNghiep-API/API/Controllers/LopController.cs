using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopController : ControllerBase
    {
        private ILopBusiness _lopBusiness;
        public LopController(ILopBusiness lopBusiness)
        {
            _lopBusiness = lopBusiness;
        }

        [Route("create-lop")]
        [HttpPost]
        public Lop CreateLop([FromBody] Lop model)
        {
            _lopBusiness.Create(model);
            return model;
        }
        [Route("delete-lop")]
        [HttpPost]
        public IActionResult DeleteLop([FromBody] Dictionary<string, object> formData)
        {
            string MaLop = "";
            if (formData.Keys.Contains("MaLop") && !string.IsNullOrEmpty(Convert.ToString(formData["MaLop"])))
            {
                MaLop = Convert.ToString(formData["MaLop"]);
            }
            _lopBusiness.Delete(MaLop);
            return Ok();
        }

        [Route("update-lop")]
        [HttpPost]
        public Lop UpdateLop([FromBody] Lop model)
        {
            _lopBusiness.Update(model);
            return model;
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public Lop GetDatabyID(string id)
        {
            return _lopBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<Lop> GetDatabAll()
        {
            return _lopBusiness.GetDataAll();
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
                string tenlop = "";
                if (formData.Keys.Contains("tenlop") && !string.IsNullOrEmpty(Convert.ToString(formData["tenlop"]))) { tenlop = Convert.ToString(formData["tenlop"]); }
                long total = 0;
                var data = _lopBusiness.Search(page, pageSize, out total, tenlop);
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
