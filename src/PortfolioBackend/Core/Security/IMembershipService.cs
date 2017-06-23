using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.Security
{
    public interface IMembershipService
    {
        bool IsAutenticated { get; }
        object UserId { get; }
    }

    public interface IMembershipService<TPermissionType> : IMembershipService
    {
        TPermissionType[] Permissions { get; set; }
        int[] PermissionKeys { get; set; }
        bool HasPermission(TPermissionType permission);
    }
}
