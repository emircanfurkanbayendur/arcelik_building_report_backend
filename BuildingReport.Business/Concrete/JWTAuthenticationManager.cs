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
using BuildingReport.Entities;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BuildingReport.Business.Concrete
{
    public class JWTAuthenticationManager : IJWTAuthenticationService
    {        
        private readonly string _key;
        private IUserRepository _userRepository;
        private IHashService _hashService;
        private IAuthorityRepository _authorityRepository;

        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
        public JWTAuthenticationManager(string key)
        {
            _key = key;
            _hashService = new HashManager();
            _userRepository = new UserRepository();
            _authorityRepository = new AuthorityRepository();
        }

        public string Authenticate(string email, string password)
        {
            byte[] _password = _hashService.HashPassword(password);
            if (!_userRepository.findUserExistsByEmailAndPassword(email, _password))
            {
                return null;
            }

            List<Authority> authorities = _authorityRepository.GetAuthoritiesByEmail(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            foreach (var authority in authorities)
            {
                claims.Add(new Claim("authority", authority.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);



            // Redis'e tokeni kaydediyoruz
            var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("CacheConnection"));
            var db = redis.GetDatabase();
            var cacheTokenKey = "token:" + email;
            db.StringSet(cacheTokenKey, tokenString);


            //Redis'e authorityleri kaydediyoruz key -> token
            var authorityKey = "authority:" + tokenString;
            var authorityValues = authorities.Select(authority => (RedisValue)authority.Name).ToArray();
            db.ListRightPush(authorityKey, authorityValues);

            var expiry = TimeSpan.FromHours(3);
            db.KeyExpire(authorityKey, expiry);
            db.KeyExpire(cacheTokenKey, expiry);




            //var tk = db.StringGet(cacheTokenKey);
            //var at = db.ListRange(authorityKey);

            //Console.WriteLine("Token: " + tk);

            //foreach (var authorityValue in at)
            //{
            //    Console.WriteLine("Authority: " + authorityValue);
            //}




            return tokenHandler.WriteToken(token);
        }
    }
}
