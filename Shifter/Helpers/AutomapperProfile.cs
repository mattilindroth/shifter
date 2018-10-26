using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Model;
using Shifter.DataTransferObjects;

namespace Shifter.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
