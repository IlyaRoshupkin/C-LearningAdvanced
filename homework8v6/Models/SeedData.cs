using homework8v6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework8v6.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new homework8v6Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<homework8v6Context>>()))
            {
                // Look for any movies.
                if (context.Employee.Any())
                {
                    return;   // DB has been seeded
                }

                context.Employee.AddRange(
                    new Employee
                    {
                        FullName = "Matvey Petrov",
                        Department = "Main",
                        Position = "Director"
                    },

                    new Employee
                    {
                        FullName = "Anna Belova",
                        Department = "Sales",
                        Position = "Sales Manager"
                    },

                    new Employee
                    {
                        FullName = "Semyon Leskov",
                        Department = "Supported",
                        Position = "Engineer"
                    },

                    new Employee
                    {
                        FullName = "Inna Somova",
                        Department = "Tested",
                        Position = "Main Tester"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
