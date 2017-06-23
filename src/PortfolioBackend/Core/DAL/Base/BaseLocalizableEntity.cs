using Microsoft.EntityFrameworkCore.LazyLoading;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public interface ILocalizable<TLocale>
        where TLocale : class, ILocale
    {
        ICollection<TLocale> Localizations { get; set; }
    }

    public interface ILocale
    {
        string Name { get; set; }
        int CultureId { get; set; }
    }

    public class BaseLocalizableEntity<TEntity, TEntityLocale> : SimpleBaseEntity, ILocalizable<TEntityLocale>
        where TEntity : class
        where TEntityLocale : BaseLocalizableEntity_Locale<TEntity>
    {
        public BaseLocalizableEntity()
        {
            Localizations = new List<TEntityLocale>();
        }

        [InverseProperty("LocalizableEntity")]
        public virtual ICollection<TEntityLocale> Localizations { get; set; }
    }

    public class BaseLocalizableEntity_Locale<TEntity> : SimpleBaseEntity, ILocale
        where TEntity : class
    {
        //private LazyReference<TEntity> _localizableEntity = new LazyReference<TEntity>();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public int CultureId { get; set; }
        public int LocalizableEntityId { get; set; }

        [ForeignKey("CultureId")]
        public virtual Culture Culture { get; set; }

        [ForeignKey("LocalizableEntityId")]
        public TEntity LocalizableEntity
        {
            get;
            //{
            //    return _localizableEntity.GetValue(this, nameof(TEntity));
            //}
            set;
            //{
            //    _localizableEntity.SetValue(value);
            //}
        }
    }
}
