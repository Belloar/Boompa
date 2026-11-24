using Boompa.Auth;
using Boompa.Implementations;
using Boompa.Implementations.Repositories;
using Boompa.Implementations.Services.AppServices;
using Boompa.Implementations.Services.UtilityServices;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;

namespace Boompa.Extensions
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddBoompaServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ILearnerRepository, LearnerRepository>();
            services.AddScoped<ILearnerService, LearnerService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISourceMaterialService, SourceMaterialService>();
            services.AddScoped<ISourceMaterialRepository, SourceMaterialRepository>();
            services.AddScoped<IAIQuestionGenerator, TypeformService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IContestRecordRepository, ContestRecordRepository>();
            services.AddScoped<IContestRecordService, ContestRecordService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<FreeTextQuestionBuilder>();
            services.AddScoped<MCQuestionBuilder>();
            services.AddScoped<MRQuestionBuilder>();
            return services;
        }
    }
}
