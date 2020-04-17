using AutoMapper;
using Identity.Application.DTOs;
using Identity.Core.Models;

namespace Identity.Application.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TokenDto, Token>();
            CreateMap<Token, TokenDto>();
        }
    }
}