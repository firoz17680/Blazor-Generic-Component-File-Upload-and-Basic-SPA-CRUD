using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController: ControllerBase
    {
        IAuthenticate authenticateService;

        public TokenController(IAuthenticate _authenticateService)
        {
            authenticateService = _authenticateService;
        }

        [HttpPost]
        public ActionResult<string> Post(User user)
        {
            var validUser = authenticateService.AuthenticateUser(user.Name, user.Password);

            if(validUser == null)
            {
                return BadRequest(new { message = "User Name and password is incorrect" });
            }

            return validUser.Token;

        }


    }
}
