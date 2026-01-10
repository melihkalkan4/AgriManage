using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgriManage.WebApp.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenOlustur(string kullaniciAdi, string rol, string userId)
        {
            // Null check ekleyerek hataları önlüyoruz
            if (string.IsNullOrEmpty(kullaniciAdi)) kullaniciAdi = "Unknown";
            if (string.IsNullOrEmpty(rol)) rol = "User";
            if (string.IsNullOrEmpty(userId)) userId = "0";

            var tokenHandler = new JwtSecurityTokenHandler();
            // appsettings.json'dan okuyamazsa varsayılan bir key kullanır (Güvenlik için)
            var secretKey = _configuration["JwtSettings:SecretKey"] ?? "AgriManage_Projesi_Icin_Cok_Gizli_Anahtar_2025";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, kullaniciAdi),
                    new Claim(ClaimTypes.Role, rol),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}