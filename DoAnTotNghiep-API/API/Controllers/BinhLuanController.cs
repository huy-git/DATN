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
    public class BinhLuanController : ControllerBase
    {
        private IBinhLuanBusiness _binhluanBusiness;
        public BinhLuanController(IBinhLuanBusiness binhluanBusiness)
        {
            _binhluanBusiness = binhluanBusiness;
        }

        [Route("create-binhluan")]
        [HttpPost]
        public BinhLuan CreateBinhLuan([FromBody] BinhLuan model)
        {
            _binhluanBusiness.Create(model);
            return model;
        }
        [Route("delete-binhluan")]
        [HttpPost]
        public IActionResult DeleteBinhLuan([FromBody] Dictionary<string, object> formData)
        {
            int MaBL = 0;
            if (formData.Keys.Contains("MaBL") && !string.IsNullOrEmpty(Convert.ToString(formData["MaBL"]))) { MaBL = int.Parse(Convert.ToString(formData["MaBL"])); }
            _binhluanBusiness.Delete(MaBL);
            return Ok();
        }

        [Route("update-binhluan")]
        [HttpPost]
        public BinhLuan UpdateBinhLuan ([FromBody] BinhLuan model)
        {
            _binhluanBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public BinhLuan GetDatabyID(int id)
        {
            return _binhluanBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<BinhLuan> GetDatabAll()
        {
            return _binhluanBusiness.GetDataAll();
        }
        [Route("get-bai-viet/{id}")]
        [HttpGet]
        public IEnumerable<BinhLuan> GetDataBaiViet(int id)
        {
            return _binhluanBusiness.GetDataBaiViet(id);
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
                int mabv = 0;
                if (formData.Keys.Contains("mabv") && !string.IsNullOrEmpty(Convert.ToString(formData["mabv"]))) { mabv = int.Parse(Convert.ToString(formData["mabv"])); }
                long total = 0;
                var data = _binhluanBusiness.Search(page, pageSize, out total, mabv);
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
