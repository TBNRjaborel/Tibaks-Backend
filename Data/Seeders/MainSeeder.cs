using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Tibaks_Backend.Data.Seeders
{
    public static class MainSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Apply migrations first to make sure the DB is up to date
            await context.Database.MigrateAsync();

            // Call individual seeders
            await VaccineSeeder.SeedAsync(context);
            await HealthcareWorkerSeeder.SeedAsync(context);

            // You can add more:
            // await UserSeeder.SeedAsync(context);
            // await RoleSeeder.SeedAsync(context);
        }
    }
}
