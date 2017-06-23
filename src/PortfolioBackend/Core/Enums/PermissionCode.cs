using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.Enums
{
    public enum PermissionCode : int
    {
        [Description("Разрешение для Администрирование")]
        DASHBOARD_PERMISSION = 1,

        [Description("Разрешение для Пользователи")]
        USER_CRUD = 4,

        [Description("Разрешение для Роли")]
        ROLE_CRUD = 5,

        [Description("Разрешение для Права доступа")]
        PERMISSION_CRUD = 6,

        [Description("Разрешение для Словарь")]
        LOCALIZED_STRINGS_CRUD = 7,
    }
}
