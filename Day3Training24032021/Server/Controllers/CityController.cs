using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day3Training24032021.Server.Data;
using Day3Training24032021.Shared;
using Day3Training24032021.Server.Services;

namespace Day3Training24032021.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : GenericController<City>
    {
        public CityController(IGenericService<City> genericService): base(genericService)
        {

        }

    }
}
