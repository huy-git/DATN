using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class HocSinhRepository: IHocSinhRepository
    {
        private IDatabaseHelper _dbHelper;
        public HocSinhRepository (IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(HocSinh model)
        {
            string msgError = "";
            try
            {

                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "hocsinh_create",
                "@MaLop", model.MaLop,
                "@HoTen", model.HoTen,
                "@GioiTinh", model.GioiTinh,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@KhoaHoc ", model.KhoaHoc);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "hocsinh_delete",
                "@MaHS", id);
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
        public bool Update(HocSinh model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "hocsinh_update",
                 "@MaHS", model.MaHS,
                "@MaLop", model.MaLop,
                "@HoTen", model.HoTen,
                "@GioiTinh", model.GioiTinh,
                "@NgaySinh", model.NgaySinh,
                "@DiaChi", model.DiaChi,
                "@SDT", model.SDT,
                "@KhoaHoc ", model.KhoaHoc);
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
        public HocSinh GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "hocsinh_by_id",
                     "@item_MaHS", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HocSinh>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HocSinh> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "hocsinh_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HocSinh>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HocSinh> Search(int pageIndex, int pageSize, out long total, string hoten)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "hocsinh_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<HocSinh>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
