using AutenticacaoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutenticacaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AutenticacaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public Token Login()
        {

            return GenerateToken(new Login
            {
                email = "jlbnlbnlj",
                senha = "kjnlçkçn"
            });
        }

        [HttpGet("teste")]
        [Authorize]
        public string ValidarAutenticacao()
        {
            return "Deu certo !!!";
        }


        private Token GenerateToken(Login userInfo)
        {
            //declarações do usuário
            var claims = new[]
            {
                new Claim("email", userInfo.email),
                new Claim("meuvalor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                //audiencia
                audience: _configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials
                );

            return new Token()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            };
        }
    }



}
