using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ITinTucBusiness
    {
        bool Create(TinTuc model);
        bool Update(TinTuc model);
        bool Delete(int id);
        TinTuc GetDatabyID(int id);
        List<TinTuc> GetDataAll();
        List<TinTuc> GetDataLoai(int loai);
        List<TinTuc> GetDataTop3();
        List<TinTuc> Search(int pageIndex, int pageSize, out long total, string tieude, string maloai, string trangthai);
    }
}
