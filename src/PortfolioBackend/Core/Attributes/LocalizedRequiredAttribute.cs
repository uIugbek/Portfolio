using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PortfolioBackend.Core.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public LocalizedRequiredAttribute()
            : base()
        {
            ErrorMessage = @"Поле '{0}' обязательно для заполнение".Localize();
        }
        
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage.Localize(), name);
        }
    }
}