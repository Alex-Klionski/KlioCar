using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace KlioCarProject.Models {

    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            //context.Database.EnsureCreated();
            context.Database.Migrate();
            if (!context.Cars.Any()) {
                context.Cars.AddRange(
                    new Car
                    {
                        Model = "Tesla",
                        Price = 54,
                        Category = "CClass"

                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 52,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 1525,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 123,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 514,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 544,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 552,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 5451,
                        Category = "CClass"
                    },
                    new Car
                    {
                        Model = "Tesla",
                        Price = 5414,
                        Category = "CClass"
                    }
                ) ;
                context.SaveChanges();
            }
        }
    }
}
