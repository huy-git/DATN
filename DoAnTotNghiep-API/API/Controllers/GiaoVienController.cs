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
    public class GiaoVienController : ControllerBase
    {
        private IGiaoVienBusiness _giaovienBusiness;
        public GiaoVienController(IGiaoVienBusiness giaovienBusiness)
        {
            _giaovienBusiness = giaovienBusiness;
        }

        [Route("create-giaovien")]
        [HttpPost]
        public GiaoVien CreateGiaoVien ([FromBody] GiaoVien model)
        {
            _giaovienBusiness.Create(model);
            return model;
        }
        [Route("delete-giaovien")]
        [HttpPost]
        public IActionResult DeleteGiaoVien([FromBody] Dictionary<string, object> formData)
        {
            int MaGV  = 0;
            if (formData.Keys.Contains("MaGV") && !string.IsNullOrEmpty(Convert.ToString(formData["MaGV"]))) { MaGV = int.Parse(Convert.ToString(formData["MaGV"])); }
            _giaovienBusiness.Delete(MaGV);
            return Ok();
        }

        [Route("update-giaovien")]
        [HttpPost]
        public GiaoVien UpdateGiaoVien ([FromBody] GiaoVien model)
        {
            _giaovienBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public GiaoVien GetDatabyID(string id)
        {
            return _giaovienBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<GiaoVien> GetDatabAll()
        {
            return _giaovienBusiness.GetDataAll();
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
                var data = _giaovienBusiness.Search(page, pageSize, out total, hoten);
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
