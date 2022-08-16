using ProjectFrameworkCommonLib;
using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFrameworkWeb
{
    public partial class Login : System.Web.UI.Page
    {
        private UserBLL UserBLLObj = new UserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Save the information to the Session Object
                Session.Add("UserID", "");
                Session.Add("UserName", "");
                Session.Add("Role", "");
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(TextBoxUserName.Text)|| string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                LabelStatusMessage.Text = "Please Enter Valid User Name | E Mail and Password";
                return;
            }
            AuthInfo Info = UserBLLObj.GetUserInfo(TextBoxUserName.Text, TextBoxPassword.Text);
                
            if(Info.UserID < 0 )
            {
                //If the id < 0 then the AuthenticationToken stores Error Message
                LabelStatusMessage.Text = Info.AuthenticationToken;
                TextBoxPassword.Focus();
            }
            else
            {
                //Save the information to the Session Object
                UserRole Role = UserBLLObj.GetUserRole(Info.UserID);
                Session.Add("UserID", Info.UserID);
                Session.Add("UserName", TextBoxUserName.Text);
                Session.Add("Role", Role.ToString());
                LabelStatusMessage.Text = "Login Succeessfull..";
                //Redirect to Control Panel Page
                Response.Redirect("~/Admin/AdminSettings");
                //redirect it to the Control Peanl Page
            }
        }
    }
}