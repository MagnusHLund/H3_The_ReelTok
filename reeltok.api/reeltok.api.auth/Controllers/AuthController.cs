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
      

      [HttpGet]
      [Route("test")]
      public async Task<IActionResult> test()
      {
        return Ok("hello"); 
      }
      
    }
}
