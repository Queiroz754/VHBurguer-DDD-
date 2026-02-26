using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHBurguer3.Applications.Services;
using VHBurguer3.DTOs.CategoriaDTO;
using VHBurguer3.Exeception;

namespace VHBurguer3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerCategoriaDto>> Listar()
        {
            List<LerCategoriaDto> categrias = _service.Listar();
            return Ok(categrias);
        }

        [HttpPost("{id}")]
        public ActionResult<LerCategoriaDto> ObterPorId(int id)
        {
            try
            {
            LerCategoriaDto categoria = _service.ObterPorId(id);
                return Ok(categoria);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarCategoriaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarCategoriaDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
