using VHBurguer3.Applications.Atenticacao;
using VHBurguer3.Domains;
using VHBurguer3.DTOs.AutentificacaoDTO;
using VHBurguer3.Exeception;
using VHBurguer3.Interfaces;

namespace VHBurguer3.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradordeTokenJwt _tokeJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradordeTokenJwt tokeJwt)
        {
            _repository = repository;
            _tokeJwt = tokeJwt;
        }

        // compara a hash SHA256

        private static bool VerificarSenha(string senhaDigitada, byte[]senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);

            if(usuario == null)
            {
                throw new DomainException("E-mail eu senha inválidos");
            }
            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
                {
                throw new DomainException("E-mail eu senha inválidos");
                }

            var token = _tokeJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { Token = token };

            return novoToken;
            
        }
    }
}
