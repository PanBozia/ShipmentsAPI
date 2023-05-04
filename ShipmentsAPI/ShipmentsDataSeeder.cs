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

                if (!dbContext.Customers.Any())
                {
                    var customers = GetCustomers();
                    dbContext.Customers.AddRange(customers);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Incoterms.Any())
                {
                    var inco = GetIncoterms();
                    dbContext.Incoterms.AddRange(inco);
                    dbContext.SaveChanges();
                }
                if (!dbContext.WarehouseAreas.Any())
                {
                    var warehouseAreas = GetAreas();
                    dbContext.WarehouseAreas.AddRange(warehouseAreas);
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
                    Name = "Kompletacja",
                    Description = "Wysyłka jest w trakcie kompletacji"
                },
                new Status()
                {
                    Name = "Gotowa",
                    Description = "Kompletacja wysyłki zakończona"
                },
                 new Status()
                {
                    Name = "Wstrzymana",
                    Description = "Wysyłka została wstrzymana"
                },
                new Status()
                {
                    Name = "Anulowana",
                    Description = "Przesyłka została anulowana"
                },
                 new Status()
                {
                    Name = "Zrealizowana",
                    Description = "Przesyłka została wysłana"
                },

            };
            return statuses;
        }
        private IEnumerable<Customer> GetCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Daicel Safety Systems Europe Sp. z o.o.",
                    ShortName = "DSSE",
                    ClientNumber = "102030",
                    CityAddress = "Żarów",
                    StreetAddress = "Strefowa 8",
                    ZipCodeAddress = "58-130",
                },
                new Customer()
                {
                    Name = "Toyoda Gosei Czech",
                    ShortName = "TGCZ",
                    ClientNumber = "203431",
                    CityAddress = "Klasterec nad Ohri",
                    StreetAddress = "Prymuslova 145",
                    ZipCodeAddress = "068-130",
                }

            };
            return customers;
        }
        private IEnumerable<Incoterm> GetIncoterms()
        {
            var incoterms = new List<Incoterm>()
            {
                new Incoterm()
                {
                    Name = "Ex Works (and named place)",
                    ShortName = "EXW"
                },
                new Incoterm()
                {
                    Name = "Free Carrier (and named place)",
                    ShortName = "FCA"
                },
                  new Incoterm()
                {
                    Name = "Carriage Paid To (insert named place of destination)",
                    ShortName = "CPT"
                },
                    new Incoterm()
                {
                    Name = "Carriage and Insurance Paid To (insert named place of destination)",
                    ShortName = "CIP"
                },  
                new Incoterm()
                {
                    Name = "Delivered at Place (insert named place of destination)",
                    ShortName = "DAP"
                },
                new Incoterm()
                {
                    Name = "Delivered at Place Unloaded (insert named place of destination)",
                    ShortName = "DPU"
                },
                 new Incoterm()
                {
                    Name = "Delivered Duty Paid (insert named place of destination)",
                    ShortName = "DDP"
                },
                  new Incoterm()
                {
                    Name = "Free Alongside Ship (insert named port of shipment)",
                    ShortName = "FAS"
                },
                   new Incoterm()
                {
                    Name = "Free On Board (insert named port of shipment)",
                    ShortName = "FOB"
                },
                    new Incoterm()
                {
                    Name = "Cost and Freight (insert named port of destination)",
                    ShortName = "CFR"
                },
                     new Incoterm()
                {
                    Name = "Cost Insurance and Freight",
                    ShortName = "CIF"
                },
                     new Incoterm()
                {
                    Name = "Nieznane",
                    ShortName = "N/A"
                }

            };
            return incoterms;
        }
        private IEnumerable<WarehouseArea> GetAreas()
        {
            var areas = new List<WarehouseArea>()
            {
                new WarehouseArea()
                {
                    Name = "TPA-1"
                },
                new WarehouseArea()
                {
                    Name = "TPA-2"
                },
                new WarehouseArea()
                {
                   Name = "TPA-3"
                },
                new WarehouseArea()
                {
                    Name = "TPA-4"
                },
                new WarehouseArea()
                {
                   Name = "TPA-5"
                },
                new WarehouseArea()
                {
                   Name = "LOK-1"
                },
                new WarehouseArea()
                {
                   Name = "LOK-2"
                }

            };
            return areas;
        }
    }
}
