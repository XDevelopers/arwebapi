using AltaRail.Application.DTOs;
using AltaRail.Domain;
using AutoMapper;

namespace AltaRail.Application.Mappings
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();            
        }
    }
}
