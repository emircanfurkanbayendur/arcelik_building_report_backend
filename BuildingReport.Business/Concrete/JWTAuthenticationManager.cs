using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
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
        private readonly string _key;

        private readonly IHashService _hash;
        private IUserService _userService;

        public JWTAuthenticationManager(string key)
        {
            _key = key;
            _hash = new HashManager();
            _userService = new UserManager();
        }

        public string Authenticate(string email, byte[] password)
        {
            if (!_userService.GetAllUsers().Any(u => u.Email == email && u.Password.SequenceEqual(password)))
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
                    new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ReturnDto Login (LoginDto loginDto)
        {
            byte[] _password = _hash.HashPassword(loginDto.Password);
            var token = Authenticate(loginDto.Email, _password);

            if (token == null)
                return null;

            var user = _userService.GetAllUsers().Where(u => u.Email == loginDto.Email && u.Password.SequenceEqual(_password)).FirstOrDefault();
            ReturnDto returnDto = new ReturnDto();
            returnDto.Token = token;
            returnDto.Id = user.Id;
            returnDto.RoleId = user.RoleId;
            returnDto.Email = user.Email;
            returnDto.FirstName = user.FirstName;
            returnDto.LastName = user.LastName;
            returnDto.Password = user.Password;
            returnDto.CreatedAt = user.CreatedAt;
            returnDto.IsActive = user.IsActive;
            
            return returnDto;

        }
    }
}
