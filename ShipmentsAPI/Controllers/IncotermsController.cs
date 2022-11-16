using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncotermsController : ControllerBase
    {
        private readonly ShipmentsDbContext dbContext;

        public IncotermsController(ShipmentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[HttpGet]
        //public ActionResult<List<IncotermDto>> Get()
        //{

        //}


    }
}
