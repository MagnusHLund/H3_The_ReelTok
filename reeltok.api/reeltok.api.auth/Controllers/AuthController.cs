using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace reeltok.api.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
      private readonly IAuthService _authService; 
      
      public AuthController(IAuthService authService) 
      {
        _authService =  authService; 
      }
      

      [HttpPost]
      [Route("RegisterUser")]
      public async Task<IActionResult> RegisterUser()
      {
        throw new NotImplementedException();
      }
      
    }
}
