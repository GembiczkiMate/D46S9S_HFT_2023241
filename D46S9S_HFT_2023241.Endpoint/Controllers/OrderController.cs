using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;

        public OrderController( IOrderLogic logic)
        {
            this.logic = logic;   
        }
       
        [HttpGet]
        public IEnumerable<Order> ReadAll()
        {
            return this.logic.ReadAll();
        }

        
        [HttpGet("{id}")]
        public Order Read(int id)
        {
            return this.logic.Read(id);
        }

        
        [HttpPost]
        public void Create([FromBody] Order value)
        {

            this.logic.Create(value);
        }

        
        [HttpPut]
        public void Update( [FromBody] Order value)
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
