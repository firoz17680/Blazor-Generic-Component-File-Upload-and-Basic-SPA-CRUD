using Day3Training24032021.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Data
{
    public static class InitialService
    {
        public static async Task SeedDatabase(ApplicationDbContext context)
        {

            // Check if any migrations is pending or not ?
            if(context.Database.GetPendingMigrations().Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            // Seed Departments
            if(context.Departments.Count() == 0)
            {
                context.Departments.Add(new Department { Id= Guid.NewGuid(), Name="Administration", EditCount=0, CreationDate = DateTime.Now });
                context.Departments.Add(new Department { Id = Guid.NewGuid(), Name = "Accounts", EditCount = 0, CreationDate = DateTime.Now });
                context.Departments.Add(new Department { Id = Guid.NewGuid(), Name = "IT", EditCount = 0, CreationDate = DateTime.Now });
                context.Departments.Add(new Department { Id = Guid.NewGuid(), Name = "HR", EditCount = 0, CreationDate = DateTime.Now });
                await context.SaveChangesAsync();




            }

            // Seed Designation
            if (context.Designations.Count() == 0)
            {
                context.Designations.Add(new Designation { Id = Guid.NewGuid(), Name = "HR Manager", EditCount = 0, CreationDate = DateTime.Now });
                context.Designations.Add(new Designation { Id = Guid.NewGuid(), Name = "Accountant", EditCount = 0, CreationDate = DateTime.Now });
                context.Designations.Add(new Designation { Id = Guid.NewGuid(), Name = "Finance Manager", EditCount = 0, CreationDate = DateTime.Now });
                context.Designations.Add(new Designation { Id = Guid.NewGuid(), Name = "IT Manager", EditCount = 0, CreationDate = DateTime.Now });
                await context.SaveChangesAsync();
            }

            // Seed City
            if (context.Cities.Count() == 0)
            {
                context.Cities.Add(new City { Id = Guid.NewGuid(), Name = "New Delhi", EditCount = 0, CreationDate = DateTime.Now });
                await context.SaveChangesAsync();
            }

            // Seed Country
            if (context.Countries.Count() == 0)
            {
                context.Countries.Add(new Country { Id = Guid.NewGuid(), Name = "India", EditCount = 0, CreationDate = DateTime.Now });
                await context.SaveChangesAsync();
            }

            // Seed Employee
            if (context.Employees.Count() == 0)
            {
                context.Employees.Add(new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Employee1",
                    EditCount = 0,
                    CreationDate = DateTime.Now,
                    CityId = Guid.Parse("B084424F-F6EF-4652-B6C6-69EBC4A29B1D"),
                    DepartmentId = Guid.Parse("B002B9FC-D9F8-4B03-9DA8-8A9A3BCC46FF"),
                    DesignationId = Guid.Parse("CAF15AD7-61BF-4022-9FE5-BD667DB309A5"),
                    JoiningDate = DateTime.Now,
                    Salary = 50000
                });

                context.Employees.Add(new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Employee2",
                    EditCount = 0,
                    CreationDate = DateTime.Now,
                    CityId = Guid.Parse("B084424F-F6EF-4652-B6C6-69EBC4A29B1D"),
                    DepartmentId = Guid.Parse("6CA88D52-286D-4FD8-8FD8-B5603A0718B8"),
                    DesignationId = Guid.Parse("045C60A8-3A32-44FE-9C81-4FD6D5D7B479"),
                    JoiningDate = DateTime.Now,
                    Salary = 40000
                });

                context.Employees.Add(new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Employee3",
                    EditCount = 0,
                    CreationDate = DateTime.Now,
                    CityId = Guid.Parse("B084424F-F6EF-4652-B6C6-69EBC4A29B1D"),
                    DepartmentId = Guid.Parse("D431AA55-9D8B-4F1C-8F17-ADC9200E88C4"),
                    DesignationId = Guid.Parse("58B2A42D-46FA-4F7D-815F-E63603F31B19"),
                    JoiningDate = DateTime.Now,
                    Salary = 35000
                });


                await context.SaveChangesAsync();
            }

            //List<Department> lst = new List<Department>();
            //foreach(Department d in lst)
            //{
            //    context.Entry(d).State = EntityState.Added;
            //}
            //await context.SaveChangesAsync();




        }
    }
}
