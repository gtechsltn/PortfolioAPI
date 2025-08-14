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
            CreateMap<Header, HeaderViewDto>()
                .ForMember(dest => dest.LogoPath, opt => opt.Ignore());

            CreateMap<AboutCreateDto, About>()
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
            CreateMap<About, AboutViewDto>()
                .ForMember(dest => dest.AboutImagePath, opt => opt.Ignore());

            CreateMap<IntroCreateDto, Intro>()
                .ForMember(dest => dest.IntroImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.ResumePath, opt => opt.Ignore());
            CreateMap<Intro, IntroViewDto>()
                .ForMember(dest => dest.IntroImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.ResumePath, opt => opt.Ignore());

            CreateMap<ServicesCreateDto, Services>()
                .ForMember(dest => dest.ServiceIconPath, opt => opt.Ignore());
            CreateMap<Services, ServicesViewDto>()
                .ForMember(dest => dest.ServiceIcon, opt => opt.MapFrom(src => src.ServiceIconPath));

            CreateMap<SkillSectionCreateDto, SkillSection>();
            CreateMap<SkillSection, SkillSectionViewDto>();

            CreateMap<SkillDetailCreateDto, SkillDetail>();
            CreateMap<SkillDetail, SkillDetailViewDto>();

            CreateMap<ExperienceCreateDto, Experience>();
            CreateMap<Experience, ExperienceViewDto>();

            CreateMap<EducationCreateDto, Education>();
            CreateMap<Education, EducationViewDto>();

            CreateMap<ReviewCreateDto, Review>()
                .ForMember(dest => dest.ClientImagePath, opt => opt.Ignore());
            CreateMap<Review, ReviewViewDto>()
                .ForMember(dest => dest.ClientImage, opt => opt.MapFrom(src => src.ClientImagePath));

            CreateMap<ContactInfoCreateDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoViewDto>();

            CreateMap<ClientMessageCreateDto, ClientMessage>();
            CreateMap<ClientMessage, ClientMessageViewDto>()
                .ForMember(dest => dest.SentMessageAtRaw, opt => opt.MapFrom(src => src.SentMessageAt));

            CreateMap<User, UserResponseDto>();
            CreateMap<User, UpdateUserRequestDto>();

            CreateMap<SocialLinksCreateDto, SocialLinks>();
            CreateMap<SocialLinks, SocialLinksViewDto>();


        }
    }
}
