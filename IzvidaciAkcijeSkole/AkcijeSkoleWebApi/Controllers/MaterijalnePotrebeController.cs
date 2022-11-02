using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkoleWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AkcijeSkoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterijalnePotrebeController : ControllerBase
    {
            private readonly AkcijeSkoleDbContext _context;
            private readonly IMaterijalnaPotrebaRepository<int, MaterijalnePotrebe> _materijalnaPotrebaRepository;

            public MaterijalnePotrebeController(IMaterijalnaPotrebaRepository<int, MaterijalnePotrebe> materijalnaPotrebaRepository)
            {
                _materijalnaPotrebaRepository = materijalnaPotrebaRepository;
            }

            [HttpGet]
            public ActionResult<IEnumerable<DTOs.MaterijalnaPotreba>> GetAllMaterijalnePotrebe()
            {
                return Ok(_materijalnaPotrebaRepository.GetAll().Select(DtoMapping.ToDto));
            }


            [HttpGet("idMaterijalnaPotreba")]
            public ActionResult<DTOs.MaterijalnaPotreba> GetMaterijalnaPotreba(int idPotreba)
            {
                var potrebaOption = _materijalnaPotrebaRepository.Get(idPotreba).Map(DtoMapping.ToDto);

                return potrebaOption
                    ? Ok(potrebaOption.Data)
                    : NotFound();
            }

            [HttpPut]
            public IActionResult EditMaterijalnaPotreba(int idPotreba, DTOs.MaterijalnaPotreba potreba)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (idPotreba != potreba.IdMaterijalnaPotreba)
                {
                    return BadRequest();
                }

                if (!_materijalnaPotrebaRepository.Exists(idPotreba))
                {
                    return NotFound();
                }

                return _materijalnaPotrebaRepository.Update(potreba.ToDbModel())
                    ? AcceptedAtAction("EditMaterijalnaPotreba", potreba)
                    : StatusCode(500);
            }

            [HttpPost]
            public ActionResult<DTOs.MaterijalnaPotreba> CreateMaterijalnaPotreba(DTOs.MaterijalnaPotreba potreba)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return _materijalnaPotrebaRepository.Insert(potreba.ToDbModel())
                    ? CreatedAtAction("GetMaterijalnaPotreba", new { idPotreba = potreba.IdMaterijalnaPotreba }, potreba)
                    : StatusCode(500);
            }

            [HttpDelete("idMaterijalnaPotreba")]
            public IActionResult DeleteMaterijalnaPotreba(int idPotreba)
            {
                if (!_materijalnaPotrebaRepository.Exists(idPotreba))
                    return NotFound();

                return _materijalnaPotrebaRepository.Remove(idPotreba)
                    ? NoContent()
                    : StatusCode(500);
            }




        }
    

