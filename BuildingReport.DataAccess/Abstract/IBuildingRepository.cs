using BuildingReport.Entities;
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
        Building GetBuildingByAdress(string adress);
        Building GetBuildingByCode(string code);
        Building GetBuildingById(long id);
        Building CreateBuilding(Building building);
        Building UpdateBuilding(Building building);
        void DeleteBuilding(long id);
        bool BuildingExists(string code, string adress);
    }
}
