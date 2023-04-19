using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AutenticacaoAPI.Entities
{
    public class Login
    {
        public string email { get; set; }     
        public string senha { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }


}
