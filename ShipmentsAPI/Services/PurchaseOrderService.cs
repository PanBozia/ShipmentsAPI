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
    public interface IPurchaseOrderService
    {
        List<PurchaseOrderDto> Get();
        PurchaseOrderDto GetById(Guid id);
        Guid Create(CreatePurchaseOrderDto dto);
        PurchaseOrderDto Update(Guid id, CreatePurchaseOrderDto dto);
        void Delete(Guid id);
        PageResult<PurchaseOrderDto> Search(QueryPurchaseOrders query);
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

        public PageResult<PurchaseOrderDto> Search(QueryPurchaseOrders query)
        {
            var orders = dbContext.PurchaseOrders
                .Include(x => x.Shipments)
                .Include(x => x.Incoterm)
                .Include(x => x.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
            {
                orders = orders.Where(x =>
                x.Customer.Name.Contains(query.SearchPhrase)
                || x.PONumber.Contains(query.SearchPhrase)
                || x.Customer.ShortName.Contains(query.SearchPhrase)
                || x.Customer.Name.Contains(query.SearchPhrase)
                || x.Customer.CityAddress.Contains(query.SearchPhrase));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<PurchaseOrder, object>>>()
                {
                    { nameof(PurchaseOrder.Category), t => t.Category },
                    { nameof(PurchaseOrder.DeliveryDate), t => t.DeliveryDate },
                    { nameof(PurchaseOrder.PONumber), t => t.PONumber },
                    { "CustomerShortName", t => t.Customer.ShortName },
                    { "Incoterms", t => t.Incoterm.ShortName },
                };

                var selectedColumn = columnsSelector[query.SortBy];

                orders = query.SortDirection == SortDirection.ASC ?
                    orders.OrderBy(selectedColumn)
                    : orders.OrderByDescending(selectedColumn);
            }
            var totalItemsCount = orders.Count();

            orders = orders
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .AsQueryable();

            var ordersDto = mapper.Map<List<PurchaseOrderDto>>(orders);

            var result = new PageResult<PurchaseOrderDto>(ordersDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
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
            var anyOtherOrderWithSamePoNumber = dbContext.PurchaseOrders.Any(x => x.PONumber == dto.PONumber && x.Id != id);
            if (anyOtherOrderWithSamePoNumber)
            {
                throw new BadRequestException($"Inne zamówienie z tym nr {dto.PONumber} już istnieje.");
            }
            purchaseOrder.PONumber = dto.PONumber;
            purchaseOrder.DeliveryDate = dto.DeliveryDate;
            purchaseOrder.CustomerId = dto.CustomerId;
            purchaseOrder.IncotermId = dto.IncotermId;
            purchaseOrder.Category = dto.Category;

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
