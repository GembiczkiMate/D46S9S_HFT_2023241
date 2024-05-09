using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using D46S9S_HFT_2023241.Endpoint.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductLogic logic;
        IOrderLogic orderLogic;
        IHubContext<SignalRHub> hub;
        OrderController order;

        public ProductController(IProductLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ProductCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Product value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ProductUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var productToDelete = this.logic.Read(id);
            
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ProductDeleted", productToDelete);
            this.hub.Clients.All.SendAsync("OrderDeleted", null);
        }
    }
}
