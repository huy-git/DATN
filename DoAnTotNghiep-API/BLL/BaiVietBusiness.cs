using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class BaiVietBusiness : IBaiVietBusiness
    {
        private IBaiVietRepository _res;
        public BaiVietBusiness(IBaiVietRepository Lopgroup)
        {
            _res = Lopgroup;
        }
        public bool Create(BaiViet model)
        {
            return _res.Create(model);
        }
        public bool Update(BaiViet model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public BaiViet GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public List<BaiViet> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<TopBaiViet> GetDataTopBaiViet()
        {
            return _res.GetDataTopBaiViet();
        }
        public List<BaiViet> Search(int pageIndex, int pageSize, out long total, string tieude, string taikhoan, string trangthai)
        {
            return _res.Search(pageIndex, pageSize, out total, tieude, taikhoan, trangthai);
        }
    }
}
