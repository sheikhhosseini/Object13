using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;

namespace Object13.WebApi.Controllers
{
    public class SliderController : MyBaseController
    {
        #region Ctor
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAllSliders()
        {
            return  JsonResponseStatus.Success(await _sliderService.GetActiveSlider());
        }
    }
}
