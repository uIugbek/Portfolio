export class Permission
{

    public id: number;
    public code: number;
    public name: string;
    public localizations: Permission_Locales[];

    constructor() {
        this.localizations = [
            new Permission_Locales(1),
            new Permission_Locales(2),
            new Permission_Locales(3)
        ];
    }
 
}

export class Permission_Locales {

    public id: number;
    public name: string;
    public description: string;
    public cultureId: number;
    public localizableEntityId: number;

    constructor(cultureId: number) {
        this.cultureId = cultureId;
    }

}