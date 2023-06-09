using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using BuildingReport.Business.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BuildingReport.Business.Concrete
{
    public class JWTTokenManager : IJWTTokenService
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;

        public JWTTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;

            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }


        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception)
            {
                // Token validation failed
                return null;
            }
        }

    }
}
