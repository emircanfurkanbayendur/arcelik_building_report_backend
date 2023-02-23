using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Concrete
{
    public class BuildingRepository : IBuildingRepository
    {
        public Building CreateBuilding(Building building)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                buildingDbContext.Buildings.Add(building);
                buildingDbContext.SaveChanges();
                return building;
            }
        }

        public void DeleteBuilding(long id)
        {
            var building = new Building() { Id = id, IsActive = false };
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {

                buildingDbContext.Attach(building);
                buildingDbContext.Entry(building).Property(x => x.IsActive).IsModified = true;
                buildingDbContext.SaveChanges();



            }
        }

        public List<Building> GetAllBuildings()
        {
            using(var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.ToList();
            }
        }

        public Building GetBuildingById(long id)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Find(id);
            }
        }

        public Building UpdateBuilding(Building building)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                buildingDbContext.Buildings.Update(building);
                buildingDbContext.SaveChanges();
                return building;
            }
        }
    }
}
