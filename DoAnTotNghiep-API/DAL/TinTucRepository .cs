using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class TinTucRepository: ITinTucRepository
    {
         private IDatabaseHelper _dbHelper;
        public TinTucRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(TinTuc model)
        {
            string msgError = "";
            try
            {
                string thoigian = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tin_tuc_create",
                "@MaLoai", model.MaLoai,
                "@TieuDe", model.TieuDe,
                "@HinhAnh", model.HinhAnh,
                "@NoiDung", model.NoiDung,
                "@ThoiGian", thoigian,
                "@TrangThai", model.TrangThai);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tin_tuc_delete",
                "@MaTin", id);
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
        public bool Update(TinTuc model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tin_tuc_update",
                "@MaTin", model.MaTin,
                "@MaLoai", model.MaLoai,
                "@TieuDe", model.TieuDe,
                "@HinhAnh", model.HinhAnh,
                "@NoiDung", model.NoiDung,
                "@ThoiGian", model.ThoiGian,
                "@TrangThai", model.TrangThai);
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
        public TinTuc GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tintuc_by_id",
                     "@item_MaTin", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinTuc>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TinTuc> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tintuc_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinTuc>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TinTuc> GetDataLoai(int loai)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tintuc_loai","@loai", loai);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinTuc>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TinTuc> GetDataTop3()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "top3_tin_tuc");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinTuc>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TinTuc> Search(int pageIndex, int pageSize, out long total, string tieude, string maloai, string trangthai)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tin_tuc_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tieude", tieude,
                    "@maloai", null,
                    "@trangthai", trangthai);
                if (!string.IsNullOrEmpty(maloai))
                {
                    int ml = int.Parse(maloai.ToString());
                    dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tin_tuc_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tieude", tieude,
                    "@maloai", ml,
                    "@trangthai", trangthai);
                }
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<TinTuc>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
