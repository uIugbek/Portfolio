using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.BLL.Services;
using PortfolioBackend.Core.Controllers;
using PortfolioBackend.Models.ViewModels;
using System;

namespace PortfolioBackend.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Route("api/dashboard/[controller]")]
    public class UserController : BaseEntityController<User, UserViewModel, UserService>
    {
       
    }
}
