using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object13.Core.Services.Interfaces;

namespace Object13.WebApi.Controllers
{
    public class TestUser : MyBaseController
    {
        #region Ctor
        private IUserService _userService;
        public TestUser(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return new ObjectResult(await _userService.GetAllUsers());
        }
    }
}
