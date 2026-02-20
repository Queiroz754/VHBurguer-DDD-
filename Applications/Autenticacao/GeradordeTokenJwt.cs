using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VHBurguer3.Domains;
using VHBurguer3.Exeception;

namespace VHBurguer3.Applications.Atenticacao
{
    public class GeradordeTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradordeTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            //KEY -> chave secreta usada para assinar o token
            //garante que o token nao foi alterado
            var chave = _config["Jwt:key"]!;

            //ISSUER -> quem gerou o token (nome da API/ sistema  que gerou)
            //a API valida se o token veio doemissor correto.
            var issuer = _config["Jwy:Issuer"]!;

            //AUDIENCE -> para quemo tokem foi criado 
            //define qual sistema pode usar o token
            var audience = _config["Jwt:Audience"]!;

            //Tempo de expiracao -> define quanto minutos o token sera valido 
            //depois disso,o usuario precisa logar novamnete.
            var expiraEmMinutos = int.Parse(_config["Jwt:ExpiraEmMinutos"]!);
           
            // Converter a chave para bytes (necessario para criar aassinatura)
            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if(keyBytes.Length < 32)
            {
                throw new DomainException("Jwt: Key precissa ter pelo menos 32 caracteres(256 bits).");
            }
            //Cria a chave de seguranca usada para assinar o token
            var securitykey = new SymmetricSecurityKey(keyBytes);

            var credentia = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            //Claims -> Informacoes do usuario que vao dentro do token 
            //essas informacoes podem ser recuperadas  na API para identificar quem esta logado
            var claims = new List<Claim>
            {
                // ID usuario (para saber quem fez a acao)
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),

                new Claim(ClaimTypes.Name, usuario.Nome),

                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,                                   //quem gerou token
                audience: audience,                               //quem pode usar o token
                claims: claims,                                   //dados do token
                expires: DateTime.Now.AddMinutes(expiraEmMinutos),//validade do token
                signingCredentials: credentia                     //assinatura de seguranca
                );          
            //Converter o token para string e essa string e  envida para o cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
