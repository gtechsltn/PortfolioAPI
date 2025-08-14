using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Mappings;
using Portfolio.Application.Settings;
using Portfolio.Application.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Service;
using Portfolio.Infrastructure.Services;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Interceptors;
using Portfolio.Persistence.Repositories;
using Portfolio.Persistence.UnitOfWork;

namespace Portfolio.Infrastructure.Extensions

{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PortfolioConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);
            services.AddHttpContextAccessor();
            services.AddScoped<AddUpdateAuditEntitiesSaveChangesInterceptor>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<HeaderDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AboutDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IIntroService, IntroService>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<ISkillSectionService, SkillSectionService>();
            services.AddScoped<ISkillDetailService, SkillDetailService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IContactInfoService, ContactInfoService>();
            services.AddScoped<IClientMessageService, ClientMessageService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IEmailSender, CustomEmailSenderService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ISocialLinksService, SocialLinksService>();


            return services;
        }

    }
}
