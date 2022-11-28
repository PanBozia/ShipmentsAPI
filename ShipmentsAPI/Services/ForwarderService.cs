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
    public interface IForwarderService
    {
        Guid Create(CreateForwarderDto dto);
        void Delete(Guid id);
        List<ForwarderDto> Get();
        ForwarderDto GetById(Guid id);
        PageResult<ForwarderDto> Search(QueryForwarders query);
        ForwarderDto Update(Guid id, CreateForwarderDto dto);
    }

    public class ForwarderService : IForwarderService
    {
        private readonly IMapper mapper;
        private readonly ShipmentsDbContext dbContext;

        public ForwarderService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<ForwarderDto> Get()
        {
            var forwarders = dbContext.Forwarders.ToList();
            var forwardersDtos = mapper.Map<List<ForwarderDto>>(forwarders);

            return forwardersDtos;
        }

        public PageResult<ForwarderDto> Search(QueryForwarders query)
        {
            var forwarders = dbContext.Forwarders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
            {
                forwarders = forwarders.Where(x => 
                x.CarPlates.Contains(query.SearchPhrase) 
                || x.Speditor.Contains(query.SearchPhrase) 
                || x.LastName.Contains(query.SearchPhrase)
                || x.FirstName.Contains(query.SearchPhrase));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Forwarder, object>>>()
                {
                    { nameof(Forwarder.FirstName), t => t.FirstName },
                    { nameof(Forwarder.LastName), t => t.LastName },
                    { nameof(Forwarder.Speditor), t => t.Speditor },
                    { nameof(Forwarder.CarPlates), t => t.CarPlates },
                    { nameof(Forwarder.PhoneNumber), t => t.PhoneNumber },
                };

                var selectedColumn = columnsSelector[query.SortBy];

                forwarders = query.SortDirection == SortDirection.ASC ?
                    forwarders.OrderBy(selectedColumn)
                    : forwarders.OrderByDescending(selectedColumn);
            }
            var totalItemsCount = forwarders.Count();

            forwarders = forwarders
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .AsQueryable();

            var forwardersDto = mapper.Map<List<ForwarderDto>>(forwarders);

            var result = new PageResult<ForwarderDto>(forwardersDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public ForwarderDto GetById(Guid id)
        {
            var forwarder = CheckForwarder(id);
            var forwarderDto = mapper.Map<ForwarderDto>(forwarder);

            return forwarderDto;
        }

        public Guid Create(CreateForwarderDto dto)
        {
            var newForwarder = mapper.Map<Forwarder>(dto);
            newForwarder.Id = Guid.NewGuid();

            dbContext.Forwarders.Add(newForwarder);
            dbContext.SaveChanges();

            return newForwarder.Id;
        }

        public ForwarderDto Update(Guid id, CreateForwarderDto dto)
        {
            var forwarder = CheckForwarder(id);
            
            forwarder.Speditor = dto.Speditor;
            forwarder.PhoneNumber = dto.PhoneNumber;
            forwarder.FirstName = dto.FirstName;
            forwarder.LastName = dto.LastName;
            forwarder.CarPlates = dto.CarPlates;

            dbContext.Forwarders.Update(forwarder);
            dbContext.SaveChanges();
            
            var forwarderDto = mapper.Map<ForwarderDto>(forwarder);
            
            return forwarderDto;
        }

        public void Delete(Guid id)
        {
            var forwarder = CheckForwarder(id);
            dbContext.Forwarders.Remove(forwarder);
            dbContext.SaveChanges();
        }

        private Forwarder CheckForwarder(Guid id)
        {
            var forwarder = dbContext.Forwarders.FirstOrDefault(x => x.Id == id);
            if (forwarder == null)
            {
                throw new NotFoundException($"Przewoźnik z nr Id {id} nie znaleziony.");
            }

            return forwarder;
        }
            
    }
}
