using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IBaiVietBusiness
    {
        bool Create(BaiViet model);
        bool Update(BaiViet model);
        bool Delete(int id);
        BaiViet GetDatabyID(int id);
        List<BaiViet> GetDataAll();
        List<TopBaiViet> GetDataTopBaiViet();
        List<BaiViet> Search(int pageIndex, int pageSize, out long total, string tieude, string taikhoan, string trangthai);
    }
}
