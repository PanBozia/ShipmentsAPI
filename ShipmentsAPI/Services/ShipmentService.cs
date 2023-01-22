using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShipmentsAPI.Services
{
    public interface IShipmentService
    {
        void AddOrderToShipment(Guid shipmentId, Guid purchaseOrderId);
        Guid Create(CreateShipmentDto dto);
        void Delete(Guid id);
        List<ShipmentDto> Get();
        PageResult<ShipmentDto> Search(QueryShipments queryShipments);
        ShipmentDto GetById(Guid id);
        ShipmentBriefDto Update(Guid id, UpdateShipmentDto dto);
        void ChangeStatus(Guid shipmentId, int statusId);
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

        public PageResult<ShipmentDto> Search(QueryShipments queryShipments)
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

            if (queryShipments.TimeOfDeparture != nullDate)
            {
                shipments = shipments
                   .Where(d => d.TimeOfDeparture > queryShipments.TimeOfDeparture.AddDays(-2))
                   .Where(d => d.TimeOfDeparture < queryShipments.TimeOfDeparture.AddDays(2));
            }
            if (queryShipments.HasPriority)
            {
                shipments = shipments.Where(x => x.HasPriority == queryShipments.HasPriority);
            }
            if (queryShipments.WarehouseAreaId != 0)
            {
                shipments = shipments.Where(x => x.WarehouseArea.Id == queryShipments.WarehouseAreaId);
            }
            if (queryShipments.StatusId != 0)
            {
                shipments = shipments.Where(x => x.Status.Id == queryShipments.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(queryShipments.Comment))
            {
                shipments = shipments.Where(x => x.Comment.ToLower().Contains(queryShipments.Comment.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(queryShipments.ContainerNumber))
            {
                shipments = shipments.Where(x => x.ContainerNumber.ToLower().Contains(queryShipments.ContainerNumber.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(queryShipments.CarPlates))
            {
                shipments = shipments.Where(x => x.Forwarder.CarPlates.ToLower().Contains(queryShipments.CarPlates.ToLower()));
            }
            if (queryShipments.HasPriority)
            {
                shipments = shipments.Where( x => x.HasPriority == queryShipments.HasPriority);
            }
            if (!string.IsNullOrWhiteSpace(queryShipments.PurchaseOrderNumber))
            {
                shipments = shipments.Where(x => x.PurchaseOrders.Any(y => y.PONumber.Contains(queryShipments.PurchaseOrderNumber)));
            }
            if (queryShipments.PalletQty != 0) 
            { 
                shipments = shipments.Where(x => x.PalletQty == queryShipments.PalletQty || x.PalletQty == queryShipments.PalletQty + 1 || x.PalletQty == queryShipments.PalletQty-1);
            }

            shipments = shipments.OrderBy(x => x.TimeOfDeparture);
            
            if (!string.IsNullOrEmpty(queryShipments.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Shipment, object>>>()
                {
                    { nameof(Shipment.TimeOfDeparture), t => t.TimeOfDeparture },
                    { nameof(Shipment.ContainerType), t => t.ContainerType },
                    { nameof(Shipment.ContainerNumber), t => t.ContainerNumber },
                    { nameof(Shipment.ETD), t => t.ETD },
                    { nameof(Shipment.PalletQty), t => t.PalletQty },
                    { nameof(Shipment.Status.Name), t => t.Status.Name }
                    
                };

                var selectedColumn = columnsSelector[queryShipments.SortBy];

                shipments = queryShipments.SortDirection == SortDirection.ASC ? 
                    shipments.OrderBy(selectedColumn)
                    : shipments.OrderByDescending(selectedColumn);
            }

            var totalItemsCount = shipments.Count();

            shipments = shipments
                .Skip(queryShipments.PageSize * (queryShipments.PageNumber - 1))
                .Take(queryShipments.PageSize)
                .AsQueryable();

            var shipmentsDto = mapper.Map<List<ShipmentDto>>(shipments);

            var result = new PageResult<ShipmentDto>(shipmentsDto, totalItemsCount, queryShipments.PageSize, queryShipments.PageNumber);

            return result;
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

        public ShipmentBriefDto Update(Guid id, UpdateShipmentDto dto)
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
        public void ChangeStatus(Guid shipmentId, int statusId)
        {
            var shipment = dbContext.Shipments
               .Include(x => x.Forwarder)
               .Include(z => z.WarehouseArea)
               .Include(y => y.Status)
               .Include(s => s.PurchaseOrders)
               .ThenInclude(c => c.Customer)
               .FirstOrDefault(x => x.Id == shipmentId);
            
            if (shipment is null)
            {
                throw new NotFoundException($"Wysyłka z nr id: {shipmentId} nie została odnaleziona.");
            }

            var statusExists = dbContext.Statuses.Any(x => x.Id == statusId);
            if (!statusExists) 
            {
                throw new NotFoundException($"Status z nr id: {statusId} nie została odnaleziona.");
            }

            shipment.StatusId = statusId;
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();

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
