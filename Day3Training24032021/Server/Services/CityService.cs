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
    public class CityService: IGenericService<City>
    {
        ApplicationDbContext context;


        public CityService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<ActionResult<bool>> CheckDuplicate(string Name)
        {
            int count = await context.Cities.Where(x => x.Name.ToUpper() == Name.ToUpper()).CountAsync();
            return count > 0 ? true : false;
        }

        public async Task<ActionResult<City>> DeleteData(Guid Id)
        {
            City City = await context.Cities.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (City == null)
            {
                return null;
            }

            context.Cities.Remove(City);
            await context.SaveChangesAsync();


            return City;

        }

        public async Task<ActionResult<City>> EditData(City model)
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();


            return model;
        }

        public async Task<ActionResult<List<City>>> GetAll()
        {
            return await context.Cities
                            .OrderBy(x => x.Name)
                            .ToListAsync();

        }

        public async Task<ActionResult<City>> GetById(Guid Id)
        {
            return await context.Cities
                              .Where(d => d.Id == Id).FirstOrDefaultAsync();

        }

        public async Task<ActionResult<City>> SaveData(City model)
        {
            try
            {
                context.Cities.Add(model);
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
