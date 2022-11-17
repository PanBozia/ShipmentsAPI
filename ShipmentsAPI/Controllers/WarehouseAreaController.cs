using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseAreaController:ControllerBase
    {
        private readonly IWarehouseAreaService warehouseAreaService;

        public WarehouseAreaController(IWarehouseAreaService warehouseAreaService)
        {
            this.warehouseAreaService = warehouseAreaService;
        }
        
        [HttpGet]
        public ActionResult<List<WarehouseAreaDto>> Get()
        {
            var warehouseAreas = warehouseAreaService.Get();
            return Ok(warehouseAreas);
        }

        [HttpGet("{id}")]
        public ActionResult<WarehouseAreaDto> GetById([FromRoute] int id)
        {
            var area = warehouseAreaService.GetById(id);
            return Ok(area);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] CreateWarehouseAreaDto dto)
        {
            var newAreaId = warehouseAreaService.Create(dto);
            return Ok(newAreaId);
        }

        [HttpPut("{id}")]
        public ActionResult<WarehouseAreaDto> Update([FromRoute] int id, [FromBody] CreateWarehouseAreaDto dto)
        {
            var warehouseAreaDto = warehouseAreaService.Update(id, dto);
            return Ok(warehouseAreaDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            warehouseAreaService.Delete(id);
            return Ok();
        }
    }
}
