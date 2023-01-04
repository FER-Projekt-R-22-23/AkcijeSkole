using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary;
using System;
using System.Data;
using AkcijeSkole.Domain.Models;
using DTOs = AkcijeSkoleWebApi.DTOs;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZahtjeviController : ControllerBase
    {
        private readonly IZahtjeviRepository _context;

        public ZahtjeviController(IZahtjeviRepository context)
        {
            _context = context;
        }

        // GET: api/Zahtjevi
        [HttpGet]
        public ActionResult<IEnumerable<DTOs.Zahtjev>> GetZahtjevi()
        {
            var zahtjeviResults = _context.GetAll()
                .Map(z => z.Select(DtoMapping.ToDto));

            return zahtjeviResults
                ? Ok(zahtjeviResults.Data)
                : Problem(zahtjeviResults.Message, statusCode: 500);
        }

        // GET: api/Zahtjevi/5
        [HttpGet("{id}")]
        public ActionResult<DTOs.ZahtjevDetails> GetZahtjevi(int id)
        {
            var zahtjevResult = _context.GetZahtjevDetails(id).Map(DtoMapping.ToDto);

            return zahtjevResult switch
            {
                { IsSuccess: true } => Ok(zahtjevResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(zahtjevResult.Message, statusCode: 500)
            };
        }
    }
}
