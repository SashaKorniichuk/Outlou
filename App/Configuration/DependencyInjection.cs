using App.OptionsSetup;
using Infrastructure.BackgroundJobs;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Quartz;
using Scrutor;


namespace App.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Persistence.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Outlou.Application.AssemblyReference.Assembly);
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                dbContextOptionBuilder =>
                {
                    var connectionString = configuration.GetConnectionString("Database");

                    dbContextOptionBuilder.UseSqlServer(connectionString);
                });

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
                 .AddControllers()
                 .AddApplicationPart(Presentation.AssemblyReference.Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Outlou", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddQuartz(configure =>
             {
                 var jobKey = new JobKey(nameof(UpdateListOfNewsFromTheFeedJob));
            
                 configure
                     .AddJob<UpdateListOfNewsFromTheFeedJob>(jobKey)
                     .AddTrigger(
                         trigger =>
                             trigger.ForJob(jobKey)
                                .WithSimpleSchedule(
                                     schedule =>
                                         schedule.WithIntervalInHours(3)
                                             .RepeatForever()));
            
                 configure.UseMicrosoftDependencyInjectionJobFactory();
             });
            services.AddQuartzHostedService();
            return services;
        }
    }
}
