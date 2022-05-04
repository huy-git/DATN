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
    public class DanhMucTinController : ControllerBase
    {
        private IDanhMucTinBusiness _danhmuctinBusiness;
        public DanhMucTinController(IDanhMucTinBusiness danhmuctinBusiness)
        {
            _danhmuctinBusiness = danhmuctinBusiness;
        }


        [Route("create-danhmuc")]
        [HttpPost]
        public DanhMucTin CreateTinTuc([FromBody] DanhMucTin model)
        {
            _danhmuctinBusiness.Create(model);
            return model;
        }
        [Route("delete-danhmuc")]
        [HttpPost]
        public IActionResult DeleteTinTuc([FromBody] Dictionary<string, object> formData)
        {
            int MaLoai = 0;
            if (formData.Keys.Contains("MaLoai") && !string.IsNullOrEmpty(Convert.ToString(formData["MaLoai"]))) { MaLoai = int.Parse(Convert.ToString(formData["MaLoai"])); }
            _danhmuctinBusiness.Delete(MaLoai);
            return Ok();
        }

        [Route("update-danhmuc")]
        [HttpPost]
        public DanhMucTin UpdateTinTuc([FromBody] DanhMucTin model)
        {
            _danhmuctinBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public DanhMucTin GetDatabyID(string id)
        {
            return _danhmuctinBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<DanhMucTin> GetDatabAll()
        {
            return _danhmuctinBusiness.GetDataAll();
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
                string loaitin = "";
                if (formData.Keys.Contains("loaitin") && !string.IsNullOrEmpty(Convert.ToString(formData["loaitin"]))) { loaitin = Convert.ToString(formData["loaitin"]); }
                long total = 0;
                var data = _danhmuctinBusiness.Search(page, pageSize, out total, loaitin);
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
