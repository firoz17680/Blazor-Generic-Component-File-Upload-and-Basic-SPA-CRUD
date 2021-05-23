using Day3Training24032021.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class GenericController<T> : ControllerBase where T : class
    {
        IGenericService<T> genericService;

        public GenericController(IGenericService<T> _genericService)
        {
            genericService = _genericService;
        }

        [HttpGet]
        public async Task<ActionResult<List<T>>> GetAll()
        {
            return await genericService.GetAll();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetDepartmentById(Guid Id)
        {
            return await genericService.GetById(Id);

        }

        // https://myapp.com/api/Customer/Duplicate/Customer1
        [HttpGet("Duplicate/{name}")]
        public async Task<ActionResult<bool>> GetDuplicate(string name)
        {
            return await genericService.CheckDuplicate(name);

        }



        [HttpPost]
        public async Task<ActionResult<T>> SaveData(T model)
        {

            ActionResult<T> result = await genericService.SaveData(model);

            if (result == null)
            {
                return BadRequest();
            }

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> DeleteDepartment(Guid Id)
        {
            ActionResult<T> result = await genericService.DeleteData(Id);

            if (result == null)
            {
                return NotFound();
            }

            return result;


        }


        [HttpPut]
        public async Task<ActionResult<T>> EditDepartment(T model)
        {
            return await genericService.EditData(model);

        }
    }
}
