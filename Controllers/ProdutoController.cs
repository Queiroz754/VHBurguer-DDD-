using Microsoft.AspNetCore.Authorization;
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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        // autenticacao do usuario

        private int ObterUsuarioIdLogado()
        {
            //busca no token/claims o valor armazenado como id do usuario
            //Claimtypes.NameIdentifier geralmente guarda o D do usuario no jwt
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado");
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
            try
            {
                LerProdutoDto produto = _service.ObterPorId(id);
                return Ok(produto);
            }
            
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
            
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

        [HttpPost]
        // Indica que recebe dados no formato multipart/from-data
        [Consumes("multipart/form-data")]
        [Authorize] // exige login para adicionar produtos

        //[FromForm]-> dz que os dados vem do formulário da requisicao (multipart/form-data)
        public ActionResult Adicionar([FromForm] CriarProdutoDto produtoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();

                // o cadastro fica associado ao usuário logado
                _service.Adicionar(produtoDto, usuarioId);

                return StatusCode(201); // Created
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Consumes("multipart/from-data")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] AtualizarProdutoDto produtoDto)
        {
            try
            {
                _service.Atualizar(id, produtoDto);
                return NoContent();
            }
            catch(DomainException ex)
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
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
