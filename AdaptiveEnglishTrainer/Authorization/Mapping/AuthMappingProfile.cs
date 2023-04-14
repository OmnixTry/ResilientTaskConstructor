using AdaptiveEnglishTrainer.Authorization.Entity;
using AutoMapper;

namespace AdaptiveEnglishTrainer
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserRegistrationRequestDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
