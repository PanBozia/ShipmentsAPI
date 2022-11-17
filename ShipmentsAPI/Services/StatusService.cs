using AutoMapper;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI.Services
{
    public interface IStatusService
    {
        List<StatusDto> Get();
        StatusDto GetById(int id);
        int Create(CreateStatusDto dto);
        StatusDto Update(int id, CreateStatusDto dto);
        void Delete(int id);
        
    }

    public class StatusService : IStatusService
    {

        private IMapper mapper;
        private ShipmentsDbContext dbContext;

        public StatusService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<StatusDto> Get()
        {
            var statuses = dbContext.Statuses.ToList();
            if (statuses == null)
            {
                throw new NotFoundException("Statuses not found");
            }
            var statusesDtos = mapper.Map<List<StatusDto>>(statuses);

            return statusesDtos;
        }
        public StatusDto GetById(int id)
        {
            var status = CheckIfStatusExists(id);
            var statusDto = mapper.Map<StatusDto>(status);

            return statusDto;
        }

        public int Create(CreateStatusDto dto)
        {
            var status = mapper.Map<Status>(dto);
            dbContext.Statuses.Add(status);
            dbContext.SaveChanges();

            return status.Id;
        }

        public StatusDto Update(int id, CreateStatusDto dto)
        {
            var status = CheckIfStatusExists(id);
            status.Name = dto.Name;
            status.Description = dto.Description;
            dbContext.Statuses.Update(status);
            dbContext.SaveChanges();
            var statusDto = mapper.Map<StatusDto>(status);

            return statusDto;
        }

        public void Delete(int id)
        {
            var status = CheckIfStatusExists(id);
            dbContext.Statuses.Remove(status);
            dbContext.SaveChanges();
        }

        private Status CheckIfStatusExists(int id)
        {
            var status = dbContext.Statuses.FirstOrDefault(x => x.Id == id);
            if (status == null)
            {
                throw new NotFoundException($"Status with id: {id} not found");
            }
            return status;
        }

    }
}
