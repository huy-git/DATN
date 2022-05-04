using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class BaiVietRepository : IBaiVietRepository
    {
        private IDatabaseHelper _dbHelper;
        public BaiVietRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(BaiViet model)
        {
            string msgError = "";
            try
            {
                string thoigian = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "bai_viet_create",
                "@TieuDe", model.TieuDe,
                "@NoiDung", model.NoiDung,
                "@HinhAnh", model.HinhAnh,
                "@ThoiGian", thoigian,
                "@TrangThai", model.TrangThai,
                "@MaTK", model.MaTK);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "bai_viet_delete",
                "@MaBaiViet", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(BaiViet model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "bai_viet_update",
                "@MaBaiViet", model.MaBaiViet,
                "@TieuDe", model.TieuDe,
                "@NoiDung", model.NoiDung,
                "@HinhAnh", model.HinhAnh,
                "@ThoiGian", model.ThoiGian,
                "@TrangThai", model.TrangThai,
                "@MaTK", model.MaTK);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BaiViet GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "baiviet_by_id",
                     "@MaBaiViet", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<BaiViet>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BaiViet> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "baiviet_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<BaiViet>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TopBaiViet> GetDataTopBaiViet()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "top5_bai_viet_cmt");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TopBaiViet>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BaiViet> Search(int pageIndex, int pageSize, out long total, string tieude, string taikhoan, string trangthai)
        {
            string msgError = "";
            total = 0;
            try
            {

                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "bai_viet_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tieude", tieude,
                    "@taikhoan", null,
                    "@trangthai", trangthai);
                if (!string.IsNullOrEmpty(taikhoan))
                {
                    int tk = int.Parse(taikhoan.ToString());
                    dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "bai_viet_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tieude", tieude,
                    "@taikhoan", tk,
                    "@trangthai", trangthai);
                }
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<BaiViet>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
