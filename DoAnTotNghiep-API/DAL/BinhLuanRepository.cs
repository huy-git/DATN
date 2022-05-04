using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class BinhLuanRepository : IBinhLuanRepository
    {
        private IDatabaseHelper _dbHelper;
        public BinhLuanRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(BinhLuan model)
        {
            string msgError = "";
            try
            {
                string thoigian = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "binh_luan_create",
                "@MaTK",model.MaTK,
                "@Username", model.Username,
                "@HoTen", model.HoTen,
                "@ThoiGian", thoigian,
                "@NoiDung", model.NoiDung,
                "@TrangThai", model.TrangThai,
                "@MaBaiViet", model.MaBaiViet);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "binh_luan_delete",
                "@MaBL", id);
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
        public bool Update(BinhLuan model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "binh_luan_update",
                "@MaBL", model.MaBL,
                "@MaTK", model.MaTK,
                 "@Username", model.Username,
                 "@HoTen", model.HoTen,
                "@ThoiGian", model.ThoiGian,
                "@NoiDung", model.NoiDung,
                "@TrangThai", model.TrangThai,
                "@MaBaiViet", model.MaBaiViet);
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
        public BinhLuan GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "binh_luan_by_id",
                     "@MaBL", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<BinhLuan>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BinhLuan> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "binh_luan_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<BinhLuan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BinhLuan> GetDataBaiViet(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "binh_luan_theo_bai_viet",
                    "@MaBaiViet", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<BinhLuan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BinhLuan> Search(int pageIndex, int pageSize, out long total, int mabv)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "binh_luan_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@mabv", mabv);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<BinhLuan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
