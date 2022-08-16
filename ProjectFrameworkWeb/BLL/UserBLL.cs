using ProjectFrameworkCommonLib;
using System;
using System.Data;

namespace ProjectFrameworkWeb.BLL
{
    public enum UserRole { None,NormalUser, Admin }
    public class UserBLL : BLLBase
    {
        public AuthInfo GetUserInfo(string UserNameOrEmail, string Password)
        {
            AuthInfo Info = new AuthInfo();
            try
            {
                //Encrypt the password to compare that with the Database
                string EncryptedPassword = Encrypt(Password);
                string QueryString = "select ID , EMail from user_info_tb where UserName='" + UserNameOrEmail + "' and Password='"+EncryptedPassword+"'";
                if(UserNameOrEmail.Contains("@"))
                {
                    //The Given Query is with E Mail so change the SQL Query
                    QueryString = "select ID , EMail from user_info_tb where EMail='"+ UserNameOrEmail + "' and Password='"+EncryptedPassword+"'";
                }
                DataSet ds = m_objDatabaseUtils.GetRecords("UserInfo", QueryString);

                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {
                    Info.UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    string EMail = ds.Tables[0].Rows[0]["EMail"].ToString();
                    Info.AuthenticationToken = GetAuthenticationToken(EMail, Info.UserID.ToString());

                }
                else
                {
                    Info.AuthenticationToken = "Invalid User Name | EMail or Password";
                }
                return Info;
            }
            catch(Exception Ex)
            {
                //the ID will be -1 
                Info.AuthenticationToken = Ex.Message;
                return Info;
            }
            
        }
        public UserRole GetUserRole(int UserID)
        {
            UserRole Role= UserRole.None;
            try
            {
                string QueryString = "select Role from user_info_tb where ID=" + UserID.ToString() + " ";
                DataSet ds = m_objDatabaseUtils.GetRecords("UserInfo", QueryString);
                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {
                    Role = (UserRole)Enum.Parse(typeof(UserRole), ds.Tables[0].Rows[0]["Role"].ToString());
                }
                return Role;
            }
            catch(Exception)
            {
                return Role;
            }
        }

        public bool CheckValidUser(string Email , string UserID)
        {
            bool bValid=false;
            try
            {
                string QueryString = "select Role from user_info_tb where ID=" + UserID.ToString() + " and EMail='"+ Email + "' ";
                DataSet ds = m_objDatabaseUtils.GetRecords("UserInfo", QueryString);
                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {
                    bValid = true;
                }
                return bValid;
            }
            catch (Exception)
            {
                return bValid;
            }

        }
    }
}