using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;

namespace ProjectFramework.Web.Utils
{
    /// <summary>
    /// Summary description for DatabaseUtils
    /// </summary>
    public class DatabaseUtils
    {

        private static OdbcConnection m_ProductConnectionODBC = new OdbcConnection();
        private static OleDbConnection m_ProductConnection = new OleDbConnection();
        private static string m_strDBConnection = "OLEDB";
        public static string m_strConnectionString = string.Empty;

        private string m_strErrorMessage;
        // WebUtils m_objWebUtils = new WebUtils();

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseUtils()
        {
            m_strErrorMessage = String.Empty;
        }

        /// <summary>
        /// Returns the error message
        /// </summary>
        /// <returns>String contains error message</returns>
        public string GetErrorMessage()
        {
            return m_strErrorMessage;
        }

        /// <summary>
        /// Sets the connection string
        /// </summary>
        /// <param name="strConnectionStringOrDSN">String contains the connection string</param>
        /// <param name="strDatabasePathOrURL">String contains Database path or URL</param>
        /// <param name="strUserName">String contains the username</param>
        /// <param name="strPassword">String contains user password</param>
        public void SetConnection(string strConnectionStringOrDSN, string strDatabasePathOrURL, string strUserName, string strPassword)
        {
            strConnectionStringOrDSN += strDatabasePathOrURL;
            m_ProductConnection.ConnectionString = strConnectionStringOrDSN;
        }

        /// <summary>
        /// Sets the connection string
        /// </summary>
        /// <param name="strConnectionString">String contains the connection string</param>
        public static void SetConnectionString(string strConnectionString)
        {
            //set connection string for further reference
            m_strConnectionString = strConnectionString;

            if (strConnectionString.Contains("ODBC") || strConnectionString.Contains("odbc") || strConnectionString.Contains("dsn"))
            {
                m_ProductConnectionODBC.ConnectionString = strConnectionString;
                m_strDBConnection = "ODBC";
            }
            else
            {
                m_ProductConnection.ConnectionString = strConnectionString;
                m_strDBConnection = "OLEDB";
            }
        }

        /// <summary>
        /// Formats the SQL Field
        /// </summary>
        /// <param name="strField">String contains the Field value</param>
        /// <returns>String formatted</returns>
        public string FormatSQLField(string strField)
        {
            string strFormattedField = strField.Replace("'", "''");
            return strFormattedField;

        }

        /// <summary>
        /// Executes the query
        /// </summary>
        /// <param name="strQuery">String contains the query</param>
        /// <returns>True if query is executed successfully</returns>
        public bool ExceuteQuery(string strQuery)
        {
            if (m_strDBConnection == "ODBC")
            {
                try
                {
                    m_ProductConnectionODBC.Open();
                    string strNewQuery = strQuery;

                    using (OdbcCommand ExecuteCommand = new OdbcCommand(strNewQuery, m_ProductConnectionODBC))
                    {
                        ExecuteCommand.ExecuteNonQuery();
                        m_ProductConnectionODBC.Close();
                        return true;
                    }
                }
                catch (Exception ex)
                {

                    m_ProductConnectionODBC.Close();
                    m_strErrorMessage = ex.ToString();

                    return false;
                }
                finally
                {
                    m_ProductConnectionODBC.Close();
                }
            }
            else if (m_strDBConnection == "OLEDB")
            {
                try
                {
                    m_ProductConnection.Open();
                    string strNewQuery = strQuery;

                    using (OleDbCommand ExecuteCommand = new OleDbCommand(strNewQuery, m_ProductConnection))
                    {
                        ExecuteCommand.ExecuteNonQuery();
                        m_ProductConnection.Close();
                        return true;
                    }
                }
                catch (Exception ex)
                {

                    m_ProductConnection.Close();
                    m_strErrorMessage = ex.ToString();

                    return false;
                }
                finally
                {
                    m_ProductConnection.Close();
                }
            }
            return false;
        }


