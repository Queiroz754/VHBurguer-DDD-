using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using VHBurguer3.Applications.Sercives;
using VHBurguer3.Domains;
using VHBurguer3.DTOs.Usuario;
using VHBurguer3.Exeception;

namespace VHBurguer3.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet] //Lista Informações

        public ActionResult<List<LerUsuarioDto>> Listar()
        {
            List<LerUsuarioDto> usuarios =  _service.Listar();
            //Retorna a lista de usuários, a partir da DTO de Services
            return Ok(usuarios); //OK - 200 - DEU CERTO
        }

        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDto> ObterPorId(int id)
        {
            LerUsuarioDto usuario = _service.ObterPorId(id);
            if(usuario == null)
            {
                return NotFound();//NÂO ENCONTRADO 404
            }

            return Ok(usuario);
        }
        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDto> ObterPorEmail(string email)
        {
            try
            { 
            LerUsuarioDto usuario = _service.ObterPorEmail(email);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<LerUsuarioDto> Adicionar(CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioCriado = _service.Adicionar(usuarioDto);

                return StatusCode(201, usuarioCriado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDto> Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto lerUsuarioAtualizado = _service.Atualizar(id, usuarioDto);
                return StatusCode(200, lerUsuarioAtualizado);
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("{id}")]
        public ActionResult Remover(int id) 
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
