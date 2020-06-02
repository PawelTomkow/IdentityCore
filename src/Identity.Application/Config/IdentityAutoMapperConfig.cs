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

            CreateMap<User, UserDto>()
                .ForMember(src => src.UserRoles, 
                    dst => dst.MapFrom(opt => opt.UserRole.Select(x => x.Role)));
            CreateMap<UserDto, User>()
                .ForMember(src => src.UserRole, opt => opt.Ignore());
        }
    }
}