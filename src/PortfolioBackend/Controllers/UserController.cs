using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Core.BLL.Base;
using PortfolioBackend.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public UserController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _dataAccessProvider.GetUsers();
        }

        [HttpGet("{id}")]
        public User Get(long id)
        {
            return _dataAccessProvider.GetUser(id);
        }

        [HttpPost]
        public void Post([FromBody]User value)
        {
            _dataAccessProvider.AddUser(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]User value)
        {
            _dataAccessProvider.UpdateUser(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _dataAccessProvider.DeleteUser(id);
        }
    }
}
