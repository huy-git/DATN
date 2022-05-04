using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL
{
    public partial class TaiKhoanBusiness : ITaiKhoanBusiness
    {
        private ITaiKhoanRepository _res;
        private string Secret;
        public TaiKhoanBusiness(ITaiKhoanRepository TaiKhoangroup, IConfiguration configuration)
        {
            _res = TaiKhoangroup;
            Secret = configuration["AppSettings:Secret"];
        }
        public TaiKhoan Authenticate(string username, string password)
        {
            var user = _res.GetUser(username, password);
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.HoTen.ToString()),
                    new Claim(ClaimTypes.Role, user.PhanQuyen)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            return user;

        }
        public bool Create(TaiKhoan model)
        {
            return _res.Create(model);
        }
        public bool Update(TaiKhoan model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public TaiKhoan GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<TaiKhoan> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<TaiKhoan> Search(int pageIndex, int pageSize, out long total, string username, string hoten)
        {
            return _res.Search(pageIndex, pageSize, out total, username, hoten);
        }
    }
}
