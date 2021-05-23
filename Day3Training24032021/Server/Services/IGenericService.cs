using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Services
{
    public interface IGenericService<T>
    {
        Task<ActionResult<List<T>>> GetAll();


        Task<ActionResult<T>> GetById(Guid Id);

        Task<ActionResult<T>> SaveData(T model);
        Task<ActionResult<T>> EditData(T model);
        Task<ActionResult<T>> DeleteData(Guid Id);
        Task<ActionResult<bool>> CheckDuplicate(string Name);



    }
}
