using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class TKBBusiness : ITKBBusiness
    {
        private ITKBRepository _res;
        public TKBBusiness(ITKBRepository TKBgroup)
        {
            _res = TKBgroup;
        }
        public bool Create(TKB model)
        {
            return _res.Create(model);
        }
        public bool Update(TKB model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public TKB GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<TKB> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<TKB> GetDataLop(string MaLop)
        {
            return _res.GetDataLop(MaLop);
        }
        public List<TKB> Search(int pageIndex, int pageSize, out long total, string malop)
        {
            return _res.Search(pageIndex, pageSize, out total, malop);
        }
    }
}
