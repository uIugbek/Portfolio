using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PortfolioBackend.Configurations
{
    [ConfigurationCollection(typeof(CultureElement), AddItemName = Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE)]
    public class CulturesElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CultureElement)element).Index;
        }
    }

    public class CultureElement : ConfigurationElement
    {
        private static int index = 0;

        public CultureElement()
        {
            Index = ++index;
        }

        public int Index { get; set; }

        private string _code;
        [ConfigurationProperty(Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_CODE, IsRequired = true)]
        public string Code
        {
            get
            {
                if (_code == null)
                    _code = (string)this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_CODE];
                return _code;
            }
            set
            {
                this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_CODE] = value;
                _code = value;
            }
        }

        private int? _id;
        [ConfigurationProperty(Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_ID, IsRequired = true)]
        public int Id
        {
            get
            {
                if (_id == null)
                    _id = (int)this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_ID];
                return _id.GetValueOrDefault(0);
            }
            set
            {
                this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES_CULTURE_ID] = value;
                _id = value;
            }
        }
    }
}
