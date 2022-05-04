using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class HocSinhBusiness : IHocSinhBusiness
    {
        private IHocSinhRepository _res;
        public HocSinhBusiness(IHocSinhRepository HocSinhgroup)
        {
            _res = HocSinhgroup;
        }
        public bool Create(HocSinh model)
        {
            return _res.Create(model);
        }
        public bool Update(HocSinh model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public HocSinh GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<HocSinh> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<HocSinh> Search(int pageIndex, int pageSize, out long total, string hoten)
        {
            return _res.Search(pageIndex, pageSize, out total, hoten);
        }
    }
}
