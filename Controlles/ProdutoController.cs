using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHBurguer3.Applications.Services;
using VHBurguer3.DTOs.ProdutoDTO;

namespace VHBurguer3.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerProdutoDto>> Listar()
        {
            List<LerProdutoDto> produtos = _service.Listar();
            //retur StatusCode(200,produto)
            return Ok(produtos);
        }

        [HttpGet("{id}")] 
        public ActionResult<LerProdutoDto> ObterPorId(int id)
        {
            LerProdutoDto produto = _service.ObterPorId(id);

            if(produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }
    }
}
