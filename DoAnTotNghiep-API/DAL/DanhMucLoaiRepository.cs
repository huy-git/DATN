using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class DanhMucLoaiRepository: IDanhMucLoaiRepository
    {
        private IDatabaseHelper _dbHelper;
        public DanhMucLoaiRepository (IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(DanhMucTin model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "danh_muc_create",
                "@MaLoai", model.MaLoai,
                "@LoaiTin", model.LoaiTin);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "danh_muc_delete",
                "@MaLoai", id);
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
        public bool Update(DanhMucTin model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "danh_muc_update",
                "@MaLoai", model.MaLoai,
                "@LoaiTin", model.LoaiTin);
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
        public DanhMucTin GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "danhmucloai_by_id",
                     "@MaLoai", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<DanhMucTin>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DanhMucTin> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "danhmucloai_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<DanhMucTin>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DanhMucTin> Search(int pageIndex, int pageSize, out long total, string loaitin)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "danh_muc_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@loaitin", loaitin);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<DanhMucTin>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
