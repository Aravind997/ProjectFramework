using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectFrameworkCommonLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectFramework.Web.API
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // GET: api/<controller>
        [Route("ValidateUser")]
        [HttpGet]
        public ActionResult ValidateUser(string UserID, string Password)
        {
            try
            {
                AuthInfo Info = new AuthInfo();
                if (UserID=="admin" && Password=="admin")
                {
                    
                    Info.UserID = 1;
                    Info.AuthenticationToken = "1234567";
                    return Ok(Info);

                }
                else
                {
                    Info.UserID = -1;
                    Info.AuthenticationToken = "User Not Found";
                    return NotFound(Info);
                }
                
                
            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }
        }

    }
}
