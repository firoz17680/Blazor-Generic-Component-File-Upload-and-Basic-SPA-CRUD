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
    public class DepartmentService: IGenericService<Department>
    {
        ApplicationDbContext context;


        public DepartmentService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<ActionResult<bool>> CheckDuplicate(string Name)
        {
            int count = await context.Departments.Where(x => x.Name.ToUpper() == Name.ToUpper()).CountAsync();
            return count > 0 ? true : false;
        }

        public async Task<ActionResult<Department>> DeleteData(Guid Id)
        {
            Department Department = await context.Departments.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (Department == null)
            {
                return null;
            }

            context.Departments.Remove(Department);
            await context.SaveChangesAsync();


            return Department;

        }

        public async Task<ActionResult<Department>> EditData(Department model)
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();


            return model;
        }

        public async Task<ActionResult<List<Department>>> GetAll()
        {
            return await context.Departments
                            .OrderBy(x => x.Name)
                            .ToListAsync();

        }

        public async Task<ActionResult<Department>> GetById(Guid Id)
        {
            return await context.Departments
                              .Where(d => d.Id == Id).FirstOrDefaultAsync();

        }

        public async Task<ActionResult<Department>> SaveData(Department model)
        {
            try
            {
                context.Departments.Add(model);
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
