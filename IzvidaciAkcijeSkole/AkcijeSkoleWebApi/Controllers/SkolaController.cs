using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Commons;

namespace ExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkoleController : ControllerBase
    {
        private readonly ISkoleRepository<int, DbModels.Skole> _skolaRepository;

        public SkoleController(ISkoleRepository<int, DbModels.Skole> context)
        {
            _skolaRepository = context;
        }
         
        // GET: api/skole
        [HttpGet]
        public ActionResult<IEnumerable<Skola>> GetAllRoles()
        {
            return Ok(_skolaRepository.GetAll().Select(DtoMapping.ToDto));
        }

        
    }
}
