using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface IGiaoVienRepository
    {
        bool Create(GiaoVien model);
        bool Update(GiaoVien model);
        bool Delete(int id);
        GiaoVien GetDatabyID(string id);
        List<GiaoVien> GetDataAll();
        List<GiaoVien> Search(int pageIndex, int pageSize, out long total, string hoten  );
    }
}
