using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IDanhMucTinBusiness
    {
        bool Create(DanhMucTin model);
        bool Update(DanhMucTin model);
        bool Delete(int id);
        DanhMucTin GetDatabyID(string id);
        List<DanhMucTin> GetDataAll();
        List<DanhMucTin> Search(int pageIndex, int pageSize, out long total, string loaitin);
    }
}
