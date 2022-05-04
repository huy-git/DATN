using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class BinhLuanBusiness : IBinhLuanBusiness
    {
        private IBinhLuanRepository _res;
        public BinhLuanBusiness(IBinhLuanRepository Lopgroup)
        {
            _res = Lopgroup;
        }
        public bool Create(BinhLuan model)
        {
            return _res.Create(model);
        }
        public bool Update(BinhLuan model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public BinhLuan GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public List<BinhLuan> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<BinhLuan> GetDataBaiViet(int id)
        {
            return _res.GetDataBaiViet(id);
        }
        public List<BinhLuan> Search(int pageIndex, int pageSize, out long total, int mabv)
        {
            return _res.Search(pageIndex, pageSize, out total, mabv);
        }
    }
}
