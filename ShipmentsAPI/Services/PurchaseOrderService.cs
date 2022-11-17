using AutoMapper;
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
        Guid Create(CreatePurchaseOrderDto dto);
        List<PurchaseOrderDto> Get();
        PurchaseOrderDto GetById(Guid id);
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
            var orders = dbContext.PurchaseOrders.ToList();
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


        private PurchaseOrder CheckIfOrderExist(Guid id)
        {
            var order = dbContext.PurchaseOrders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new NotFoundException($"Purchase order wit id: {id} not found");
            }
            return order;
        }

    }
}
