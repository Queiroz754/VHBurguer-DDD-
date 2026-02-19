using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHBurguer3.Applications.Services;
using VHBurguer3.DTOs.AutentificacaoDTO;
using VHBurguer3.Exeception;

namespace VHBurguer3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutentificacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                var token = _service.Login(loginDto);

                return Ok(token);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
