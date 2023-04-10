using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
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
        List<BuildingResponse> GetAllBuildings();
        List<BuildingResponse> GetBuildingsByUserId(long userId);
        List<BuildingResponse> GetBuildingByCity(string city);
        List<BuildingResponse> GetBuildingByDistrict(string district);
        List<BuildingResponse> GetBuildingByNeighbourhood(string neighbourhood);
        List<BuildingResponse> GetBuildingByStreet(string street);
        BuildingListDTO GetBuildingsByCityDistrictNeighbourhoodStreet(string city, string district, string neighbourhood, string street);
        BuildingStreetsDTO GetStreetsByCityDistrictNeighbourhood(string city,string district,string neighbourhood);
        List<BuildingNameBuildingNumberDTO> GetBuildingNameBuildingNumbers(string city,string district,string neighbourhood,string street);
        BuildingCountDTO GetBuildingCounts();
        BuildingResponse GetBuildingByCode(string code);
        BuildingResponse GetBuildingById(long id);
        BuildingResponse CreateBuilding(BuildingRequest buildingDTO);
        BuildingResponse UpdateBuilding(UpdateBuildingRequest buildingDTO);
        Building UpdateBuildingPatch(int id, JsonPatchDocument<UpdateBuildingRequest> pathdoc);
        bool DeleteBuilding(long id);
        void CheckIfBuildingExistsByCode(string code);
        void CheckIfBuildingExistsById(long id);
    }
}
