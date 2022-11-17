using AutoMapper;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI.Services
{
    public interface IForwarderService
    {
        Guid Create(CreateForwarderDto dto);
        void Delete(Guid id);
        List<ForwarderDto> Get();
        ForwarderDto GetById(Guid id);
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
            forwarder.ContainerNumber = dto.ContainerNumber;    
            forwarder.PhoneNumber = dto.PhoneNumber;
            forwarder.ContainerSealNumber = dto.ContainerSealNumber;
            forwarder.FirstName = dto.FirstName;
            forwarder.LastName = dto.LastName;
            forwarder.CarPlates = dto.CarPlates;
            forwarder.ContainerType = dto.ContainerType;

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
