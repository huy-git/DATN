using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private ITaiKhoanBusiness _taikhoanBusiness;
        private string _path;
        public TaiKhoanController(ITaiKhoanBusiness taikhoanBusiness, IConfiguration configuration)
        {
            _taikhoanBusiness = taikhoanBusiness;
            _path = configuration["AppSettings:PATH"];
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _taikhoanBusiness.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("create-taikhoan")]
        [HttpPost]
        public TaiKhoan CreateTaiKhoan([FromBody] TaiKhoan model)
        {
            model.NgaySinh = model.NgaySinh.AddDays(1);
            _taikhoanBusiness.Create(model);
            return model;
        }
        [Route("delete-taikhoan")]
        [HttpPost]
        public IActionResult DeleteTaiKhoan([FromBody] Dictionary<string, object> formData)
        {
            int MaTK = 0;
            if (formData.Keys.Contains("MaTK") && !string.IsNullOrEmpty(Convert.ToString(formData["MaTK"]))) { MaTK = int.Parse(Convert.ToString(formData["MaTK"])); }
            _taikhoanBusiness.Delete(MaTK);
            return Ok();
        }

        [Route("update-taikhoan")]
        [HttpPost]
        public TaiKhoan UpdateTaiKhoan([FromBody] TaiKhoan model)
        {
            model.NgaySinh = model.NgaySinh.AddDays(1);
            _taikhoanBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public TaiKhoan GetDatabyID(string id)
        {
            return _taikhoanBusiness.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<TaiKhoan> GetDatabAll()
        {
            return _taikhoanBusiness.GetDataAll();
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
                string username = "";
                if (formData.Keys.Contains("username") && !string.IsNullOrEmpty(Convert.ToString(formData["username"]))) { username = Convert.ToString(formData["username"]); }
                long total = 0;
                if (formData.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formData["hoten"]))) { username = Convert.ToString(formData["hoten"]); }
                string hoten = "";
                var data = _taikhoanBusiness.Search(page, pageSize, out total, username, hoten);
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
