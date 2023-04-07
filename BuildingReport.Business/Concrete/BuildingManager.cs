using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class BuildingManager : IBuildingService
    {
        private IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;

        public BuildingManager(IMapper mapper)
        {
            _buildingRepository = new BuildingRepository();
            _mapper = mapper;
        }

        
        public Building CreateBuilding(BuildingDTO buildingDTO)
        {
            Building building = _mapper.Map<Building>(buildingDTO);
            CheckIfBuildingExistsByCode(building.Code);
            return _buildingRepository.CreateBuilding(building);
        }

        public void DeleteBuilding(long id)
        {
            _buildingRepository.DeleteBuilding(id);
        }

        public List<Building> GetAllBuildings()
        {
            return _buildingRepository.GetAllBuildings();
        }

        public List<Building> GetBuildingByCity(string city)
        {
            return _buildingRepository.GetBuildingByCity(city);
        }
        public List<Building> GetBuildingByDistrict(string district)
        {
            return _buildingRepository.GetBuildingByDistrict(district);
        }
        public List<Building> GetBuildingByNeighbourhood(string neighbourhood)
        {
            return _buildingRepository.GetBuildingByNeighbourhood(neighbourhood);
        }

        public List<Building> GetBuildingByStreet(string street)
        {
            return _buildingRepository.GetBuildingByStreet(street);
        }

        public Building GetBuildingByCode(string code)
        {
            CheckIfBuildingExistsByCode(code);
            return _buildingRepository.GetBuildingByCode(code);
        }

        public Building GetBuildingById(long id)
        {
            CheckIfBuildingExistsById(id);
            return _buildingRepository.GetBuildingById(id);
        }

        public List<Building> GetBuildingsByUserId(long userId)
        {
            return _buildingRepository.GetBuildingsByUserId(userId);
        }

        public Building UpdateBuilding(BuildingDTO buildingDTO)
        {
            Building building = _mapper.Map<Building>(buildingDTO);
            return _buildingRepository.UpdateBuilding(building);
        }

        public Building UpdateBuildingPatch(int id, JsonPatchDocument<BuildingDTO> pathdoc)
        {
            Building building = _buildingRepository.GetBuildingById(id);
            if(building == null)
            {
                throw new Exception($"Building with ID {id} not found");
            }
            BuildingDTO buildingDTO = _mapper.Map<BuildingDTO>(building);

            
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
