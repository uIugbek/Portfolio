using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Configurations
{
    public class EntityProperties
    {
        public static bool UseDefault { get; set; }
        public static bool IsDeleted { get; set; }
        public static bool OwnerId { get; set; }
        public static bool ModifiedDate { get; set; }
        public static bool CreatedDate { get; set; }
    }
}
