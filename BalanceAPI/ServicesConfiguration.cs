using DataAccess;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

using Application;
using DataAccess.Repositories;
using FluentValidation;
using BalanceAPI.Validators;
using Application.Models;

namespace BalanceAPI
{
    public static class ServicesConfiguration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWalletService, WalletService>();
            var connectionString = configuration.GetConnectionString("PsqlConnection");
            services.AddDbContext<DbAccess>(options => options.UseNpgsql(connectionString));

            services.AddTransient<IValidator<UserRegistrationRequest>, UserRegistrationValidator>();
        }
    }
}
