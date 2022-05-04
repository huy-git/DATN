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
    public class TinTucController : ControllerBase
    {
        private ITinTucBusiness _tintucBusiness;
        public TinTucController (ITinTucBusiness tintucBusiness )
        {
            _tintucBusiness = tintucBusiness ;
        }

        [Route("create-tintuc")]
        [HttpPost]
        public TinTuc CreateTinTuc([FromBody] TinTuc model)
        {
            _tintucBusiness.Create(model);
            return model;
        }
        [Route("delete-tintuc")]
        [HttpPost]
        public IActionResult DeleteTinTuc([FromBody] Dictionary<string, object> formData)
        {
            int MaTin=0;
            if (formData.Keys.Contains("MaTin") && !string.IsNullOrEmpty(Convert.ToString(formData["MaTin"]))) { MaTin = int.Parse(Convert.ToString(formData["MaTin"])); }
            _tintucBusiness.Delete(MaTin);
            return Ok();
        }

        [Route("update-tintuc")]
        [HttpPost]
        public TinTuc UpdateTinTuc([FromBody] TinTuc model)
        {
            _tintucBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public TinTuc GetDatabyID(int id)
        {
            return _tintucBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<TinTuc> GetDatabAll()
        {
            return _tintucBusiness.GetDataAll();
        }
        [Route("get-loai/{loai}")]
        [HttpGet]
        public IEnumerable<TinTuc> GetDatabLoai(int loai)
        {
            return _tintucBusiness.GetDataLoai(loai);
        }
        [Route("get-top3")]
        [HttpGet]
        public IEnumerable<TinTuc> GetDataTop3()
        {
            return _tintucBusiness.GetDataTop3();
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
                string maloai = null;
                string tieude = "";
                string trangthai = "";
                if (formData.Keys.Contains("tieude") && !string.IsNullOrEmpty(Convert.ToString(formData["tieude"]))) { tieude = Convert.ToString(formData["tieude"]); }
                if (formData.Keys.Contains("trangthai") && !string.IsNullOrEmpty(Convert.ToString(formData["trangthai"]))) { trangthai = Convert.ToString(formData["trangthai"]); }
                if (formData.Keys.Contains("maloai") && !string.IsNullOrEmpty(Convert.ToString(formData["maloai"]))) { maloai = Convert.ToString(formData["maloai"]); }
                long total = 0;
                var data = _tintucBusiness.Search(page, pageSize, out total, tieude, maloai, trangthai);
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
