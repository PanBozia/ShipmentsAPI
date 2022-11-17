using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController:ControllerBase
    {
        private readonly IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }
        
        [HttpGet]
        public ActionResult<List<StatusDto>> Get()
        {
            var statuses = statusService.Get();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public ActionResult<StatusDto> GetById([FromRoute] int id)
        {
            var status = statusService.GetById(id);
            return Ok(status);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] CreateStatusDto dto)
        {
            var newStatusId = statusService.Create(dto);
            return Ok(newStatusId);
        }

        [HttpPut("{id}")]
        public ActionResult<StatusDto> Update([FromRoute] int id, [FromBody] CreateStatusDto dto)
        {
            var statusDto = statusService.Update(id, dto);
            return Ok(statusDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            statusService.Delete(id);
            return Ok();
        }
    }
}
