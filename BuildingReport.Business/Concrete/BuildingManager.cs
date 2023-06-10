using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.BuildingExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class BuildingManager : IBuildingService
    {
        private IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        private readonly IRoleAuthorityService _roleAuthorityService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheAuthorityService _cacheAuthorityService;

        public BuildingManager(IMapper mapper, IRoleAuthorityService roleAuthorityService,IUserService userService, IHttpContextAccessor httpContextAccessor,ICacheAuthorityService cacheAuthorityService)
        {
            _buildingRepository = new BuildingRepository();
            _mapper = mapper;
            _roleAuthorityService = roleAuthorityService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _cacheAuthorityService = cacheAuthorityService;

        }

        
        public BuildingResponse CreateBuilding(BuildingRequest buildingDTO)
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



            _ = buildingDTO ?? throw new ArgumentNullException(nameof(buildingDTO), " cannot be null");
            _userService.CheckIfUserExistsById(buildingDTO.CreatedByUserId);

            Building building = _mapper.Map<Building>(buildingDTO);
            building.RegisteredAt = DateTime.Now;
            building.IsActive = true;
            CheckIfBuildingExistsByCode(building.Code);
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.CreateBuilding(building));
            return response;
        }

        public bool DeleteBuilding(long id)
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

            CheckIfBuildingExistsById(id);
            _buildingRepository.DeleteBuilding(id);
            return true;
        }


        public List<BuildingResponse> GetAllBuildings()
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetAllBuildings());
            return response;
        }

        public List<BuildingResponse> GetBuildingByCity(string city) // string almak yerine dto içinde string alabiliriz böylece kontrolleri tek yerde yapabiliriz.
        {
            if (string.IsNullOrWhiteSpace(city)) 
                throw new ArgumentNullException(nameof(city), " cannot be null or empty.");

            if (city.Length > 50)
                throw new ArgumentException("City name must be 50 characters or less.", nameof(city));

            if (!Regex.IsMatch(city, "^[a-zA-Z0-9_ -]*$"))
                throw new ArgumentException("City name can only contain letters, numbers, underscores, spaces, and dashes.", nameof(city));

            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByCity(city));
            return response;
        }
        public List<BuildingResponse> GetBuildingByDistrict(string district)
        {
            if (string.IsNullOrWhiteSpace(district))
                                throw new ArgumentNullException(nameof(district), " cannot be null or empty.");

            if (district.Length > 50)
                throw new ArgumentException("District name must be 50 characters or less.", nameof(district));

            if (!Regex.IsMatch(district, "^[a-zA-Z0-9_ -]*$"))
                throw new ArgumentException("District name can only contain letters, numbers, underscores, spaces, and dashes.", nameof(district));


            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByDistrict(district));
            return response;
        }
        public List<BuildingResponse> GetBuildingByNeighbourhood(string neighbourhood)
        {
            if (string.IsNullOrWhiteSpace(neighbourhood))
                throw new ArgumentNullException(nameof(neighbourhood), " cannot be null or empty.");

            if (neighbourhood.Length > 70)
                throw new ArgumentException("Neighbourhood name must be 70 characters or less.", nameof(neighbourhood));

            if (!Regex.IsMatch(neighbourhood, "^[a-zA-Z0-9_ -]*$"))
                throw new ArgumentException("Neighbourhood name can only contain letters, numbers, underscores, spaces, and dashes.", nameof(neighbourhood));


            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByNeighbourhood(neighbourhood));
            return response;
        }

        public List<BuildingResponse> GetBuildingByStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street), " cannot be null or empty.");

            if (street.Length > 70)
                throw new ArgumentException("Street name must be 70 characters or less.", nameof(street));

            if (!Regex.IsMatch(street, "^[a-zA-Z0-9_ -]*$"))
                throw new ArgumentException("Street name can only contain letters, numbers, underscores, spaces, and dashes.", nameof(street));


            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByStreet(street));
            return response;
        }

        public BuildingResponse GetBuildingByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code), " cannot be null or empty.");

            if (code.Length > 50)
                throw new ArgumentException("Code must be 10 characters or less.", nameof(code));

            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.GetBuildingByCode(code));
            if (response == null)
            {
                throw new Exception("Building not found.");
            }
            return response;
        }

        public BuildingResponse GetBuildingById(long id)
        {
            ValidateId(id);

            CheckIfBuildingExistsById(id);
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.GetBuildingById(id));
            return response;
        }

        public List<BuildingResponse> GetBuildingsByUserId(long userId)
        {
            ValidateId(userId);
            _userService.CheckIfUserExistsById(userId);
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingsByUserId(userId));
            return response;
        }

        public BuildingResponse UpdateBuilding(UpdateBuildingRequest buildingDTO)
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



            _ = buildingDTO ?? throw new ArgumentNullException(nameof(buildingDTO), " cannot be null.");

            CheckIfBuildingExistsById(buildingDTO.Id);
            _userService.CheckIfUserExistsById(buildingDTO.CreatedByUserId);


            Building building = _mapper.Map<Building>(buildingDTO);
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.UpdateBuilding(building));
            return response;
        }

        public Building UpdateBuildingPatch(int id, JsonPatchDocument<PatchBuildingRequest> pathdoc)
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


            ValidateId(id);

            CheckIfBuildingExistsById(id);

            Building building = _buildingRepository.GetBuildingById(id);
            PatchBuildingRequest buildingDTO = _mapper.Map<PatchBuildingRequest>(building);

            
            pathdoc.ApplyTo(buildingDTO);

            ValidateId(buildingDTO.CreatedByUserId);

            _userService.CheckIfUserExistsById(buildingDTO.CreatedByUserId);

            building = _mapper.Map<Building>(buildingDTO);
            return _buildingRepository.UpdateBuilding(building);
        }

        public BuildingCountDTO GetBuildingCounts()
        {
            List<int> counts = _buildingRepository.GetBuildingCounts();
            BuildingCountDTO buildingCountDto = _mapper.Map<BuildingCountDTO>(counts);
            return buildingCountDto;

        }

        public BuildingStreetsDTO GetStreetsByCityDistrictNeighbourhood(string city, string district, string neighbourhood)
        {
            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(neighbourhood))
                throw new ArgumentNullException("One or more input values are null or whitespace.");

            if(city.Length > 50 || district.Length > 50 || neighbourhood.Length > 70)
                throw new ArgumentOutOfRangeException("One or more input values are out of range.");

            List<string> streets = _buildingRepository.GetStreetsByCityDistrictNeighbourhood(city,district, neighbourhood);
            BuildingStreetsDTO streetsDto = _mapper.Map<BuildingStreetsDTO>(streets);
            return streetsDto;

        }

        public BuildingListDTO GetBuildingsByCityDistrictNeighbourhoodStreet(string city,string district, string neighbourhood,string street) 
        {
            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(neighbourhood) || string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException("One or more input values are null or whitespace.");

            if (city.Length > 50 || district.Length > 50 || neighbourhood.Length > 70 || street.Length > 70)
                throw new ArgumentOutOfRangeException("One or more input values are out of range.");


            List<Building> buildings = _buildingRepository.GetBuildingsByCityDistrictNeighbourhoodStreet(city,district, neighbourhood, street);
            BuildingListDTO buildingsDto = _mapper.Map<BuildingListDTO>(buildings);
            return buildingsDto;
        }

        public List<BuildingNameBuildingNumberDTO> GetBuildingNameBuildingNumbers(string city,string district,string neighbourhood,string street)
        {
            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(neighbourhood) || string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException("One or more input values are null or whitespace.");

            if (city.Length > 50 || district.Length > 50 || neighbourhood.Length > 70 || street.Length > 70)
                throw new ArgumentOutOfRangeException("One or more input values are out of range.");


            List<Building> buildings = _buildingRepository.GetBuildingsByCityDistrictNeighbourhoodStreet(city, district, neighbourhood, street);
            List<BuildingNameBuildingNumberDTO> buildingsDto = _mapper.Map<List<BuildingNameBuildingNumberDTO>>(buildings);

            return buildingsDto;
        }

        //BusinessRules
        public void CheckIfBuildingExistsByCode(string code)
        {
            if (_buildingRepository.BuildingExistsByCode(code))
            {
                throw new BuildingAlreadyExists("Building already exists.");
            }
        }

        public void CheckIfBuildingExistsById(long id)
        {
            if (!_buildingRepository.BuildingExistsById(id))
            {
                throw new BuildingNotFoundException("Building cannot be found.");
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
