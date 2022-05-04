using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class DanhMucTinBusiness: IDanhMucTinBusiness
    {
        private IDanhMucLoaiRepository _res;
        public DanhMucTinBusiness(IDanhMucLoaiRepository DanhMucTingroup)
        {
            _res = DanhMucTingroup;
        }
        public bool Create(DanhMucTin model)
        {
            return _res.Create(model);
        }
        public bool Update(DanhMucTin model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public DanhMucTin GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<DanhMucTin> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<DanhMucTin> Search(int pageIndex, int pageSize, out long total, string loaitin)
        {
            return _res.Search(pageIndex, pageSize, out total, loaitin);
        }
    }
}
