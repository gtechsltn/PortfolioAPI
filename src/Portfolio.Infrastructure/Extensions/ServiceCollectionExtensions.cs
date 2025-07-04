using Portfolio.Application.Interfaces;
using Portfolio.Infrastructure.Services;
using Portfolio.Application.Validators;
using Portfolio.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Portfolio.Persistence.Repositories;
using Portfolio.Persistence.UnitOfWork;
using Portfolio.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Portfolio.Infrastructure.Extensions

{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PortfolioConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddHttpContextAccessor();

            services.AddValidatorsFromAssemblyContaining<HeaderDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AboutDtoValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<IAboutService, AboutService>();

            return services;
        }

    }
}
