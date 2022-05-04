using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IBinhLuanBusiness
    {
        bool Create(BinhLuan model);
        bool Update(BinhLuan model);
        bool Delete(int id);
        BinhLuan GetDatabyID(int id);
        List<BinhLuan> GetDataAll();
        List<BinhLuan> GetDataBaiViet(int id);
        List<BinhLuan> Search(int pageIndex, int pageSize, out long total, int mabv);
    }
}
