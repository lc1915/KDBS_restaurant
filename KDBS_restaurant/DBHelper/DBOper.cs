using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KDBS_restaurant
{
    public class DBOper
    {
        private string strCon = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        /// <summary>
        /// 数据库连接参数
        /// </summary>
        public string StrConnection
        {
            get { return strCon; }
            set
            {
                strCon = value;
                dbtran.StrConnection = strCon;
            }
        }

        private DBOperTran dbtran = new DBOperTran();

        public DBOperTran TranOper
        {
            get { return dbtran; }
        }

        #region 测试连接
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="strcon">连接参数</param>
        /// <returns></returns>
        public static bool TestConnect(string strcon)
        {
            try
            {
                SqlConnection tempcon = new SqlConnection(strcon);
                tempcon.Open();
                tempcon.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 执行增/删/更新操作
        /// <summary>
        /// 执行Insert,Update,Delete操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNoQuery(string sql)
        {
            SqlConnection connsql = new SqlConnection(strCon);
            try
            {

                connsql.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connsql;
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connsql.Close();
            }
        }
        #endregion

        #region 执行存储过程/增/删/更新操作
        /// <summary>
        /// 执行存储过程
        /// 增 删 更新操作
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="paraValues">参数</param>
        /// <returns></returns>
        public int ExecuteProcNoQuery(string name, params object[] paraValues)
        {
            SqlConnection conn = new SqlConnection(strCon);
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddInParaValues(cmd, paraValues);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行存储过程
        /// 增 删 更新操作
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="para"></param>
        /// <param name="paravalues"></param>
        /// <returns></returns>
        public int ExecuteProcNoQuery(string name, SqlParameter para, params SqlParameter[] paravalues)
        {
            SqlConnection conn = new SqlConnection(strCon);
            try
            {
                int result;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(para);
                    if (paravalues.Length > 0)
                        cmd.Parameters.AddRange(paravalues);
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return result;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region GetSingle
        public object GetSingle(string sql)
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }

                }
            }
        }
        #endregion

        #region ExecuteReader
        public SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection connsql = new SqlConnection(strCon);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connsql);
                connsql.Open();

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    connsql.Close();
            //}
        }
        #endregion

        #region 执行查询语句
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteSQL(string sql)
        {
            SqlConnection connsql = new SqlConnection(strCon);
            try
            {
                connsql.Open();
                DataTable dt = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(sql, connsql))
                {
                    adp.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connsql.Close();
            }
        }
        #endregion

        #region 执行存储过程  查询语句
        /// <summary>
        /// 执行存储过程
        /// 查询语句
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public DataTable ExecuteProc(string name, params object[] paraValues)
        {
            SqlConnection sqlConn = null;
            try
            {
                sqlConn = new SqlConnection(strCon);

                DataTable dt = new DataTable();
                using (SqlCommand comm = new SqlCommand(name, sqlConn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    AddInParaValues(comm, paraValues);
                    sqlConn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(comm);
                    sda.Fill(dt);
                    comm.Parameters.Clear();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        #endregion

        #region 执行存储过程 查询语句
        public DataTable ExecuteProc(string name, SqlParameter para, params SqlParameter[] paraValues)
        {
            SqlConnection conn = new SqlConnection(strCon);

            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                using (SqlCommand comm = new SqlCommand(name, conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add(para);
                    if (paraValues.Length > 0)
                        comm.Parameters.AddRange(paraValues);

                    SqlDataAdapter sda = new SqlDataAdapter(comm);
                    sda.Fill(dt);
                    comm.Parameters.Clear();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 事务处理
        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="lstSql">SQL语句集合</param>
        /// <returns></returns>
        public bool BatchExecute(List<string> lstSql)
        {
            SqlConnection conn = new SqlConnection(strCon);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            using (SqlCommand cmn = new SqlCommand())
            {
                cmn.Connection = conn;
                cmn.Transaction = tran;
                try
                {
                    foreach (string sql in lstSql)
                    {
                        cmn.CommandText = sql;
                        cmn.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region 更新DataTable
        /// <summary>
        /// 更新DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool UpdataTable(string sql, DataTable dt)
        {
            SqlConnection conn = new SqlConnection(strCon);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                SqlCommandBuilder cbd = new SqlCommandBuilder(adt);
                adt.Update(dt);
                dt.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region
        private ArrayList GetParas(string _name)
        {
            SqlConnection conn = new SqlConnection(strCon);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("dbo.sp_sproc_columns_90", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@procedure_name", _name);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ArrayList al = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    al.Add(dt.Rows[i][3].ToString());
                }
                return al;
            }
            catch (Exception ex)
            { throw ex; }
            finally { conn.Close(); }
        }
        // 为 SqlCommand 添加参数及赋值。
        private void AddInParaValues(SqlCommand comm, params object[] paraValues)
        {
            if (paraValues.Length == 0)
                return;

            comm.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
            comm.Parameters["@RETURN_VALUE"].Direction =
            ParameterDirection.ReturnValue;
            if (paraValues != null)
            {
                ArrayList al = GetParas(comm.CommandText);
                for (int i = 0; i < paraValues.Length; i++)
                {
                    comm.Parameters.AddWithValue(al[i + 1].ToString(), paraValues[i]);
                }
            }
        }
        #endregion

        #region 大量数据插入

        public DataTable GetTableStructureFromDb(string tablename)
        {
            string sql;
            sql = "select top 1 * from " + tablename;
            DataTable dt = ExecuteSQL(sql);
            dt.Rows.Clear();
            return dt;
        }

        public void BulkInsert(string tablename, DataTable dtData)
        {
            if (dtData == null || dtData.Rows.Count == 0)
                return;

            SqlConnection connsql = new SqlConnection(strCon);
            connsql.Open();
            SqlTransaction sqlTran = connsql.BeginTransaction();
            try
            {

                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connsql, SqlBulkCopyOptions.Default, sqlTran);
                sqlBulkCopy.DestinationTableName = tablename;
                sqlBulkCopy.BatchSize = dtData.Rows.Count;
                sqlBulkCopy.WriteToServer(dtData);
                sqlTran.Commit();
                sqlBulkCopy.Close();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                connsql.Close();
            }

        }

        public void BulkInsert(DataSet ds)
        {

            SqlConnection connsql = new SqlConnection(strCon);
            connsql.Open();
            SqlTransaction sqlTran = connsql.BeginTransaction();
            try
            {

                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connsql, SqlBulkCopyOptions.Default, sqlTran);
                foreach (DataTable dt in ds.Tables)
                {
                    sqlBulkCopy.DestinationTableName = dt.TableName;
                    sqlBulkCopy.BatchSize = dt.Rows.Count;
                    sqlBulkCopy.WriteToServer(dt);
                }
                sqlTran.Commit();
                sqlBulkCopy.Close();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                connsql.Close();
            }
        }

        #endregion

    }

    public class DBOperTran
    {
        private string strCon = "";
        /// <summary>
        /// 数据库连接参数
        /// </summary>
        public string StrConnection
        {
            get { return strCon; }
            set { strCon = value; }
        }

        bool blnBegin = false;
        SqlConnection sqlconn;
        SqlTransaction sqltran;

        public void BeginTran()
        {
            if (blnBegin)
                throw new Exception("当前事物正在运动,尚没有结束!");

            sqlconn = new SqlConnection(strCon);
            sqlconn.Open();
            sqltran = sqlconn.BeginTransaction();
            blnBegin = true;
        }

        public bool ExecuteNoQuery(List<string> lstSql)
        {
            using (SqlCommand cmn = new SqlCommand())
            {
                cmn.Connection = sqlconn;
                cmn.Transaction = sqltran;
                try
                {
                    foreach (string sql in lstSql)
                    {
                        cmn.CommandText = sql;
                        cmn.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void BulkInsert(DataSet ds)
        {
            try
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlconn, SqlBulkCopyOptions.Default, sqltran);
                foreach (DataTable dt in ds.Tables)
                {
                    sqlBulkCopy.DestinationTableName = dt.TableName;
                    sqlBulkCopy.BatchSize = dt.Rows.Count;
                    sqlBulkCopy.WriteToServer(dt);
                }
                sqlBulkCopy.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Commit()
        {
            if (!blnBegin)
                throw new Exception("没有需要提交数据!");

            sqltran.Commit();
            sqlconn.Close();
            blnBegin = false;
        }

        public void RollBack()
        {
            if (!blnBegin)
                throw new Exception("没有需要回滚数据!");
            sqltran.Rollback();
            sqlconn.Close();
            blnBegin = false;
        }
    }
}
