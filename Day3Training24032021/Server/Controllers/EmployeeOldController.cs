using Day3Training24032021.Server.Data;
using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeOldController : ControllerBase
    {
        ApplicationDbContext context;

        public EmployeeOldController(ApplicationDbContext _context)
        {
            context = _context;
        }

        // api/Employee
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return await context.Employees
                                .Include(x => x.City)
                                .Include(x => x.City.Country)
                                .Include(x => x.Department)
                                .Include(x => x.Designation)
                                .OrderBy(x => x.Name)
                                .ToListAsync();
        }

        // api/Employee/f3f34-3434f4-34323232-34343
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid Id)
        {
            //return await context.Employees
            //                    .Include(x => x.City)
            //                    .Include(x => x.City.Country)
            //                    .Include(x => x.Department)
            //                    .Include(x => x.Designation)
            //                    .Where(x => x.Id == Id)
            //                    .FirstOrDefaultAsync();

            return await context.Employees
                        .FromSqlInterpolated($"select * from employees where id = {Id}")
                        .FirstOrDefaultAsync();

            //return await context.Employees
            //.FromSqlRaw("select * from employees where id = {0}", Id)
            //.FirstOrDefaultAsync();


        }

        // api/Employee/Query?Id=34fded-asdw343-2343242343242-233
        [HttpGet("Query")]
        public async Task<ActionResult<Employee>> GetEmployeeByIdFromQueryParam([FromQuery] Guid Id)
        {
            return await context.Employees
                                .Include(x => x.City)
                                .Include(x => x.City.Country)
                                .Include(x => x.Department)
                                .Include(x => x.Designation)
                                .Where(x => x.Id == Id)
                                .FirstOrDefaultAsync();
        }

        // api/Employee/SalaryByDepartment
        [HttpGet("SalaryByDepartment")]
        public async Task<ActionResult<List<Report>>> GetSalaryByDepartment()
        {
            return await context.Employees.GroupBy(x => x.City.Name)
                                .Select(c=> new Report { 
                                     Name = c.Key,
                                     Salary =c.Sum(x=> (decimal)x.Salary)
                                }).ToListAsync();

        }



        [HttpPost]
        public async Task<ActionResult<Employee>> SaveData(Employee employee)
        {
            try
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();

                return employee;


            }
            catch(DbUpdateConcurrencyException e)
            {
                return BadRequest(new Employee { Id = Guid.Empty, Name = e.InnerException.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateData(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return employee;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteData(Guid Id)
        {
            Employee employee = await context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if(employee == null)
            {
                return NotFound();
            }

            //context.Employees.Remove(employee);
            context.Entry(employee).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return employee;

        }




    }
}