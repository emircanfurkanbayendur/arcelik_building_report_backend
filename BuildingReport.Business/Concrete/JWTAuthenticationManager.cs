using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class JWTAuthenticationManager : IJWTAuthenticationService
    {        
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        private readonly string _key;

        public JWTAuthenticationManager(string key)
        {
            _key = key;
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();

        }

        public string Authenticate(string email, byte[] password)
        {

            if (!_userRepository.GetAllUsers().Any(u => u.Email == email && u.Password.SequenceEqual(password)))
            {
                return null;
            }


            /*var roleID = _roleRepository.GetAllRoles().Where(r => r.Name == "admin").FirstOrDefault().Id;
            if(_userRepository.GetAllUsers().Where(u => u.Email == email && u.Password.SequenceEqual(password)).FirstOrDefault().RoleId != roleID)
            {
                return null;
            }*/

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
                    new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
