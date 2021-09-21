using AutoMapper;
using Object13.Core.DTOs.Account;
using Object13.DataLayer.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object13.Core.DTOs.Slider;
using Object13.DataLayer.Models.SiteUtilites;

namespace Object13.WebApi.Mapper
{
    public class MyProfiles
    {
    }

    public class testProfile : Profile
    {
        public testProfile()
        {
            CreateMap<UserProfileInfoDto, User>().ReverseMap();

        }
    }

    public class SliderProfile : Profile
    {
        public SliderProfile()
        {
            CreateMap<SliderDto, Slider>().ReverseMap();
        }
    }
}
