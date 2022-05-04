using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class GiaoVienBusiness : IGiaoVienBusiness
    {
        private IGiaoVienRepository _res;
        public GiaoVienBusiness(IGiaoVienRepository GiaoViengroup)
        {
            _res = GiaoViengroup;
        }
        public bool Create(GiaoVien model)
        {
            return _res.Create(model);
        }
        public bool Update(GiaoVien model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public GiaoVien GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<GiaoVien> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<GiaoVien> Search(int pageIndex, int pageSize, out long total, string hoten)
        {
            return _res.Search(pageIndex, pageSize, out total, hoten);
        }
    }
}
