using PortfolioBackend.Core.BLL.Services;
using PortfolioBackend.Configurations;
using PortfolioBackend.Core.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PortfolioBackend.Core.Models
{
    public interface ILocalizableModel
    {
    }

    public class LocalizableModel<TEntity, TEntityLocale, TLocaleModel> : Model<TEntity>, ILocalizableModel
        where TEntity : BaseLocalizableEntity<TEntity,TEntityLocale>
        where TEntityLocale : BaseLocalizableEntity_Locale<TEntity>
        where TLocaleModel : Localizable_LocaleModel<TEntityLocale>
    {
        protected TEntityLocale CurrentLocale = null;

        public int Id { get; set; }

        public LocalizableModel()
        {
            Localizations = new List<TLocaleModel>();
        }

        public override void Initialize()
        {
            
        }

        public override void LoadEntity(TEntity t)
        {
            base.LoadEntity(t);
            Localizations.Sort((a, b) => a.CultureId - b.CultureId);
            CurrentLocale = t.GetCurrentLocale();
        }

        public override TEntity CreateEntity()
        {
            if (!Localizations.Any())
                throw new Exception("Not localizations in localizable model!");
            return base.CreateEntity();
        }

        public override TEntity UpdateEntity(TEntity t)
        {
            var ent = base.UpdateEntity(t);
            foreach (var item in ent.Localizations)
                if (ent.Localizations.Any(a => a != item && a.CultureId == item.CultureId))
                    throw new Exception("One or more localizations already exist!");
            return ent;
        }

        public List<TLocaleModel> Localizations { get; set; }
    }

    public class Localizable_LocaleModel<TEntityLocale> : Model<TEntityLocale>
        where TEntityLocale : class
    {
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int CultureId { get; set; }
    }
}
