using System.Linq;
using AutoMapper;
using Identity.Application.DTOs;
using Identity.Core.Models;

namespace Identity.Application.Config
{
    public class IdentityAutoMapperConfig : Profile
    {
        public IdentityAutoMapperConfig()
        {
            CreateMap<TokenDto, Token>();
            CreateMap<Token, TokenDto>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            
        }
    }
}