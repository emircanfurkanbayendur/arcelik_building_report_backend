using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class BuildingManager : IBuildingService
    {
        private IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        private readonly IRoleAuthorityService _roleAuthorityService;

        public BuildingManager(IMapper mapper, IRoleAuthorityService roleAuthorityService)
        {
            _buildingRepository = new BuildingRepository();
            _mapper = mapper;
            _roleAuthorityService = roleAuthorityService;
        }

        
        public BuildingResponse CreateBuilding(BuildingRequest buildingDTO)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 2))
            {
                return null;
            }

            Building building = _mapper.Map<Building>(buildingDTO);
            building.RegisteredAt = DateTime.Now;
            building.IsActive = true;
            CheckIfBuildingExistsByCode(building.Code);
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.CreateBuilding(building));
            return response;
        }

        public bool DeleteBuilding(long id)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 3))
            {
                return false;
            }
            _buildingRepository.DeleteBuilding(id);
            return true;
        }

        public List<BuildingResponse> GetAllBuildings()
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetAllBuildings());
            return response;
        }

        public List<BuildingResponse> GetBuildingByCity(string city)
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByCity(city));
            return response;
        }
        public List<BuildingResponse> GetBuildingByDistrict(string district)
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByDistrict(district));
            return response;
        }
        public List<BuildingResponse> GetBuildingByNeighbourhood(string neighbourhood)
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByNeighbourhood(neighbourhood));
            return response;
        }

        public List<BuildingResponse> GetBuildingByStreet(string street)
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingByStreet(street));
            return response;
        }

        public BuildingResponse GetBuildingByCode(string code)
        {
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.GetBuildingByCode(code));
            return response;
        }

        public BuildingResponse GetBuildingById(long id)
        {
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.GetBuildingById(id));
            return response;
        }

        public List<BuildingResponse> GetBuildingsByUserId(long userId)
        {
            List<BuildingResponse> response = _mapper.Map<List<BuildingResponse>>(_buildingRepository.GetBuildingsByUserId(userId));
            return response;
        }

        public BuildingResponse UpdateBuilding(UpdateBuildingRequest buildingDTO)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }
            Building building = _mapper.Map<Building>(buildingDTO);
            BuildingResponse response = _mapper.Map<BuildingResponse>(_buildingRepository.UpdateBuilding(building));
            return response;
        }

        public Building UpdateBuildingPatch(int id, JsonPatchDocument<UpdateBuildingRequest> pathdoc)
        {
            Building building = _buildingRepository.GetBuildingById(id);
            if(building == null)
            {
                throw new Exception($"Building with ID {id} not found");
            }
            UpdateBuildingRequest buildingDTO = _mapper.Map<UpdateBuildingRequest>(building);

            
            pathdoc.ApplyTo(buildingDTO);

            building = _mapper.Map(buildingDTO, building);
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
            List<string> streets = _buildingRepository.GetStreetsByCityDistrictNeighbourhood(city,district, neighbourhood);
            BuildingStreetsDTO streetsDto = _mapper.Map<BuildingStreetsDTO>(streets);
            return streetsDto;

        }

        public BuildingListDTO GetBuildingsByCityDistrictNeighbourhoodStreet(string city,string district, string neighbourhood,string street) 
        {
            List<Building> buildings = _buildingRepository.GetBuildingsByCityDistrictNeighbourhoodStreet(city,district, neighbourhood, street);
            BuildingListDTO buildingsDto = _mapper.Map<BuildingListDTO>(buildings);
            return buildingsDto;
        }


        //BusinessRules
        public void CheckIfBuildingExistsByCode(string code)
        {
            if (_buildingRepository.BuildingExistsByCode(code))
            {
                throw new NotImplementedException("Building already exists.");
            }
        }

        public void CheckIfBuildingExistsById(long id)
        {
            if (!_buildingRepository.BuildingExistsById(id))
            {
                throw new NotImplementedException("Building cannot be found.");
            }
        }
    }
}
