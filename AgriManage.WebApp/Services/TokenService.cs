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
            // 1. Ayarları Oku
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            // 2. Pasaportun İçine Yazılacak Bilgiler (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kullaniciAdi),     // Adı ne?
                new Claim(ClaimTypes.Role, rol),              // Rolü ne? (Admin/Çiftçi)
                new Claim("UserId", userId),                  // ID'si ne?
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Benzersiz Kod
            };

            // 3. Şifreleme Anahtarı
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Token'ı Oluştur (Süresi 1 Saat)
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Kart 1 saat geçerli
                signingCredentials: credentials);

            // 5. String olarak geri döndür
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}