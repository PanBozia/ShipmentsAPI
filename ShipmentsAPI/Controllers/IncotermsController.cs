using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncotermsController : ControllerBase
    {
        private readonly ShipmentsDbContext dbContext;
        private readonly IMapper mapper;

        public IncotermsController(ShipmentsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<IncotermDto>> Get()
        {
            
            var incoterms = dbContext.Incoterms.ToList();
            if (incoterms is null)
            {
                throw new NotFoundException("Incoterms not found");
            }
            var incotermsDtos = mapper.Map<List<IncotermDto>>(incoterms);
            
            return Ok(incotermsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IncotermDto> GetById([FromRoute] int id)
        {
            var incoterm = dbContext.Incoterms.FirstOrDefault(x => x.Id == id);
            if(incoterm == null)
            {
                throw new NotFoundException($"Incoterms with id: {id} not found");
            }
            var incotermDto = mapper.Map<IncotermDto>(incoterm);
            
            return Ok(incotermDto);
        }


    }
}
