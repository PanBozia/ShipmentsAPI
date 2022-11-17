using AutoMapper;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI.Services
{
    public interface IWarehouseAreaService
    {
        List<WarehouseAreaDto> Get();
        WarehouseAreaDto GetById(int id);
        int Create(CreateWarehouseAreaDto dto);
        WarehouseAreaDto Update(int id, CreateWarehouseAreaDto dto);
        void Delete(int id);
    }

    public class WarehouseAreaService : IWarehouseAreaService
    {

        private IMapper mapper;
        private ShipmentsDbContext dbContext;

        public WarehouseAreaService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<WarehouseAreaDto> Get()
        {
            var warehouseAreas = dbContext.WarehouseAreas.ToList();
            if (warehouseAreas == null)
            {
                throw new NotFoundException("Warehouse area not found");
            }
            var warehouseAreasDtos = mapper.Map<List<WarehouseAreaDto>>(warehouseAreas);

            return warehouseAreasDtos;
        }
        public WarehouseAreaDto GetById(int id)
        {
            var warehouseArea = CheckIfWarehouseAreaExists(id);
            var warehouseAreaDto = mapper.Map<WarehouseAreaDto>(warehouseArea);

            return warehouseAreaDto;
        }

        public int Create(CreateWarehouseAreaDto dto)
        {
            var warehouseArea = mapper.Map<WarehouseArea>(dto);
            dbContext.WarehouseAreas.Add(warehouseArea);
            dbContext.SaveChanges();

            return warehouseArea.Id;
        }

        public WarehouseAreaDto Update(int id, CreateWarehouseAreaDto dto)
        {
            var warehouseArea = CheckIfWarehouseAreaExists(id);
            warehouseArea.Name = dto.Name;
            
            dbContext.WarehouseAreas.Update(warehouseArea);
            dbContext.SaveChanges();
            var warehouseAreaDto = mapper.Map<WarehouseAreaDto>(warehouseArea);

            return warehouseAreaDto;
        }

        public void Delete(int id)
        {
            var warehouseArea = CheckIfWarehouseAreaExists(id);
            dbContext.WarehouseAreas.Remove(warehouseArea);
            dbContext.SaveChanges();
        }

        private WarehouseArea CheckIfWarehouseAreaExists(int id)
        {
            var warehouseArea = dbContext.WarehouseAreas.FirstOrDefault(x => x.Id == id);
            if (warehouseArea == null)
            {
                throw new NotFoundException($"Warehouse Area with id: {id} not found");
            }
            return warehouseArea;
        }

    }
}
