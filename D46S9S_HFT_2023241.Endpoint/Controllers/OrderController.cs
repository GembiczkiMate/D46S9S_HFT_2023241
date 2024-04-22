using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using D46S9S_HFT_2023241.Endpoint.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;
        IHubContext<SignalRHub> hub;

        public OrderController( IOrderLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("OrderCreated", value);
        }

        
        [HttpPut]
        public void Update( [FromBody] Order value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OrderUpdated", value);
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var orderToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("OrderDeleted", orderToDelete);
        }

        
    }
}
