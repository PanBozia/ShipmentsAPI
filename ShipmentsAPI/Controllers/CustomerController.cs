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
    public class CustomerController:ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpGet]
        public ActionResult<List<CustomerDto>> Get()
        {
            var customers = customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetById([FromRoute] Guid id)
        {
            var customer = customerService.GetById(id);
            return Ok(customer);
        }

        [HttpGet("search")]
        public ActionResult<PageResult<CustomerDto>> Search([FromQuery] QueryCustomers filter)
        {
            var customers = customerService.Search(filter);
            return Ok(customers);
        }

        [HttpPost]
        public ActionResult<Guid> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            var newCustomer = customerService.Create(createCustomerDto);
            return Ok(newCustomer);
        }

        [HttpPut("{id}")]
        public ActionResult<CustomerDto> Update([FromRoute] Guid id, [FromBody] CreateCustomerDto dto)
        {
            var customer = customerService.Update(id, dto);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            customerService.Delete(id);

            return Ok();
        }
    }
}
