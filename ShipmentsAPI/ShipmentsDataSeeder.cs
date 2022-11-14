using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI
{
    public class ShipmentsDataSeeder
    {
        private readonly ShipmentsDbContext dbContext;

        public ShipmentsDataSeeder(ShipmentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Statuses.Any())
                {
                    var statuses = GetStatuses();
                    dbContext.Statuses.AddRange(statuses);
                    dbContext.SaveChanges();
                }
                
            }
        }

        private IEnumerable<Status> GetStatuses()
        {
            var statuses = new List<Status>()
            {
                new Status()
                {
                    Name = "Nowa",
                    Description = "Wysyłka została utworzona"
                },
                new Status()
                {
                    Name = "W przygotowaniu",
                    Description = "Wysyłka jest w trakcie kompletacji"
                },
                new Status()
                {
                    Name = "Gotowa",
                    Description = "Kompletacja wysyłki zakończona"
                },
                new Status()
                {
                    Name = "Wstrzymana LP",
                    Description = "Wysyłka wstrzymana przez dział LP"
                },
                new Status()
                {
                    Name = "Wstrzymana QA",
                    Description = "Wysyłka wstrzymana przez dział QA"
                },
                 new Status()
                {
                    Name = "Wysłana",
                    Description = "Przesyłka została wysłana"
                },

            };
            return statuses;
        }
    }
}
