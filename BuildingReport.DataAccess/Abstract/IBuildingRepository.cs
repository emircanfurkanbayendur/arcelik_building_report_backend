﻿using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Abstract
{
    public interface IBuildingRepository
    {
        List<Building> GetAllBuildings();
        List<Building> GetBuildingsByUserId(long userId);
        List<Building> GetBuildingByCity(string city);
        List<Building> GetBuildingByDistrict(string district);
        List<Building> GetBuildingByNeighbourhood(string neighbourhood);
        List<Building> GetBuildingByStreet(string street);
        List<Building> GetBuildingsByCityDistrictNeighbourhoodStreet(string city, string district, string neighbourhood, string street);
        List<int> GetBuildingCounts();
        List<string> GetStreetsByCityDistrictNeighbourhood(string city,string district,string neighbourhood);
        Building GetBuildingByCode(string code);
        Building GetBuildingById(long id);
        Building CreateBuilding(Building building);
        Building UpdateBuilding(Building building);
        void DeleteBuilding(long id);
        bool BuildingExistsByCode(string code);
        bool BuildingExistsById(long id);


    }
}
