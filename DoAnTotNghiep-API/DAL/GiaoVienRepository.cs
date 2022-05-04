using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class GiaoVienRepository : IGiaoVienRepository
    {
        private IDatabaseHelper _dbHelper;
        public GiaoVienRepository (IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(GiaoVien model)
        {
            string msgError = "";
            try
            {
               
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "giaovien_create",
                "@MonDay", model.MonDay,
                "@ToDay", model.ToDay,
                "@HoTen", model.HoTen,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@ChucVu", model.ChucVu,
                "@MaLop", model.MaLop);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "giaovien_delete",
                "@MaGV", id);
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
        public bool Update(GiaoVien model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "giaovien_update",
                 "@MaGV", model.MaGV,
                "@MonDay", model.MonDay,
                "@ToDay", model.ToDay,
                "@HoTen", model.HoTen,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@ChucVu", model.ChucVu,
                "@MaLop", model.MaLop);
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
        public GiaoVien GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "giaovien_by_id",
                     "@item_MaGV", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GiaoVien>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GiaoVien> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "giaovien_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GiaoVien>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GiaoVien> Search(int pageIndex, int pageSize, out long total, string hoten )
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "giaovien_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<GiaoVien>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
