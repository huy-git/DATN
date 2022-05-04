using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class LopBusiness : ILopBusiness
    {
        private ILopRepository _res;
        public LopBusiness(ILopRepository Lopgroup)
        {
            _res = Lopgroup;
        }
        public bool Create(Lop model)
        {
            return _res.Create(model);
        }
        public bool Update(Lop model)
        {
            return _res.Update(model);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public Lop GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<Lop> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<Lop> Search(int pageIndex, int pageSize, out long total, string tenlop)
        {
            return _res.Search(pageIndex, pageSize, out total, tenlop);
        }
    }
}
