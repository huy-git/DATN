using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ITKBBusiness
    {
        bool Create(TKB model);
        bool Update(TKB model);
        bool Delete(int id);
        TKB GetDatabyID(string id);
        List<TKB> GetDataAll();
        List<TKB> GetDataLop(string MaLop);
        List<TKB> Search(int pageIndex, int pageSize, out long total, string malop);
    }
}
