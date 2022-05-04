using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface IHocSinhRepository
    {
        bool Create(HocSinh model);
        bool Update(HocSinh model);
        bool Delete(int id);
        HocSinh GetDatabyID(string id);
        List<HocSinh> GetDataAll();
        List<HocSinh> Search(int pageIndex, int pageSize, out long total, string hoten);
    }
}
