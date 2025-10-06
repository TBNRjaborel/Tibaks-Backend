using Tibaks_Backend.Models;

namespace Tibaks_Backend.Data.Seeders
{
    public static class HealthcareWorkerSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.HealthcareWorkers.Any())
            {
                var workers = new List<HealthcareWorker>
                {
                    new HealthcareWorker { Name = "Dr. Charmaine Jane Gelilang", Position = "Medical Officer" },
                    new HealthcareWorker { Name = "Venus Zephyr C. Monsanto", Position = "Nurse" },

                    new HealthcareWorker { Name = "Jovelyn S. Ponce", Position = "Midwife" },
                    new HealthcareWorker { Name = "Jennifer A. Albarando", Position = "Midwife" },
                    new HealthcareWorker { Name = "Cielo Anor", Position = "Midwife" },
                    new HealthcareWorker { Name = "Roxanne Grace Secuya", Position = "Midwife" },

                    new HealthcareWorker { Name = "Guadalupe Martinez", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Paz Bacalan", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Jean Cabornay", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Amelita Labra", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Zenda Dela Pena", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Emily Alfafara", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Corazon Labador", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Florabel Tagbar", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Mercedita Pagao", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Riza Aroa", Position = "Barangay Health Worker" },
                    new HealthcareWorker { Name = "Riza Martinez", Position = "Barangay Health Worker" }
                };

                context.HealthcareWorkers.AddRange(workers);
                await context.SaveChangesAsync();
            }
        }
    }
}
