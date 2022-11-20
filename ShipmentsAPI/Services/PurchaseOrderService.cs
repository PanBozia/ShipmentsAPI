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
    public interface IPurchaseOrderService
    {
        List<PurchaseOrderDto> Get();
        PurchaseOrderDto GetById(Guid id);
        Guid Create(CreatePurchaseOrderDto dto);
        PurchaseOrderDto Update(Guid id, CreatePurchaseOrderDto dto);
        void Delete(Guid id);
    }

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IMapper mapper;
        private readonly ShipmentsDbContext dbContext;

        public PurchaseOrderService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<PurchaseOrderDto> Get()
        {
            var orders = dbContext
                .PurchaseOrders
                .Include(x => x.Incoterm)
                .Include(x => x.Customer)
                .Include(x => x.Shipments)
                .ToList();
            if (!orders.Any())
            {
                throw new NotFoundException("Purchase orders not found");
            }

            var ordersDtos = mapper.Map<List<PurchaseOrderDto>>(orders);
            return ordersDtos;
        }

        public PurchaseOrderDto GetById(Guid id)
        {
            var order = CheckIfOrderExist(id);
            var orderDto = mapper.Map<PurchaseOrderDto>(order);

            return orderDto;
        }

        public Guid Create(CreatePurchaseOrderDto dto)
        {
            var result = dbContext.PurchaseOrders.Any(x => x.PONumber == dto.PONumber);
            if (result)
            {
                throw new BadRequestException($"Zamówienie z nr {dto.PONumber} już istnieje.");
            }
            var newOrder = mapper.Map<PurchaseOrder>(dto);
            newOrder.Id = Guid.NewGuid();
            dbContext.PurchaseOrders.Add(newOrder);
            dbContext.SaveChanges();

            return newOrder.Id;
        }

        public PurchaseOrderDto Update(Guid id, CreatePurchaseOrderDto dto)
        {
            var purchaseOrder = CheckIfOrderExist(id);
            purchaseOrder.PONumber = dto.PONumber;
            purchaseOrder.DeliveryDate = dto.DeliveryDate;
            purchaseOrder.CustomerId = dto.CustomerId;
            purchaseOrder.IncotermId = dto.IncotermId;

            dbContext.PurchaseOrders.Update(purchaseOrder);
            dbContext.SaveChanges();

            var purchaseOrderDto = mapper.Map<PurchaseOrderDto>(purchaseOrder);

            return purchaseOrderDto;
        }

        public void Delete(Guid id)
        {
            var purchaseOrder = CheckIfOrderExist(id);
            dbContext.PurchaseOrders.Remove(purchaseOrder);
            dbContext.SaveChanges();
        }

        private PurchaseOrder CheckIfOrderExist(Guid id)
        {
            var order = dbContext
                .PurchaseOrders
                .Include(x => x.Incoterm)
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new NotFoundException($"Purchase order wit id: {id} not found");
            }
            return order;
        }

    }
}
