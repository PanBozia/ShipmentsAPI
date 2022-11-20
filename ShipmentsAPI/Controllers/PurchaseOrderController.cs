using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController:ControllerBase
    {
        private readonly IPurchaseOrderService orderService;

        public PurchaseOrderController(IPurchaseOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<PurchaseOrderDto>> Get()
        {
            return orderService.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<PurchaseOrderDto> GetById([FromRoute] Guid id)
        {
            return orderService.GetById(id);
        }
        [HttpPost]
        public ActionResult<Guid> Create([FromBody] CreatePurchaseOrderDto dto)
        {
            var newOrderId = orderService.Create(dto);
            return Ok(newOrderId);
        }
        

        [HttpPut("{id}")]
        public ActionResult<PurchaseOrderDto> Update([FromRoute] Guid id, CreatePurchaseOrderDto dto)
        {
            var updatedOrder = orderService.Update(id, dto);
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            orderService.Delete(id);
            return Ok();
        }
    }
}
