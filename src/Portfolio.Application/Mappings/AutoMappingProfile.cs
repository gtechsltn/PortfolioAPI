using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Mappings
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<HeaderCreateDto, Header>()
                .ForMember(dest => dest.LogoPath, opt => opt.Ignore());

            CreateMap<AboutCreateDto, About>()
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.ResumePath, opt => opt.Ignore());

        }
    }
}
