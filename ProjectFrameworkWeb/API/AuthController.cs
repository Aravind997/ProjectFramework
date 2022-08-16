using ProjectFrameworkCommonLib;
using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectFrameworkWeb.API
{
    public class AuthController : ApiController
    {
        private UserBLL UserBLLObj = new UserBLL();

        [HttpGet]
        public HttpResponseMessage ValidateUser(string UserID,string Password)
        {
            try
            {
                
                AuthInfo Info = UserBLLObj.GetUserInfo(UserID, Password);

                if (Info.UserID!=-1)
                {
                    var message = Request.CreateResponse(HttpStatusCode.OK, Info);
                    message.Headers.Location = new Uri(Request.RequestUri.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, Info.AuthenticationToken);
                }
            }
            catch (Exception Ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
        }

        
    }
}