using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IBuildingService
    {
        List<Building> GetAllBuildings();
        List<Building> GetBuildingsByUserId(long userId);
        List<Building> GetBuildingByCity(string city);
        List<Building> GetBuildingByDistrict(string district);
        List<Building> GetBuildingByNeighbourhood(string neighbourhood);
        List<Building> GetBuildingByStreet(string street);
        BuildingListDTO GetBuildingsByCityDistrictNeighbourhoodStreet(string city, string district, string neighbourhood, string street);
        BuildingStreetsDTO GetStreetsByCityDistrictNeighbourhood(string city,string district,string neighbourhood);
        BuildingCountDTO GetBuildingCounts();
        Building GetBuildingByCode(string code);
        Building GetBuildingById(long id);
        Building CreateBuilding(BuildingDTO buildingDTO);

        Building UpdateBuilding(BuildingDTO buildingDTO);
        Building UpdateBuildingPatch(int id, JsonPatchDocument<BuildingDTO> pathdoc);
        void DeleteBuilding(long id);
        void CheckIfBuildingExistsByCode(string code);
        void CheckIfBuildingExistsById(long id);
    }
}
