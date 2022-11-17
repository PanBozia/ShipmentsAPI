﻿using Microsoft.AspNetCore.Mvc;
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
            return orderService.Create(dto);
        }


    }
}
