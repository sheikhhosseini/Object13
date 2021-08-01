using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Object13.Core.DTOs.Account;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;
using Object13.Core.Utilites.Extention;

namespace Object13.WebApi.Controllers
{
    public class AccountController : MyBaseController
    {
        #region ctor
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (ModelState.IsValid)
            {
                var reponse = await _userService.RegisterUser(user);
                switch (reponse)
                {
                    case UserRegisterDtoResult.EmailExist:
                        return JsonResponseStatus.Error("EmailExist");
                }
            }
            return JsonResponseStatus.Success();
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            if (ModelState.IsValid)
            {
                var response =  await _userService.LoginUser(login);
                switch (response)
                {
                    case UserLoginDtoResult.Failed:
                        return JsonResponseStatus.NotFound("NotFound");
                    case UserLoginDtoResult.NotAcctive:
                        return JsonResponseStatus.UserNotActivated();
                    case UserLoginDtoResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Object13JwtBearer"));
                        var siginCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOption = new JwtSecurityToken(
                            issuer: "https://localhost:44345/",
                            claims:new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.Email),
                                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                            }
                            ,expires:DateTime.Now.AddMinutes(10)
                            ,signingCredentials:siginCredential);

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
                        return JsonResponseStatus.Success(new
                        {
                            token = tokenString,
                            expireTime = login.RememberMe == "true" ? 7 : 0,
                            firstName = user.FirstName,
                            userId = user.Id
                        });
                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region Check USer Auth
        [HttpPost("checkuserauth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return JsonResponseStatus.Success(new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    email = user.Email
                });
            }
            return JsonResponseStatus.Error("Unauthorized");
        }

        #endregion

        #region Sign Out
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