        /// <summary>
        /// Executes the SQL transaction.
        /// </summary>
        /// <param name="strQuery">The query to be put in the transaction.</param>
        /// <returns>true if the transaction was success</returns>
        public bool ExecuteSQLTransaction(string strQuery)
        {
            if (m_strDBConnection == "ODBC")
            {
                using (OdbcConnection odbcConnection = new OdbcConnection(m_strConnectionString))
                {
                    try
                    {
                        odbcConnection.Open();

                        OdbcTransaction odbcTransaction = odbcConnection.BeginTransaction();
                        OdbcCommand odbcCommand = new OdbcCommand(strQuery, odbcConnection, odbcTransaction);
                        try
                        {
                            odbcCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                odbcTransaction.Rollback();
                            }
                            catch (Exception e)
                            {
                                m_strErrorMessage = e.Message;
                                throw new Exception(e.Message, e.InnerException);
                            }
                            m_strErrorMessage = ex.Message;
                            throw new Exception(ex.Message, ex.InnerException);
                        }
                        odbcTransaction.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(Ex.Message, Ex.InnerException);
                    }
                    finally
                    {
                        if (odbcConnection != null)
                        {
                            odbcConnection.Close();
                        }
                    }
                }
            }
            else if (m_strDBConnection == "OLEDB")
            {
                using (OleDbConnection oledbConnection = new OleDbConnection(m_strConnectionString))
                {
                    try
                    {
                        oledbConnection.Open();

                        OleDbTransaction oledbTransaction = oledbConnection.BeginTransaction();
                        OleDbCommand oledbCommand = new OleDbCommand(strQuery, oledbConnection, oledbTransaction);
                        try
                        {
                            oledbCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                oledbTransaction.Rollback();
                            }
                            catch (Exception e)
                            {
                                m_strErrorMessage = e.Message;
                                throw new Exception(e.Message, e.InnerException);
                            }
                            m_strErrorMessage = ex.Message;
                            throw new Exception(ex.Message, ex.InnerException);
                        }
                        oledbTransaction.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(Ex.Message, Ex.InnerException);
                    }
                    finally
                    {
                        if (oledbConnection != null)
                        {
                            oledbConnection.Close();
                        }
                    }
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Executes the SQL transaction Containing Two Queries.
        /// </summary>
        /// <param name="strQuery1">The First Query To Be Executed.</param>
        /// <param name="strQuery2">The Second Query To Be Executed.</param>
        /// <returns>True if transaction completed successfully</returns>
        public bool ExecuteSQLTransaction(string strQuery1, string strQuery2)
        {
            if (m_strDBConnection == "ODBC")
            {
                using (OdbcConnection odbcConnection1 = new OdbcConnection(m_strConnectionString))
                {
                    OdbcConnection odbcConnection2 = null;
                    try
                    {
                        odbcConnection2 = new OdbcConnection(m_strConnectionString);
                        odbcConnection1.Open();
                        odbcConnection2.Open();

                        OdbcTransaction objOdbcTransaction1 = odbcConnection1.BeginTransaction();
                        OdbcTransaction objOdbcTransaction2 = odbcConnection2.BeginTransaction();
                        OdbcCommand objOdbcCommand1 = new OdbcCommand(strQuery1, odbcConnection1, objOdbcTransaction1);
                        OdbcCommand objOdbcCommand2 = new OdbcCommand(strQuery2, odbcConnection2, objOdbcTransaction2);
                        try
                        {
                            objOdbcCommand1.ExecuteNonQuery();
                        }
                        catch (Exception Ex)
                        {
                            try
                            {
                                objOdbcTransaction1.Rollback();
                            }
                            catch (Exception ex)
                            {
                                m_strErrorMessage = ex.Message;
                                throw new Exception(m_strDBConnection + " : Transaction 1 Rollback Failed", ex.InnerException);
                            }
                            m_strErrorMessage = Ex.Message;
                            throw new Exception(m_strDBConnection + " : Transaction 1 Failed", Ex.InnerException);
                        }
                        try
                        {
                            objOdbcCommand2.ExecuteNonQuery();
                        }
                        catch (Exception Ex)
                        {
                            try
                            {
                                objOdbcTransaction1.Rollback();
                                objOdbcTransaction2.Rollback();
                            }
                            catch (Exception ex)
                            {
                                m_strErrorMessage = ex.Message;
                                throw new Exception(m_strDBConnection + " : Transaction 1 or Transaction 2 Rollback Failed", ex.InnerException);
                            }
                            m_strErrorMessage = Ex.Message;
                            throw new Exception(m_strDBConnection + " : Transaction 2 Failed", Ex.InnerException);
                        }
                        objOdbcTransaction1.Commit();
                        objOdbcTransaction2.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(m_strDBConnection + " : Transaction Execution Failed", Ex.InnerException);
                    }
                    finally
                    {
                        if (odbcConnection1 != null)
                        {
                            odbcConnection1.Close();
                        }
                        if (odbcConnection2 != null)
                        {
                            odbcConnection2.Close();
                        }
                    }
                }
            }
            else if (m_strDBConnection == "OLEDB")
            {
                using (OleDbConnection oledbConnection1 = new OleDbConnection(m_strConnectionString))
                {
                    OleDbConnection oledbConnection2 = null;
                    try
                    {
                        oledbConnection2 = new OleDbConnection(m_strConnectionString);

                        oledbConnection1.Open();
                        oledbConnection2.Open();

                        OleDbTransaction oledbTransaction1 = oledbConnection1.BeginTransaction();
                        OleDbTransaction oledbTransaction2 = oledbConnection2.BeginTransaction();
                        OleDbCommand oledbCommand1 = new OleDbCommand(strQuery1, oledbConnection1, oledbTransaction1);
                        OleDbCommand oledbCommand2 = new OleDbCommand(strQuery2, oledbConnection2, oledbTransaction2);
                        try
                        {
                            oledbCommand1.ExecuteNonQuery();
                        }
                        catch (Exception Ex)
                        {
                            try
                            {
                                oledbTransaction1.Rollback();
                            }
                            catch (Exception ex)
                            {
                                m_strErrorMessage = ex.Message;
                                throw new Exception(m_strDBConnection + " : Transaction 1 Rollback Failed", ex.InnerException);
                            }
                            m_strErrorMessage = Ex.Message;
                            throw new Exception(m_strDBConnection + " : Transaction 1 Execution Failed", Ex.InnerException);
                        }
                        try
                        {
                            oledbCommand2.ExecuteNonQuery();
                        }
                        catch (Exception Ex)
                        {
                            try
                            {
                                oledbTransaction1.Rollback();
                                oledbTransaction2.Rollback();
                            }
                            catch (Exception ex)
                            {
                                m_strErrorMessage = ex.Message;
                                throw new Exception(m_strDBConnection + " : Transaction 1 or Transaction 2 Rollback Failed", ex.InnerException);
                            }
                            m_strErrorMessage = Ex.Message;
                            throw new Exception(m_strDBConnection + " : Transaction 2 Execution Failed", Ex.InnerException);
                        }
                        oledbTransaction1.Commit();
                        oledbTransaction2.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(m_strDBConnection + " : Transaction Execution failed", Ex.InnerException);
                    }
                    finally
                    {
                        if (oledbConnection1 != null)
                        {
                            oledbConnection1.Close();
                        }
                        if (oledbConnection2 != null)
                        {
                            oledbConnection2.Close();
                        }
                    }
                }
            }
            return false;
        }

        //#if TEST_
        public bool ExecuteSQLTransactionEx(List<string> strQueryList)
        {
            if (m_strDBConnection == "ODBC")
            {
                using (OdbcConnection odbcConnection1 = new OdbcConnection(m_strConnectionString))
                {

                    try
                    {
                        odbcConnection1.Open();


                        OdbcTransaction objOdbcTransaction1 = odbcConnection1.BeginTransaction();

                        for (int i = 0; i < strQueryList.Count; i++)
                        {
                            try
                            {

                                OdbcCommand objOdbcCommand1 = new OdbcCommand(strQueryList[i], odbcConnection1, objOdbcTransaction1);
                                objOdbcCommand1.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                                objOdbcTransaction1.Rollback();
                                return false;
                            }
                        }

                        objOdbcTransaction1.Commit();

                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(m_strDBConnection + " : Transaction Execution Failed", Ex.InnerException);
                    }
                    finally
                    {
                        if (odbcConnection1 != null)
                        {
                            odbcConnection1.Close();
                        }

                    }
                }
            }
            else if (m_strDBConnection == "OLEDB")
            {
                using (OleDbConnection oledbConnection1 = new OleDbConnection(m_strConnectionString))
                {
                    //OleDbConnection oledbConnection2 = null;
                    try
                    {
                        // oledbConnection2 = new OleDbConnection(m_strConnectionString);

                        oledbConnection1.Open();
                        //oledbConnection2.Open();

                        OleDbTransaction oledbTransaction1 = oledbConnection1.BeginTransaction();
                        //OleDbTransaction oledbTransaction2 = oledbConnection2.BeginTransaction();
                        // OleDbCommand oledbCommand1 = new OleDbCommand(strQuery1, oledbConnection1, oledbTransaction1);
                        //OleDbCommand oledbCommand2 = new OleDbCommand(strQuery2, oledbConnection2, oledbTransaction2);
                        for (int j = 0; j < strQueryList.Count; j++)
                        {
                            try
                            {

                                OleDbCommand objOledbCommand1 = new OleDbCommand(strQueryList[j], oledbConnection1, oledbTransaction1);
                                objOledbCommand1.ExecuteNonQuery();
                            }

                            catch (Exception)
                            {
                                oledbTransaction1.Rollback();
                                return false;
                            }
                        }
                        // try
                        //{
                        //    catch (Exception ex)
                        //    {
                        //        m_strErrorMessage = ex.Message;
                        //        throw new Exception(m_strDBConnection + " : Transaction 1 Rollback Failed", ex.InnerException);
                        //    }
                        //    m_strErrorMessage = Ex.Message;
                        //    throw new Exception(m_strDBConnection + " : Transaction 1 Execution Failed", Ex.InnerException);
                        //}
                        //try
                        //{
                        //    oledbCommand2.ExecuteNonQuery();
                        //}
                        //catch (Exception Ex)
                        //{
                        //    try
                        //    {
                        //        oledbTransaction1.Rollback();
                        //        oledbTransaction2.Rollback();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        m_strErrorMessage = ex.Message;
                        //        throw new Exception(m_strDBConnection + " : Transaction 1 or Transaction 2 Rollback Failed", ex.InnerException);
                        //    }
                        //    m_strErrorMessage = Ex.Message;
                        //    throw new Exception(m_strDBConnection + " : Transaction 2 Execution Failed", Ex.InnerException);
                        //}
                        oledbTransaction1.Commit();
                        //oledbTransaction2.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        m_strErrorMessage = Ex.Message;
                        throw new Exception(m_strDBConnection + " : Transaction Execution failed", Ex.InnerException);
                    }
                    finally
                    {
                        if (oledbConnection1 != null)
                        {
                            oledbConnection1.Close();
                        }
                        //if (oledbConnection2 != null)
                        //{
                        //    oledbConnection2.Close();
                        //}
                    }
                }
            }
            return false;
        }
        //#endif


        /// <summary>
        /// Returns records of a particular table 
        /// </summary>
        /// <param name="strTableName">String contains table name</param>
        /// <param name="strQuery">String contains the query</param>
        /// <returns>An object of Dataset contains the records</returns>
        public System.Data.DataSet GetRecords(string strTableName, string strQuery)
        {
            if (m_strDBConnection == "ODBC")
            {
                using (OdbcConnection odbcConnection1 = new OdbcConnection(m_strConnectionString))
                {
                    try
                    {
                        //odbcConnection1.Open();
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter())
                        {
                            using (adapter.SelectCommand = new OdbcCommand(strQuery, odbcConnection1))
                            {

                                DataSet dataset = new DataSet();
                                if ("" != strTableName)
                                {
                                    //dataset.Tables.Add(strTableName);
                                    //adapter.Fill(dataset.Tables[strTableName]);
                                    adapter.Fill(dataset, strTableName);

                                }
                                else
                                {
                                    adapter.Fill(dataset);

                                }
                                return dataset;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        m_strErrorMessage = ex.ToString();
                        return null;
                    }
                    finally
                    {
                        odbcConnection1.Close();
                    }
                }
            }
            else //if(m_strDBConnection == "OLEDB")
            {
                using (OleDbConnection oledbConnection1 = new OleDbConnection(m_strConnectionString))
                {
                    try
                    {
                        //oledbConnection1.Open();
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {
                            using (adapter.SelectCommand = new OleDbCommand(strQuery, oledbConnection1))
                            {

                                DataSet dataset = new DataSet();
                                if ("" != strTableName)
                                {
                                    //dataset.Tables.Add(strTableName);
                                    //adapter.Fill(dataset.Tables[strTableName]);
                                    adapter.Fill(dataset, strTableName);

                                }
                                else
                                {
                                    adapter.Fill(dataset);

                                }

                                return dataset;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        m_strErrorMessage = ex.ToString();
                        return null;
                    }
                    finally
                    {
                        oledbConnection1.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Returns records
        /// </summary>
        /// <param name="strTableName">String contains table name</param>
        /// <param name="strQuery">String contains the query</param>
        /// <param name="strDatasetName">String contains the data set nane</param>
        /// <returns>An object of System.Data.DataSet contains the records</returns>
        public System.Data.DataSet GetRecords(string strTableName, string strQuery, string strDatasetName)
        {
            if (m_strDBConnection == "ODBC")
            {
                using (OdbcConnection odbcConnection1 = new OdbcConnection(m_strConnectionString))
                {
                    try
                    {
                        //odbcConnection1.Open();
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter())
                        {
                            using (adapter.SelectCommand = new OdbcCommand(strQuery, odbcConnection1))
                            {

                                DataSet dataset = new DataSet(strDatasetName);
                                if ("" != strTableName)
                                {
                                    dataset.Tables.Add(strTableName);
                                    adapter.Fill(dataset.Tables[strTableName]);

                                }
                                else
                                {
                                    adapter.Fill(dataset);

                                }
                                return dataset;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        m_strErrorMessage = ex.ToString();

                        return null;
                    }
                    finally
                    {
                        odbcConnection1.Close();

                    }
                }
            }
            else //if(m_strDBConnection == "OLEDB")
            {
                using (OleDbConnection oledbConnection1 = new OleDbConnection(m_strConnectionString))
                {
                    try
                    {
                        //oledbConnection1.Open();
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {
                            using (adapter.SelectCommand = new OleDbCommand(strQuery, oledbConnection1))
                            {

                                DataSet dataset = new DataSet(strDatasetName);
                                if ("" != strTableName)
                                {
                                    dataset.Tables.Add(strTableName);
                                    adapter.Fill(dataset.Tables[strTableName]);

                                }
                                else
                                {
                                    adapter.Fill(dataset);

                                }
                                return dataset;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        m_strErrorMessage = ex.ToString();

                        return null;
                    }
                    finally
                    {
                        oledbConnection1.Close();

                    }
                }
            }
        }

        public static bool IsDatabaseMSAccess(string strConnectionString)
        {
            string strMSAccessKeyWord = "microsoft.jet.oledb";
            strConnectionString = strConnectionString.ToLower();
            if (strConnectionString.Contains(strMSAccessKeyWord))
            {
                return true;
            }
            return false;
        }

        public static bool IsDatabaseMySQL()
        {
            string strConnectionString = m_strConnectionString;

            string strMSAccessKeyWord = "mysql";
            strConnectionString = strConnectionString.ToLower();
            if (strConnectionString.Contains(strMSAccessKeyWord))
            {
                return true;
            }
            return false;
        }

    }
}
