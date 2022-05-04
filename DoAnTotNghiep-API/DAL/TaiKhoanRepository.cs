using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class TaiKhoanRepository : ITaiKhoanRepository
    {
        private IDatabaseHelper _dbHelper;
        public TaiKhoanRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(TaiKhoan model)
        {
            string msgError = "";
            try
            {
                var test = model;
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "taikhoan_create",
                "@Username", model.Username,
                "@Password", model.Password,
                "@HoTen", model.HoTen,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@Email", model.Email,
                "@PhanQuyen", model.PhanQuyen);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "taikhoan_delete",
                "@MaTK", id);
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
        public bool Update(TaiKhoan model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "taikhoan_update",
               "@MaTK", model.MaTK,
               "@Username", model.Username,
                "@Password", model.Password,
                "@HoTen", model.HoTen,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@Email", model.Email,
                "@PhanQuyen", model.PhanQuyen);
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
        public TaiKhoan GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "taikhoan_by_id",
                     "@item_id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TaiKhoan>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TaiKhoan> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "taikhoan_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TaiKhoan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TaiKhoan GetUser(string username, string password)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tai_khoan_get_by_username_password",
                     "@taikhoan", username,
                     "@matkhau", password);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TaiKhoan>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TaiKhoan> Search(int pageIndex, int pageSize, out long total, string username, string hoten)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "taikhoan_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten,
                    "@username", username);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<TaiKhoan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
