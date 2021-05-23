using Day3Training24032021.Server.Data;
using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Services
{
    public class EmployeeService: IGenericService<Employee>
    {
        ApplicationDbContext context;


        public EmployeeService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<ActionResult<bool>> CheckDuplicate(string Name)
        {
            int count = await context.Employees.Where(x => x.Name.ToUpper() == Name.ToUpper()).CountAsync();
            return count > 0 ? true : false;
        }

        public async Task<ActionResult<Employee>> DeleteData(Guid Id)
        {
            Employee Employee = await context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (Employee == null)
            {
                return null;
            }

            context.Employees.Remove(Employee);
            await context.SaveChangesAsync();


            return Employee;

        }

        public async Task<ActionResult<Employee>> EditData(Employee model)
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();


            return model;
        }

        public async Task<ActionResult<List<Employee>>> GetAll()
        {
            return await context.Employees
                              .Include(x => x.City)
                                .Include(x => x.City.Country)
                                .Include(x => x.Department)
                                .Include(x => x.Designation)
                            .OrderBy(x => x.Name)
                            .ToListAsync();

        }

        public async Task<ActionResult<Employee>> GetById(Guid Id)
        {
            return await context.Employees
                  .Include(x => x.City)
                                .Include(x => x.City.Country)
                                .Include(x => x.Department)
                                .Include(x => x.Designation)
                              .Where(d => d.Id == Id).FirstOrDefaultAsync();

        }

        public async Task<ActionResult<Employee>> SaveData(Employee model)
        {
            try
            {
                context.Employees.Add(model);
                await context.SaveChangesAsync();

                return model;

            }
            catch
            {
                return null;
            }
        }

    }
}
