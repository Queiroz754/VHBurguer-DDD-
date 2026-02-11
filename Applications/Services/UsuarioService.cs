using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using VHBurguer3.Domains;
using VHBurguer3.DTOs;
using VHBurguer3.Exeception;
using VHBurguer3.Interfaces;

namespace VHBurguer3.Applications.Sercives
{
    // service concentra o "como fazer"
    public class UsuarioService
    {
        // _repository é o canal para acessar os dados
        private readonly IUsuarioRepository _repository;

        //injeção de dependencias 
        //implementamos o repositório e o service só depende da interface
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerUsuario = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true
            };

            return lerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();
                return usuariosDto;
        }

        private static void Validaremail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new DomainException("Email invalido.");
            }
        }

            private static byte[] HashSenha(string senha)
        {
             if(string.IsNullOrWhiteSpace(senha)) //garante que não está vazia
            {
                throw new DomainException("Senha é obrigatorio");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
            public LerUsuarioDto ObterPorId(int id)
            {
                Usuario usuario = _repository.ObterPorId(id);

                if(usuario == null)
                {
                throw new DomainException("Usuário não existe.");
                }

                return LerDto(usuario);
            }
        }
    }

