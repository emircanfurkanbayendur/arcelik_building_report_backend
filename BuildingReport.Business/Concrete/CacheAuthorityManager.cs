using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.AuthorityExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using BuildingReport.Business.CustomExceptions.AuthorityExceptions;
using BuildingReport.Business.Logging.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingReport.Business.Concrete
{

    public class CacheAuthorityManager : ICacheAuthorityService
    {
        private readonly IJWTTokenService _jwtTokenService;

        public CacheAuthorityManager(IJWTTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }


        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();


        public List<RedisValue> CheckCacheAuthority(string token)
        {
            // headerdan çektiğimiz tokeni key olarak kullanıp, authorityleri redis cache'den çekiyoruz
            var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("CacheConnection"));
            var db = redis.GetDatabase();
            var authorityKey = "authority:" + token;
            var authorityValues = db.ListRange(authorityKey);

            //Cache'nin boş olup olmadığını kontrol ediyoruz , eğer boşsa authorityleri tokenden çözüp ekliyoruz
            if (authorityValues.IsNullOrEmpty())
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = _jwtTokenService.GetPrincipalFromToken(token);

                var authorities = principal.Claims
                    .Where(c => c.Type == "authority")
                    .Select(c => c.Value)
                    .ToList();

                authorityValues = authorities.Select(authority => (RedisValue)authority).ToArray();
                db.ListRightPush(authorityKey, authorityValues);

                var expiry = TimeSpan.FromHours(3);
                db.KeyExpire(authorityKey, expiry);

            }

            return authorityValues.ToList();

        }


    }
}
