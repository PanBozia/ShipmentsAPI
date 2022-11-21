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
        void Delete(Guid id);
        List<ShipmentDto> Get();
        List<ShipmentDto> Search(FilterShipmentsDto filterShipmentsDto);
        ShipmentDto GetById(Guid id);
        ShipmentBriefDto Update(Guid id, CreateShipmentDto dto);
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
                .AsQueryable();

            if (!shipments.Any())
            {
                throw new NotFoundException("Brak wysyłek - nie znaleziono");
            }
            var shipmentsDto = mapper.Map<List<ShipmentDto>>(shipments);

            return shipmentsDto;
        }

        public List<ShipmentDto> Search(FilterShipmentsDto filterShipmentsDto)
        {
            var nullDate = new DateTime();

            var shipments = dbContext.Shipments
                .Include(x => x.Forwarder)
                .Include(z => z.WarehouseArea)
                .Include(y => y.Status)
                .Include(s => s.PurchaseOrders)
                .ThenInclude(c => c.Customer)
                .Include(s => s.PurchaseOrders)
                .ThenInclude(c => c.Incoterm)
                .AsQueryable();

            if (filterShipmentsDto.TimeOfDeparture != nullDate)
            {
                shipments = shipments
                   .Where(d => d.TimeOfDeparture > filterShipmentsDto.TimeOfDeparture.AddDays(-2))
                   .Where(d => d.TimeOfDeparture < filterShipmentsDto.TimeOfDeparture.AddDays(2));
            }
            if (filterShipmentsDto.HasPriority)
            {
                shipments = shipments.Where(x => x.HasPriority == filterShipmentsDto.HasPriority);
            }
            if (filterShipmentsDto.WarehouseAreaId != 0)
            {
                shipments = shipments.Where(x => x.WarehouseArea.Id == filterShipmentsDto.WarehouseAreaId);
            }
            if (filterShipmentsDto.StatusId != 0)
            {
                shipments = shipments.Where(x => x.Status.Id == filterShipmentsDto.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(filterShipmentsDto.Comment))
            {
                shipments = shipments.Where(x => x.Comment.ToLower().Contains(filterShipmentsDto.Comment.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filterShipmentsDto.ContainerNumber))
            {
                shipments = shipments.Where(x => x.ContainerNumber.ToLower().Contains(filterShipmentsDto.Comment.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filterShipmentsDto.CarPlates))
            {
                shipments = shipments.Where(x => x.Forwarder.CarPlates.ToLower().Contains(filterShipmentsDto.CarPlates.ToLower()));
            }
            if (filterShipmentsDto.HasPriority)
            {
                shipments = shipments.Where( x => x.HasPriority == filterShipmentsDto.HasPriority);
            }
            if (!string.IsNullOrWhiteSpace(filterShipmentsDto.PurchaseOrderNumber))
            {
                shipments = shipments.Where(x => x.PurchaseOrders.Any(y => y.PONumber.Contains(filterShipmentsDto.PurchaseOrderNumber)));
            }


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

        public ShipmentBriefDto Update(Guid id, CreateShipmentDto dto)
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

            var updatedShipmentDto = mapper.Map<ShipmentBriefDto>(shipment);

            return updatedShipmentDto;
        }

        public void Delete(Guid id)
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

            dbContext.Shipments.Remove(shipment);
            dbContext.SaveChanges();
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
