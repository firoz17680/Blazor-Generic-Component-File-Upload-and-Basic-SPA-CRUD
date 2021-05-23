using Day3Training24032021.Server.Services;
using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : GenericController<Employee>
    {
        public EmployeeController(IGenericService<Employee> genericService): base(genericService)
        {

        }
    }
}
