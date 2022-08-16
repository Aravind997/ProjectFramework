using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace ProjectFrameworkWeb.BLL
{
    public class SettingsBLL : BLLBase
    {
        public static bool UseAuthToken { get; set; } = false;

        public static void SetAuthToken(bool bEnable)
        {
            UseAuthToken = bEnable;
        }
        public string GetValue(string Key)
        {
            string Data = "";
            try
            {
                string QueryString = "select SettingsValue from settings_tb where SettingsKey='" + Key + "'";
                DataSet ds = m_objDatabaseUtils.GetRecords("UserInfo", QueryString);

                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {
                    Data = ds.Tables[0].Rows[0]["SettingsValue"].ToString();

                }

                return Data;
            }
            catch(Exception)
            {
                return Data;
            }
        }
        public bool IsSettingsFiledPresent(string Key)
        {
            bool bResult = false;
            try
            {
                string QueryString = "select SettingsValue from settings_tb where SettingsKey='" + Key + "'";
                DataSet ds = m_objDatabaseUtils.GetRecords("UserInfo", QueryString);

                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {
                    bResult = true;

                }
                return bResult;
            }
            catch(Exception)
            {
                return bResult;
            }
        }
        public void SetValue(string Key, string Value)
        {
            
            string Query = "";
            try
            {
                if (IsSettingsFiledPresent(Key))
                {
                    Query = "update settings_tb set SettingsValue='" + Value + "' where SettingsKey='" + Key + "'";
                }
                else
                {
                    Query = "insert into settings_tb (SettingsKey,SettingsValue) values ('" + Key + "','" + Value + "')";
                }
                m_objDatabaseUtils.ExceuteQuery(Query);
                
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            
        }
    }
}