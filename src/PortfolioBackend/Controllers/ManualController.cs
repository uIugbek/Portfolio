using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.BLL.Services;
using PortfolioBackend.Core.Controllers;
using PortfolioBackend.Models.ViewModels;
using System;
using PortfolioBackend.Core.Utility;
using PortfolioBackend.Core.Enums;

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    public class ManualController : Controller
    {
        [HttpGet]
        [Route("GetPermissionCode")]
        public IActionResult GetPermissionCode()
        {
            var result = EnumHelper<PermissionCode>.ToSelectList();

            return Ok(result.Items);
        }
    }
}
