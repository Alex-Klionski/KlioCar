using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace KlioCarProject.Models {

    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
           // context.Database.Migrate();
            if (!context.Cars.Any()) {
                context.Cars.AddRange(
                    new Car
                    {
                        Model = "Citroen C1",
                        Price = 250,
                        Category = "AClass",
                        Type = "hatchback",
                        Engine = "1.0",
                        ImageName = "citroen.jpg"                    
                    },
                    new Car
                    {
                        Model = "Renault Logan",
                        Price = 290,
                        Category = "BClass",
                        Type = "hatchback",
                        Engine = "2.2",
                        ImageName = "logan.jpg"
                    },
                    new Car
                    {
                        Model = "Skoda Octavia",
                        Price = 330,
                        Category = "CClass",
                        Type = "sedan",
                        Engine = "2.3",
                        ImageName = "octavia.jpg"

                    },
                    new Car
                    {
                        Model = "Skoda Octavia",
                        Price = 330,
                        Category = "CClass",
                        Type = "sedan",
                        Engine = "2.3",
                        ImageName = "octavia.jpg"

                    },
                    new Car
                    {
                        Model = "Audi A4",
                        Price = 514,
                        Category = "DClass",
                        Type = "sedan",
                        Engine = "2.5",
                        ImageName = "a4.jpg"
                    },
                    new Car
                    {
                        Model = "Honda Legend",
                        Price = 544,
                        Category = "EClass",
                        Type = "universal",
                        Engine = "2.5",
                        ImageName = "legend.jpg"
                    },
                    new Car
                    {
                        Model = "Rolls Royce Phantom",
                        Price = 830,
                        Category = "FClass",
                        Type = "universal",
                        Engine = "3.4",
                        ImageName = "royce.jpg"
                    },
                    new Car
                    {
                        Model = "Volkswagen Touareg",
                        Price = 750,
                        Category = "JClass",
                        Type = "crossover",
                        Engine = "5.0",
                        ImageName = "touareg.jpg"
                    }
                ) ;
                context.SaveChanges();
            }
        }
    }
}
