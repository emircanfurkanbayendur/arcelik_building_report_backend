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
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Linq;

namespace BuildingReport.Business.Concrete
{

    public class AuthorityManager : IAuthorityService
    {
        private IAuthorityRepository _authorityRepository;
        private readonly IMapper _mapper;
        private int maxLength = 50;
        private readonly ICacheAuthorityService _cacheAuthorityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorityManager(IMapper mapper,ILoggerManager logger, ICacheAuthorityService cacheAuthorityService,IHttpContextAccessor httpContextAccessor)
        {
            _authorityRepository = new AuthorityRepository();
            _mapper = mapper;           
            _cacheAuthorityService = cacheAuthorityService;
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthorityResponse CreateAuthority(AuthorityRequest request )
        {

            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Create" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Create"))
            {
                throw new UnauthorizedAccessException();
            }

            _ = request ?? throw new ArgumentNullException(nameof(request), " cannot be null.");
            //if(name.Length > maxLength) { throw new ArgumentOutOfRangeException("Name length exceeds the limit:" + maxLength); }

            Authority authority = _mapper.Map<Authority>(request);
            checkIfAuthorityExistsByName(authority.Name);
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(_authorityRepository.CreateAuthority(authority));
            return response;
        }
        
        public void DeleteAuthority(long id)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Delete" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Delete"))
            {
                throw new UnauthorizedAccessException();
            }

            ValidateId(id);

            checkIfAuthorityExistsById(id);
            _authorityRepository.DeleteAuthority(id);
        }

        public List<AuthorityResponse> GetAllAuthorities()
        {
            List<AuthorityResponse> response = _mapper.Map<List<AuthorityResponse>>(_authorityRepository.GetAllAuthorities());
            return response;
        }

        public AuthorityResponse GetAuthorityById(long id)
        {

            ValidateId(id);
            checkIfAuthorityExistsById(id);
            Authority authority = _authorityRepository.GetAuthorityById(id);
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(authority);
            return response;
        }

        public AuthorityResponse UpdateAuthority(UpdateAuthorityRequest authorityDTO)
        {

            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Update" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Update"))
            {
                throw new UnauthorizedAccessException();
            }

            checkIfAuthorityExistsById(authorityDTO.Id);
            Authority authority = _mapper.Map<Authority>(authorityDTO);
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(_authorityRepository.UpdateAuthority(authority));
            return response;
        }

        //BusinessRules
        public void checkIfAuthorityExistsByName(string name)
        {
            if (_authorityRepository.AuthorityExistsByName(name))
            {
                throw new AuthorityAlreadyExistsException("Authority already exists.");
            }
        }

        public void checkIfAuthorityExistsById(long id)
        {
            if (!_authorityRepository.AuthorityExistsById(id))
            {
                throw new AuthorityNotFoundException("Authority cannot be found.");
            }
        }

        private void ValidateId(long id)
        {
            if (id <= 0 || id > long.MaxValue)
            {
                throw new IdOutOfRangeException(nameof(id), id);
            }
        }
    }
}
