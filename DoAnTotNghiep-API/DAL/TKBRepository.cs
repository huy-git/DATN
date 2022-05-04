using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class TKBRepository: ITKBRepository
    {
        private IDatabaseHelper _dbHelper;
        public TKBRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(TKB model)
        {
            string msgError = "";
            try
            {
                var test = model;
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tkb_create",
                "@MaLop", model.MaLop,
                "@TenMon", model.TenMon,
                "@TietDay", model.TietDay,
                 "@Thu", model.Thu,
                "@GVCN", model.GVCN);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tkb_delete",
                "@MaTKB", id);
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
        public bool Update(TKB model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "tin_tuc_update",
               "@MaTKB", model.MaTKB,
                "@MaLop", model.MaLop,
                "@TenMon", model.TenMon,
                "@TietDay", model.TietDay,
                 "@Thu", model.Thu,
                "@GVCN", model.GVCN);
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
        public TKB GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tkb_by_lop",
                     "@MaLop", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TKB>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TKB> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tkb_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TKB>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TKB> GetDataLop(string MaLop)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tkb_get_by_lop","@MaLop",MaLop);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TKB>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TKB> Search(int pageIndex, int pageSize, out long total, string malop)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "tkb_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@malop", malop);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<TKB>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
