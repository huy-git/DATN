using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class TinTucBusiness: ITinTucBusiness
    {
        private ITinTucRepository _res;
        public TinTucBusiness (ITinTucRepository Lopgroup)
        {
            _res = Lopgroup;
        }
        public bool Create(TinTuc model)
        {
            return _res.Create(model);
        }
        public bool Update(TinTuc model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public TinTuc GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public List<TinTuc> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<TinTuc> GetDataLoai(int loai)
        {
            return _res.GetDataLoai(loai);
        }
        public List<TinTuc> GetDataTop3()
        {
            return _res.GetDataTop3();
        }
        public List<TinTuc> Search(int pageIndex, int pageSize, out long total, string tieude, string maloai, string trangthai)
        {
            return _res.Search(pageIndex, pageSize, out total, tieude, maloai, trangthai);
        }
    }
}
