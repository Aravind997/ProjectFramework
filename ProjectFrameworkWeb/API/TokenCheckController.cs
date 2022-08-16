using Microsoft.Ajax.Utilities;
using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProjectFrameworkWeb.API
{
    public class TokenCheckController : ApiController
    {
        private UserBLL UserBLLObj = new UserBLL();
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            
            if (SettingsBLL.UseAuthToken)
            {
                if (controllerContext.Request.Headers.Authorization == null)
                {
                    return Task<HttpResponseMessage>.Factory.StartNew(() =>
                    {
                        return controllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Header Data Missing");
                    });

                }
                else
                {
                    string authenticationToken = controllerContext.Request.Headers.Authorization.Parameter;
                    string decryptedToken = "";
                    try
                    {
                        decryptedToken = BLLBase.Decrypt(authenticationToken);
                    }
                    catch(Exception)
                    {
                        return Task<HttpResponseMessage>.Factory.StartNew(() =>
                        {
                            return controllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Authentication Token");
                        });
                    }
                    string[] Token = decryptedToken.Split(':');
                    if ((Token.Length == 2) && (UserBLLObj.CheckValidUser(Token[0], Token[1])))
                    {
                        return base.ExecuteAsync(controllerContext, cancellationToken);
                    }
                    else
                    {
                        return Task<HttpResponseMessage>.Factory.StartNew(() =>
                        {
                            return controllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication Token Mismatch");
                        });

                    }
                }
            }
            else
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }

        }
    }

}