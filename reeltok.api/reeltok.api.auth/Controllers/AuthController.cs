using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.DTOs;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces;

namespace reeltok.api.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
      private readonly IAuthService _authService; 
      
      public AuthController(IAuthService authService) 
      {
        _authService = authService; 
      }
      

      [HttpPost]
      [Route("RegisterUser")]
      public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto request)
      {
        
        RegisterDetails registerDetails = new RegisterDetails(request.UserId, request.PlainTextPassword);

        await _authService.RegisterUser(registerDetails);

        return Ok(new RegisterUserResponseDto(true)); 
      }

      [HttpPost]
      [Route("Login")]
      public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto request)
      {
        LoginCredentials loginCredentials = new LoginCredentials(request.UserId, request.PlainTextPassword);

        Tokens tokens = await _authService.LoginUser(loginCredentials);

        HttpContext.Response.Cookies.Append("AccessToken", tokens.AccessToken.Token, 
          new CookieOptions 
          {
              Expires = tokens.AccessToken.ExpireTime,
              HttpOnly = true,
              Secure = true
          });
        
        HttpContext.Response.Cookies.Append("RefreshToken", tokens.RefreshToken.Token,
          new CookieOptions
          {
              Expires = tokens.RefreshToken.ExpireDate,
              HttpOnly = true,
              Secure = true,
          });

        return Ok(new LoginUserResponseDto(true));
      }

      [HttpGet]
      [Route("RefreshAccessToken")]
      public async Task<IActionResult> RefreshAccessToken()
      {
        string refreshToken = HttpContext.Request.Cookies["RefreshToken"];
         
        AccessToken newAccessToken = await _authService.RefreshAccessToken(refreshToken);

        HttpContext.Response.Cookies.Append("AccessToken", newAccessToken.Token,
          new CookieOptions
          {
              Expires = newAccessToken.ExpireTime,
              HttpOnly = true,
              Secure = true,
          });

        return Ok(new RefreshTokenResponseDto(true));
      }

      [HttpDelete]
      [Route("DeleteUser")]
      public async Task<IActionResult> DeleteUser()
      {
        throw new NotImplementedException(); 
      }
    }
}
