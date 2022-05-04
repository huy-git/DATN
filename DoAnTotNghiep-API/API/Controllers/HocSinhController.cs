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
    public class HocSinhController : ControllerBase
    {
        private IHocSinhBusiness _hocsinhBusiness;
        public HocSinhController(IHocSinhBusiness hocsinhBusiness)
        {
            _hocsinhBusiness = hocsinhBusiness;
        }

        [Route("create-hocsinh")]
        [HttpPost]
        public HocSinh CreateHocSinh ([FromBody] HocSinh model)
        {
            _hocsinhBusiness.Create(model);
            return model;
        }
        [Route("delete-hocsinh")]
        [HttpPost]
        public IActionResult DeleteHocSinh([FromBody] Dictionary<string, object> formData)
        {
            int MaHS  = 0;
            if (formData.Keys.Contains("MaHS") && !string.IsNullOrEmpty(Convert.ToString(formData["MaHS"]))) { MaHS = int.Parse(Convert.ToString(formData["MaHS"])); }
            _hocsinhBusiness.Delete(MaHS);
            return Ok();
        }

        [Route("update-hocsinh")]
        [HttpPost]
        public HocSinh UpdateHocSinh([FromBody] HocSinh model)
        {
            _hocsinhBusiness.Update(model);
            return model;
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public HocSinh GetDatabyID(string id)
        {
            return _hocsinhBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<HocSinh> GetDatabAll()
        {
            return _hocsinhBusiness.GetDataAll();
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
                string hoten = "";
                if (formData.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formData["hoten"]))) { hoten = Convert.ToString(formData["hoten"]); }
                long total = 0;
                var data = _hocsinhBusiness.Search(page, pageSize, out total, hoten);
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
