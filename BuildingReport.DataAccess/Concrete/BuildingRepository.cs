using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Concrete
{
    public class BuildingRepository : IBuildingRepository
    {
        public bool BuildingExists(string code)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Any(b => b.Code == code);
            }
        }

        public Building CreateBuilding(Building building)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                buildingDbContext.Buildings.Add(building);
                buildingDbContext.SaveChanges();
                return GetBuildingById(building.Id);
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
                List<Building> buildings = buildingDbContext.Buildings.Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                //return buildingDbContext.Buildings.ToList();

                return buildings;
            }
        }



        public List<Building> GetBuildingByCity(string city)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.City == city)
                    .Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                return buildings;
            }
        }

        public List<Building> GetBuildingByDistrict(string district)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.District == district)
                   .Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                return buildings;

                //return buildingDbContext.Buildings.Where(b => b.District == district).ToList();
            }
        }

        public List<Building> GetBuildingByNeighbourhood(string neighbourhood)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.Neighbourhood == neighbourhood)
                   .Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                return buildings;

                //return buildingDbContext.Buildings.Where(b => b.Neighbourhood == neighbourhood).ToList();
            }
        }

        public List<Building> GetBuildingByStreet(string street)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.Street == street)
                   .Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                return buildings;

                //return buildingDbContext.Buildings.Where(b => b.Street == street).ToList();
            }
        }


        public Building GetBuildingByCode(string code)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                Building building = buildingDbContext.Buildings
                   .Include(x => x.CreatedByUser).Include(x => x.Documents).First(s => s.Code == code);

                return building;
                //return buildingDbContext.Buildings.Where(b => b.Code == code).FirstOrDefault();
            }
        }

        public Building GetBuildingById(long id)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {

                Building building = buildingDbContext.Buildings
                  .Include(x => x.CreatedByUser).Include(x => x.Documents).First(s => s.Id == id);

                return building;

                //return buildingDbContext.Buildings.Find(id);
            }
        }

        public List<Building> GetBuildingsByUserId(long userId)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.CreatedByUserId == userId)
                   .Include(x => x.CreatedByUser).Include(x => x.Documents).ToList();

                return buildings;

                //return buildingDbContext.Buildings.Where(b => b.CreatedByUserId == userId).ToList();
            }
        }

        public Building UpdateBuilding(Building building)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                buildingDbContext.Buildings.Update(building);
                buildingDbContext.SaveChanges();
                return GetBuildingById(building.Id);
            }
        }
    }
}
