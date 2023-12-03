using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductLogic logic;

        public ProductController(IProductLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Product> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public Product Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Product value)
        {

            this.logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] Product value)
        {
            this.logic.Update(value);

        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            this.logic.Delete(id);
        }
    }
}
