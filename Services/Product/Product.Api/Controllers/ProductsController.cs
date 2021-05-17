using CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Queries;
using Product.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IQueryBus queryBus;

        public ProductsController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        [HttpGet]
        public async Task<List<ProductDto>> Get()
        {
            return await queryBus.Send(new GetAllProductQuery());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await queryBus.Send(new GetProductByIdQuery(id)));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
