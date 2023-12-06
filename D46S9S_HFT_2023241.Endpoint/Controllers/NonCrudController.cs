using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
        public IEnumerable<User> GetMostBuysID()
        {
            return this.logic.MostBuysID();
        }

        
        [HttpGet]
        public IEnumerable<Product> GetMostSellsID()
        {
            return this.logic.MostSellsID();
        }
        [HttpGet]
        public IEnumerable<int> GetBuyersOfNutsID()
        {
            return this.logic.BuyersOfNutsID();
        }
        [HttpGet]
        public IEnumerable<OrderLogic.Data> GetDatas()
        {
            return this.logic.Datas();
        }
        [HttpGet]
        public IEnumerable<OrderLogic.UO> GetUsersOrder()
        {
            return this.logic.UsersOrder();
        }
        [HttpGet]
        public IEnumerable<Product> GetOldestOrder()
        {
            return this.logic.OldesOrder();
        }

    }
}
