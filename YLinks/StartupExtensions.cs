using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLinks.Data;

namespace YLinks
{
    public static class StartupExtensions
    {
        public static IWebHost InitializeData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    MigrateDatabase(services);
                    Task.Run(async () => await SeedUsers(services)).Wait();

                    return host;
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, ex.Message);
                    throw;
                }
            }
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        private static async Task SeedUsers(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var config = services.GetRequiredService<IConfiguration>();
            var userConfig = config.GetSection("DefaultUser");

            var user = new IdentityUser();
            userConfig.Bind(user);

            if (!userManager.Users.Any())
            {
                var result = await userManager.CreateAsync(user, userConfig.GetValue<string>("Password"));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Unable to create default user");
                }
            }
        }

        public static IWebHost MigrateDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<ApplicationDbContext>();
                    db.Database.Migrate();
                    return host;
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, ex.Message);
                    throw;
                }
            }
        }
    }
}
