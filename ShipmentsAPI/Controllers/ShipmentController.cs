using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            this.shipmentService = shipmentService;
        }

        [HttpGet]
        public ActionResult<List<ShipmentDto>> Get()
        {
            var shipments = shipmentService.Get();
            return Ok(shipments);
        }
        [HttpPost("search")]
        public ActionResult<PageResult<ShipmentDto>> Search([FromBody] QueryShipments filter)
        {
            var shipments = shipmentService.Search(filter);
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public ActionResult<List<ShipmentDto>> GetById([FromRoute] Guid id)
        {
            var shipment = shipmentService.GetById(id);
            return Ok(shipment);
        }
        [HttpPost]
        public ActionResult<Guid> Create([FromBody] CreateShipmentDto dto)
        {
            var newShipmentId = shipmentService.Create(dto);
            return Ok(newShipmentId);
        }

        [HttpPost("{shipmentId}/{orderId}")]
        public ActionResult AddPurchaseOrder([FromRoute] Guid shipmentId, [FromRoute] Guid orderId)
        {
            shipmentService.AddOrderToShipment(shipmentId, orderId);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<ShipmentBriefDto> Update([FromRoute] Guid id, [FromBody] CreateShipmentDto dto)
        {
            var updatedShipment = shipmentService.Update(id, dto);
            return Ok(updatedShipment);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            shipmentService.Delete(id);
            return Ok();
        }
    }
}
