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
        public bool BuildingExistsByCode(string code)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Any(b => b.Code == code);
            }
        }

        public bool BuildingExistsById(long id)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Any(b => b.Id == id);
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
                List<Building> buildings = buildingDbContext.Buildings.Include(x => x.Documents).ToList();
                return buildings;
            }
        }



        public List<Building> GetBuildingByCity(string city)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.City == city)
                    //.Include(x => x.CreatedByUserId)
                    .Include(x => x.Documents).ToList();

                return buildings;
            }
        }

        public List<Building> GetBuildingByDistrict(string district)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.District == district)
                   //.Include(x => x.CreatedByUser)
                   .Include(x => x.Documents).ToList();

                return buildings;
            }
        }

        public List<Building> GetBuildingByNeighbourhood(string neighbourhood)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.Neighbourhood == neighbourhood)
                   //.Include(x => x.CreatedByUser)
                   .Include(x => x.Documents).ToList();

                return buildings;
            }
        }

        public List<Building> GetBuildingByStreet(string street)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.Street == street)
                   //.Include(x => x.CreatedByUser)
                   .Include(x => x.Documents).ToList();

                return buildings;
            }
        }


        public Building GetBuildingByCode(string code)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                Building building = buildingDbContext.Buildings
                   //.Include(x => x.CreatedByUser)
                   .Include(x => x.Documents).First(s => s.Code == code);

                return building;
            }
        }

        public Building GetBuildingById(long id)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {

                Building building = buildingDbContext.Buildings
                  //.Include(x => x.CreatedByUser)
                  .Include(x => x.Documents).First(s => s.Id == id);

                return building;
            }
        }

        public List<Building> GetBuildingsByUserId(long userId)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.CreatedByUserId == userId)
                   //.Include(x => x.CreatedByUser)
                   .Include(x => x.Documents).ToList();

                return buildings;
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

        public List<int> GetBuildingCounts()
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                var cityCount = buildingDbContext.Buildings
                    .Where(b => b.IsActive == true)
                    .GroupBy(b => b.City)
                    .Select(g => g.Key)
                    .Distinct()
                    .Count();

                var districtCount = buildingDbContext.Buildings
                    .Where(b => b.IsActive == true)
                    .GroupBy(b => b.District)
                    .Select(g => g.Key)
                    .Distinct()
                    .Count();

                var neighbourhoodCount = buildingDbContext.Buildings
                    .Where(b => b.IsActive == true)
                    .GroupBy(b => b.Neighbourhood)
                    .Select(g => g.Key)
                    .Distinct()
                    .Count();

                var buildingCount = buildingDbContext.Buildings
                    .Where(b => b.IsActive == true)
                    .Select(b => b.Id)
                    .Distinct()
                    .Count();

                return new List<int> { cityCount, districtCount, neighbourhoodCount, buildingCount };

            }

        }

        public List<string> GetStreetsByCityDistrictNeighbourhood(string city, string district, string neighbourhood)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<string> streets = buildingDbContext.Buildings.Where(b => b.City == city && b.District == district && b.Neighbourhood == neighbourhood)
                    .Select(b => b.Street)
                    .Distinct()
                    .ToList();
                return streets;
            }

        }

        public List<Building> GetBuildingsByCityDistrictNeighbourhoodStreet(string city, string district, string neighbourhood,string street) 
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Building> buildings = buildingDbContext.Buildings.Where(b => b.City == city && b.District == district && b.Neighbourhood == neighbourhood && b.Street == street)
                    .Include(x => x.Documents).ToList();
                //burada include'lar eklenip çıkarılabilir isteğe göre
                return buildings;
            }

        }
    }
}
