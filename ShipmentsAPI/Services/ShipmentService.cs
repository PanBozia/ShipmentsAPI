using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace ShipmentsAPI.Services
{
    public interface IShipmentService
    {
        void AddOrderToShipment(Guid shipmentId, Guid purchaseOrderId);
        Guid Create(CreateShipmentDto dto);
        void Delete(Guid id);
        List<ShipmentDto> GetScheduleShipments();
        PageResult<ShipmentDto> Search(QueryShipments queryShipments);
        ShipmentDto GetById(Guid id);
        ShipmentBriefDto Update(Guid id, UpdateShipmentDto dto);
        void ChangeStatus(Guid shipmentId, int statusId);
        void RemoveOrderFromShipment(Guid shipmentId, Guid purchaseOrderId);
        void ChangeForwarder(Guid shipmentId, Guid forwarderId);
        void ChangeDepartureDate(Guid shipmentId, string dateTime);
        void ChangeWarehouseLocation(Guid shipmentId, int areaId);
        void RemoveWarehouseLocation(Guid shipmentId);
        void AddOrdersToShipment(Guid shipmentId, Guid[] purchaseOrdersIds);
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

        public List<ShipmentDto> GetScheduleShipments()
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

            var excludedStatus1 = dbContext.Statuses
                .Where(x => x.Name == "Anulowana")
                .FirstOrDefault();
            var excludedStatus2 = dbContext.Statuses
                .Where(x => x.Name == "Zrealizowana")
                .FirstOrDefault();
            DateTime yesterday = DateTime.Today.AddDays(-1);

            var oldShipments = shipments
                    .Where(x => x.Status != excludedStatus1 && x.Status != excludedStatus2 && x.ETD <= yesterday);
            var newShipments = shipments
                    .Where(x => x.ETD > yesterday);
            shipments = oldShipments.Concat(newShipments).OrderBy(x => x.ETD);

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
                   .Where(d => d.ETD  > queryShipments.TimeOfDeparture.AddDays(-2))
                   .Where(d => d.ETD < queryShipments.TimeOfDeparture.AddDays(2));
            }
            if (queryShipments.ScheduleDate != nullDate)
            {
                if(queryShipments.DateOffset != 0)
                {
                    shipments = shipments
                        .Where(d => d.ETD > queryShipments.ScheduleDate.AddDays(queryShipments.DateOffset * (-1)));
                }
                else
                {
                    shipments = shipments
                       .Where(d => d.ETD > queryShipments.ScheduleDate.AddDays(-2));
                }
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

            shipments = shipments.OrderBy(x => x.ETD);
            
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
            var statusIdNew = dbContext.Statuses
                .AsNoTracking()
                .Where(x => x.Name == "Nowa")
                .FirstOrDefault();
            newShipment.StatusId = statusIdNew.Id;
            dbContext.Shipments.Add(newShipment);
                dbContext.SaveChanges();

            return newShipment.Id;
        }

        public ShipmentBriefDto Update(Guid id, UpdateShipmentDto dto)
        {
            var shipment = dbContext.Shipments
               .AsNoTracking()
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
            dto.ShipmentId = id;
            
            var updatedShipment = mapper.Map<Shipment>(dto);
            updatedShipment.CreatedByUser = shipment.CreatedByUser;
            dbContext.Shipments.Update(updatedShipment);
            //dbContext.Shipments.Update(updatedShipment).State = EntityState.Modified;
            
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
        public void ChangeWarehouseLocation(Guid shipmentId, int areaId)
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
            var areaExists = dbContext.WarehouseAreas.Any(x => x.Id == areaId);
            if (!areaExists)
            {
                throw new NotFoundException($"Lokacja z nr id: {areaId} nie została odnaleziona.");
            }
            shipment.WarehouseAreaId = areaId;
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();

        }
        public void RemoveWarehouseLocation(Guid shipmentId)
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
            
            shipment.WarehouseAreaId = null;
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();
        }
        public void ChangeForwarder(Guid shipmentId, Guid forwarderId)
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

            var forwarderExists = dbContext.Forwarders.Any(x => x.Id == forwarderId);
            if (!forwarderExists)
            {
                throw new NotFoundException($"Przewoźnik z nr id: {forwarderId} nie został odnaleziony.");
            }

            shipment.ForwarderId = forwarderId;
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();

        }
        public void ChangeDepartureDate(Guid shipmentId, string dateTime)
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
            if(dateTime is null || dateTime == "" || dateTime.ToUpper() == "NULL")
            {
                shipment.TimeOfDeparture = null;
            }
            else
            {
                try
                {
                    var parsedDate = DateTime.Parse(dateTime.ToString());
                    shipment.TimeOfDeparture = parsedDate;
                }
                catch (Exception)
                {

                    throw new BadRequestException($"Podana data jest błędna: {dateTime}");
                }

            }
            //if (dateTime.GetType() == typeof(DateTime))
            
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
        public void AddOrdersToShipment(Guid shipmentId, Guid[] purchaseOrdersIds)
        {
            var shipment = dbContext.Shipments
                .Include(p => p.PurchaseOrders)
                .FirstOrDefault(x => x.Id == shipmentId);
            if (shipment is null)
            {
                throw new NotFoundException($"Wysyła nie została znaleziona.");
            }
            
            using (var dbTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var id in purchaseOrdersIds)
                    {
                        var purchaseOrder = dbContext.PurchaseOrders
                            .Include(p => p.Shipments)
                            .FirstOrDefault(x => id == x.Id);
                        if (purchaseOrder is null)
                        {
                            throw new NotFoundException($"Zamówienie nie zostało znalezione.");
                        }
                        shipment.PurchaseOrders.Add(purchaseOrder);
                        
                    }
                    dbContext.Shipments.Update(shipment);
                    dbContext.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    throw new BadRequestException($"Coś poszło nie tak. Info: {ex.Message}.");
                }
            }
        }
        public void RemoveOrderFromShipment(Guid shipmentId, Guid purchaseOrderId)
        {
            var purchaseOrder = dbContext.PurchaseOrders
                .Include(p => p.Shipments)
                .FirstOrDefault(x => x.Id == purchaseOrderId);
            var shipment = dbContext.Shipments
                .Include(p => p.PurchaseOrders)
                .FirstOrDefault(x => x.Id == shipmentId);
            if (shipment is null || purchaseOrder is null)
            {
                throw new NotFoundException($"Wysyła lub Zamówienie nie zostały znalezione.");
            }
            shipment.PurchaseOrders.Remove(purchaseOrder);
            dbContext.Shipments.Update(shipment);
            dbContext.SaveChanges();
        }

    }
}
