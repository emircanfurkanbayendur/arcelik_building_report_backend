﻿using BuildingReport.DataAccess.Abstract;
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
        public bool BuildingExists(string code, string adress)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Any(b => b.Code == code && b.Adress == adress);
            }
        }

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

        public Building GetBuildingByAdress(string adress)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Where(b => b.Adress == adress).FirstOrDefault();
            }
        }

        public Building GetBuildingByCode(string code)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Where(b => b.Code == code).FirstOrDefault();
            }
        }

        public Building GetBuildingById(long id)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Find(id);
            }
        }

        public List<Building> GetBuildingsByUserId(long userId)
        {
            using (var buildingDbContext = new ArcelikBuildingReportDbContext())
            {
                return buildingDbContext.Buildings.Where(b => b.CreatedByUserId == userId).ToList();
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
