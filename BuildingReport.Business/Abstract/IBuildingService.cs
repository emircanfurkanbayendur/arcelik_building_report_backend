using BuildingReport.Entities;
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

        Building GetBuildingById(long id);

        Building CreateBuilding(Building building);

        Building UpdateBuilding(Building building);

        void DeleteBuilding(long id);

    }
}
