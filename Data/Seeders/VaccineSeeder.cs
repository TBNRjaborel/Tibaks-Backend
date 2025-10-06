using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Data.Seeders
{
    public static class VaccineSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Vaccines.Any())
            {
                var vaccines = new List<Vaccine>
                {
                    new Vaccine
                    {
                        Id = 1,
                        Name = "BCG",
                        RecommendedDosage = "0.05 mL",
                        Route = "Intradermal",
                        SiteOfAdministration = "Right upper arm",
                        Description = "The BCG vaccine, given at birth, protects children from tuberculosis, which most often affects the lungs but in infants and young children can also spread to the brain, bones, joints, and other organs."
                    },
                    new Vaccine
                    {
                        Id = 2,
                        Name = "Hepatitis B",
                        RecommendedDosage = "0.5 mL",
                        Route = "Intramuscular",
                        SiteOfAdministration = "Left thigh",
                        Description = "The Hepatitis B vaccine, given at birth, protects against hepatitis B virus infection, a dangerous liver disease that in infants often goes unnoticed for decades but can progress into cirrhosis or liver cancer later in life."
                    },
                    new Vaccine
                    {
                        Id = 3,
                        Name = "Pentavalent",
                        RecommendedDosage = "0.5 mL",
                        Route = "Intramuscular",
                        SiteOfAdministration = "Left thigh",
                        Description = "The Pentavalent vaccine, given at 6, 10, and 14 weeks, protects against diphtheria, pertussis, tetanus, Haemophilus influenzae type b, and hepatitis B; diphtheria can block the airway and cause heart or kidney failure, pertussis causes severe coughing fits and pneumonia, tetanus leads to painful and often fatal muscle spasms, Hib can cause life-threatening meningitis and pneumonia, and early hepatitis B infection in infants has a high chance of becoming chronic."
                    },
                    new Vaccine
                    {
                        Id = 4,
                        Name = "Oral Polio Vaccine (OPV)",
                        RecommendedDosage = "2 drops",
                        Route = "Oral",
                        SiteOfAdministration = "Mouth",
                        Description = "The Oral Polio Vaccine (OPV), given at 6, 10, and 14 weeks, protects against poliovirus; this infection can spread silently but in about 1 in 200 cases it causes paralysis, and in some cases it can be fatal when the muscles used for breathing are affected, making vaccination the only way to prevent its lifelong consequences."
                    },
                    new Vaccine
                    {
                        Id = 5,
                        Name = "Inactivated Polio Vaccine (IPV)",
                        RecommendedDosage = "0.5 mL",
                        Route = "Intramuscular",
                        SiteOfAdministration = "Right thigh",
                        Description = "The Inactivated Polio Vaccine (IPV), given at 14 weeks, also protects against poliovirus; unlike the oral form, it is injected and contains a killed virus that cannot cause polio, offering strong protection against paralysis and death from the disease in combination with OPV."
                    },
                    new Vaccine
                    {
                        Id = 6,
                        Name = "Pneumococcal Conjugate Vaccine (PCV)",
                        RecommendedDosage = "0.5 mL",
                        Route = "Intramuscular",
                        SiteOfAdministration = "Right thigh",
                        Description = "The Pneumococcal Conjugate Vaccine (PCV), given at 6, 10, and 14 weeks, protects against pneumococcal diseases such as pneumonia and meningitis, which remain among the leading causes of sickness and death in young children under 2 years old worldwide."
                    },
                    new Vaccine
                    {
                        Id = 7,
                        Name = "MMR",
                        RecommendedDosage = "0.5 mL",
                        Route = "Subcutaneous",
                        SiteOfAdministration = "Right upper arm",
                        Description = "The MMR vaccine, given at 9 months and again at 1 year, protects against measles, mumps, and rubella; measles can lead to pneumonia, blindness, or brain swelling, mumps can cause meningitis, deafness, or inflamed testicles, and rubella, though usually mild, can cause miscarriage, stillbirth, or severe birth defects if contracted during pregnancy."
                    }
                };

                context.Vaccines.AddRange(vaccines);
                await context.SaveChangesAsync();
            }
        }
    }
}
