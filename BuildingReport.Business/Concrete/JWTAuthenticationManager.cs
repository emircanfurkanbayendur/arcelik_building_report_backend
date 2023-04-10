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

namespace BuildingReport.Business.Concrete
{
    public class JWTAuthenticationManager : IJWTAuthenticationService
    {        
        private readonly string _key;
        private IUserRepository _userRepository;
        private IHashService _hashService;

        public JWTAuthenticationManager(string key)
        {
            _key = key;
            _hashService = new HashManager();
            _userRepository = new UserRepository();
        }

        public string Authenticate(string email, string password)
        {
            byte[] _password = _hashService.HashPassword(password);
            if (!_userRepository.findUserExistsByEmailAndPassword(email, _password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
