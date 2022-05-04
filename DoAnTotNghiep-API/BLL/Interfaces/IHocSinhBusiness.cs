using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IHocSinhBusiness
    {
        bool Create(HocSinh model);
        bool Update(HocSinh model);
        bool Delete(int id);
        HocSinh GetDatabyID(string id);
        List<HocSinh> GetDataAll();
        List<HocSinh> Search(int pageIndex, int pageSize, out long total, string hoten);
    }
}
