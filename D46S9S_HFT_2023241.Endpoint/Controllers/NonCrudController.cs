using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        // GET: api/<NonCrudController>

        IOrderLogic logic;

        
        public NonCrudController(IOrderLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<User> GetMostBuys()
        {
            return this.logic.MostBuys();
        }

        
        [HttpGet]
        public IEnumerable<Product> GetMostSells()
        {
            return this.logic.MostSells().AsEnumerable();
        }
        [HttpGet]
        public IEnumerable<User> GetBuyersOfNuts()
        {
            return this.logic.BuyersOfNuts() as IEnumerable<User>;
        }
        [HttpGet]
        public IEnumerable<OrderLogic.Data> GetDatas()
        {
            return this.logic.Datas();
        }
        [HttpGet]
        public IEnumerable<Order> GetOldestOrder()
        {
            return this.logic.OldesOrder();
        }

    }
}
