using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace reeltok.api.auth.Controllers
{
    [ApiController]
    public class AuthController
    {
      private readonly IAuthService _authService; 
      
      public AuthController(IAuthService authService) 
      {
        _authService =  authService; 
      }

    }
}
