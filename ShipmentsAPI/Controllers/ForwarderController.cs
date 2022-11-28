using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Services;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForwarderController : ControllerBase
    {
        private readonly IForwarderService forwarderService;

        public ForwarderController(IForwarderService forwarderService)
        {
            this.forwarderService = forwarderService;
        }
        
        [HttpGet]
        public ActionResult<List<ForwarderDto>> Get()
        {
            var forwardersDtos = forwarderService.Get();
            return Ok(forwardersDtos);
        }
        [HttpGet("search")]
        public ActionResult<PageResult<ForwarderDto>> Search([FromQuery] QueryForwarders filter)
        {
            var forwarders = forwarderService.Search(filter);
            return Ok(forwarders);
        }

        [HttpGet("{id}")]
        public ActionResult<ForwarderDto> GetById([FromRoute] Guid id)
        {
            var forwarderDto = forwarderService.GetById(id);
            return Ok(forwarderDto);
        }

        [HttpPost]
        public ActionResult<Guid> Create(CreateForwarderDto dto)
        {
            var newForwarderId = forwarderService.Create(dto);
            return Ok(newForwarderId);
        }

        [HttpPut("{id}")]
        public ActionResult<ForwarderDto> Update([FromRoute] Guid id, [FromBody] CreateForwarderDto dto)
        {
            var newForwarderDto = forwarderService.Update(id, dto);
            return Ok(newForwarderDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            forwarderService.Delete(id);
            return Ok();
        }
    }
}
