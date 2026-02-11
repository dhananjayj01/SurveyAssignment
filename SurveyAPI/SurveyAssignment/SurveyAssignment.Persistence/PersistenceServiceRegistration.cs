using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyAssignment.Application.Contracts.Persistence;
using SurveyAssignment.Persistence.Contexts;
using SurveyAssignment.Persistence.Repositories;

namespace SurveyAssignment.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddInterfaceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            return services;
        }
    }
}
