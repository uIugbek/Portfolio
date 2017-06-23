using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PortfolioBackend.Configurations
{
    /// <summary>
    /// BLL configuration section
    /// </summary>
    public class GlobalizationConfigurationSection : ConfigurationSection
    {
        private string _defaultCulture;
        /// <summary>
        /// Entity element that configurate from config
        /// </summary>
        [ConfigurationProperty(Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_DEFAULTCULTURE)]
        public string DefaultCulture
        {
            get
            {
                if (_defaultCulture == null)
                    _defaultCulture = (string)this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_DEFAULTCULTURE];
                return _defaultCulture;
            }
            set
            {
                this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_DEFAULTCULTURE] = value;
                _defaultCulture = value;
            }
        }

        private CulturesElementCollection _cultures;

        [ConfigurationProperty(Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES, IsDefaultCollection = true)]
        public CulturesElementCollection Cultures
        {
            get
            {
                if (_cultures == null)
                    _cultures = (CulturesElementCollection)this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES];
                return _cultures;
            }
            set
            {
                this[Constants.CONFIGURATION_NAME_FOR_SECTION_GLOBALIZATION_CULTURES] = value;
                _cultures = value;
            }
        }
    }
}