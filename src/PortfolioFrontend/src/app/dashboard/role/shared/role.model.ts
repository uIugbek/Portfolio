import { Permission } from '../../permission/shared/permission.model';
import { PermissionService } from '../../permission/shared/permission.service';

export class Role {

    public id: number;
    public name: string;
    public localizations: Role_Locales[];
    public roleInPermissions: RoleInPermission[];

    constructor(
        private permissionService: PermissionService
    ) {
        this.localizations = [
            new Role_Locales(1),
            new Role_Locales(2),
            new Role_Locales(3)
        ];
        this.roleInPermissions = new Array<RoleInPermission>();
    }

    initialize() {
        this.permissionService.GetAll().subscribe(
            data => this.roleInPermissions = data.map(function (element, index) {
                var role = new RoleInPermission();
                role.initialize(element.id);
                return role;
            }));
    }

    loadEntity() {

    }

}

export class Role_Locales {

    public id: number;
    public name: string;
    public description: string;
    public cultureId: number;
    public localizableEntityId: number;

    constructor(cultureId: number) {
        this.cultureId = cultureId;
    }

}

export class RoleInPermission {

    public id: number;
    public roleId: number;
    public permissionId: number;
    public isAccessible: boolean;

    constructor() {//permissionId: number
        // this.permissionId = permissionId;
        // this.isAccessible = false;
    }

    initialize(permissionId: number){
        this.permissionId = permissionId;
    }

}