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

            CreateMap<User, UserDto>()
                .ForMember(dst => dst.Roles, 
                    dst => dst.MapFrom(src => src.Roles));
            CreateMap<UserDto, User>()
                .ForMember(dst => dst.Roles, 
                    dst => dst.MapFrom(src => src.Roles));
        }
    }
}