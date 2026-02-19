using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VHBurguer3.Applications.Services;
using VHBurguer3.DTOs.ProdutoDTO;
using VHBurguer3.Exeception;

namespace VHBurguer3.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoControrller : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoControrller(ProdutoService service)
        {
            _service = service;
        }

        // autenticacao do usuario

        private int ObterUsuarioIdLogado()
        {
            //busca no token/claims o valor armazenado como id do usuario
            //Claimtypes.NameIdentifier geralmente guarda o D do usuario no jwt
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(idTexto))
            {
                throw new CannotUnloadAppDomainException("Ùsuario não autenticado");
            }

            //Converte o Id que veio como texto para inteiro 
            //Nosso UsuarioID no sistema está como int
            //asClaims (informacoes do usuario dentro do token) sempre sao armazenadas
            //como texto
            return int.Parse(idTexto);

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

        //GET -> api/produto/5/imagem
        [HttpGet("{id}/imagem")]
        public ActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);

                //retorna o arquivo para o navegador
                //"imagem/jpeg" informa o tipo da imagem (MIME type)
                //o navegador entende que deve renderizar como imagem
                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message); // NotFound -> não encontrado
            }
        }

    }
}
