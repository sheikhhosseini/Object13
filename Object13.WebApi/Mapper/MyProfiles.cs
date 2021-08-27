using AutoMapper;
using Object13.Core.DTOs.Account;
using Object13.DataLayer.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}
