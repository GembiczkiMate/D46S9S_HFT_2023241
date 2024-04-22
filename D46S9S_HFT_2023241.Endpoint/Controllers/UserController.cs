using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Endpoint.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D46S9S_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic logic;
        IHubContext<SignalRHub> hub;

        public UserController(IUserLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<User> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] User value)
        {

            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("UserCreated",value);
        }


        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);

        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("UserDeleted", userToDelete);
        }
    }
}
