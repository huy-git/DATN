using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface ITaiKhoanRepository
    {
        TaiKhoan GetUser(string username, string password);
        bool Create(TaiKhoan model);
        bool Update(TaiKhoan model);
        bool Delete(int id);
        TaiKhoan GetDatabyID(string id);
        List<TaiKhoan> GetDataAll();
        List<TaiKhoan> Search(int pageIndex, int pageSize, out long total, string username, string hoten);
    }
}
