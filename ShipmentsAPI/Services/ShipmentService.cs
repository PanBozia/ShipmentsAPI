using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI.Services
{
    public interface IShipmentService
    {
        void AddOrderToShipment(Guid shipmentId, Guid purchaseOrderId);
        Guid Create(CreateShipmentDto dto);
        List<ShipmentDto> Get();
        ShipmentDto GetById(Guid id);
        ShipmentDto Update(Guid id, CreateShipmentDto dto);
    }

    public class ShipmentService : IShipmentService
    {
        private readonly IMapper mapper;
        private readonly ShipmentsDbContext dbContext;

        public ShipmentService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<ShipmentDto> Get()
        {
            var shipments = dbContext.Shipments
                .Include(x => x.Forwarder)
                .Include(z => z.WarehouseArea)
                .Include(y => y.Status)
                .Include(s => s.PurchaseOrders)
                .ThenInclude(c => c.Customer)
                .Include(s => s.PurchaseOrders)
                .ThenInclude(c => c.Incoterm)
                .ToList();

            if (!shipments.Any())
            {
                throw new NotFoundException("Brak wysyłek - nie znaleziono");
            }

            var shipmentsDto = mapper.Map<List<ShipmentDto>>(shipments);

            return shipmentsDto;
        }
        public ShipmentDto GetById(Guid id)
        {
            var shipment = dbContext.Shipments
               .Include(x => x.Forwarder)
               .Include(z => z.WarehouseArea)
               .Include(y => y.Status)
               .Include(s => s.PurchaseOrders)
               .ThenInclude(c => c.Customer)
               .Include(s => s.PurchaseOrders)
               .ThenInclude(c => c.Incoterm)
               .FirstOrDefault(x => x.Id == id);

            if (shipment is null)
            {
                throw new NotFoundException($"Wysyłka z nr id: {id} nie została odnaleziona.");
            }

            var shipmentDto = mapper.Map<ShipmentDto>(shipment);
            return shipmentDto;
        }

        public Guid Create(CreateShipmentDto dto)
        {
            var newShipment = mapper.Map<Shipment>(dto);
            newShipment.Id = Guid.NewGuid();
            newShipment.StatusId = 1;
            dbContext.Shipments.Add(newShipment);
            dbContext.SaveChanges();

            return newShipment.Id;
        }

        public ShipmentDto Update(Guid id, CreateShipmentDto dto)
        {
            var shipment = dbContext.Shipments
               .Include(x => x.Forwarder)
               .Include(z => z.WarehouseArea)
               .Include(y => y.Status)
               .Include(s => s.PurchaseOrders)
               .ThenInclude(c => c.Customer)
               .FirstOrDefault(x => x.Id == id);

            if (shipment is null)
            {
                throw new NotFoundException($"Wysyłka z nr id: {id} nie została odnaleziona.");
            }
            var updatedShipment = mapper.Map<Shipment>(dto);
            dbContext.Shipments.Update(updatedShipment);
            dbContext.SaveChanges();

            var updatedShipmentDto = mapper.Map<ShipmentDto>(shipment);

            return updatedShipmentDto;
        }

        public void AddOrderToShipment(Guid shipmentId, Guid purchaseOrderId)
        {
            var purchaseOrder = dbContext.PurchaseOrders
                .Include(p => p.Shipments)
                .FirstOrDefault( x => x.Id == purchaseOrderId);
            var shipment = dbContext.Shipments
                .Include(p => p.PurchaseOrders)
                .FirstOrDefault(x => x.Id == shipmentId);
            if(shipment is null || purchaseOrder is null)
            {
                throw new NotFoundException($"Wysyła lub Zamówienie nie zostały znalezione.");
            }
            shipment.PurchaseOrders.Add(purchaseOrder);
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();
        }


    }
}
