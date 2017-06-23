using PortfolioBackend.Core.BLL.Services;
using PortfolioBackend.Configurations;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBackend.Core.Controllers
{
    public class BaseEntityController<TEntity, TEntityModel, TEntityService> : Controller
        where TEntity : BaseEntity
        where TEntityModel : Model<TEntity>
        where TEntityService : BaseService<TEntity>
    {
        protected readonly TEntityService Service;

        protected BaseEntityController() : this(AppDependencyResolver.Current.GetService<TEntityService>())
        {
        }

        public BaseEntityController(TEntityService service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var ents = Service.AllAsQueryable;
            var list = ents.ToList();
            var result = list.Select(s => s.ConvertToModel<TEntity, TEntityModel>());

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async virtual Task<IActionResult> GetById(int id)
        {
            TEntity ent = await Service.ByIDAsync(id);

            if (ent == null)
                return NotFound();

            TEntityModel model = Activator.CreateInstance<TEntityModel>();
            model.LoadEntity(ent);

            return Ok(model);
        }

        [HttpPost]
        public virtual IActionResult Add([FromBody] TEntityModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity ent = model.CreateEntity();

            ent = Service.Create(ent);

            model.AfterCreateEntity(ent);

            return Ok(ent);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async virtual Task<IActionResult> Update(int id, [FromBody]TEntityModel model)
        {
            var ent = await Service.ByIDAsync(id);

            if (ent == null)
                return NotFound();

            ent = model.UpdateEntity(ent);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ent = Service.Update(ent);

            model.AfterUpdateEntity(ent);

            return Ok(ent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async virtual Task<IActionResult> Remove(int id)
        {
            TEntity ent = await Service.ByIDAsync(id);

            if (ent == null)
                return NotFound();

            TEntityModel model = Activator.CreateInstance<TEntityModel>();
            model.LoadEntity(ent);

            Service.Delete(ent);

            model.AfterDeleteEntity(ent);

            return Ok();
        }
    }
}
