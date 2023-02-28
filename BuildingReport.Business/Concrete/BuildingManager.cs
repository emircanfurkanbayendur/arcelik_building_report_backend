using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class BuildingManager : IBuildingService
    {
        private IBuildingRepository _buildingRepository;

        public BuildingManager()
        {
            _buildingRepository = new BuildingRepository();
        }

        public bool BuildingExists(string code, string adress)
        {
            return _buildingRepository.BuildingExists(code, adress);
        }

        public Building CreateBuilding(Building building)
        {
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

        public Building GetBuildingByAdress(string adress)
        {
            return _buildingRepository.GetBuildingByAdress(adress);
        }

        public Building GetBuildingByCode(string code)
        {
            return _buildingRepository.GetBuildingByCode(code);
        }

        public Building GetBuildingById(long id)
        {
            return _buildingRepository.GetBuildingById(id);
        }

        public List<Building> GetBuildingsByUserId(long userId)
        {
            return _buildingRepository.GetBuildingsByUserId(userId);
        }

        public Building UpdateBuilding(Building building)
        {
            return _buildingRepository.UpdateBuilding(building);
        }
    }
}
