using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Object13.Core.DTOs.Account;
using Object13.Core.DTOs.Slider;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;
using Object13.Core.Utilites.Extention;

namespace Object13.WebApi.Controllers
{
    public class SliderController : MyBaseController
    {
        #region Ctor
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet("GetActiveSlider")]
        public async Task<IActionResult> GetActiveSlider()
        {
            var sliders = await _sliderService.GetActiveSlider();
            var res = _mapper.Map<List<SliderDto>>(sliders);
            return  JsonResponseStatus.Success(res);
        }
    }
}
