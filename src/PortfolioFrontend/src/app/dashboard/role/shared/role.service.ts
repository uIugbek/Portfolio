import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { Role, Role_Locales } from './role.model';
import { Permission } from '../../permission/shared/permission.model';
import { SelectList } from '../../../shared/models/select-list.model';
import { Configuration } from '../../../app.constants';

@Injectable()
export class RoleService {

    private actionUrl: string;
    private headers: Headers;
    
    constructor(private http: Http, private configuration: Configuration) {
        this.configuration = configuration;
        this.actionUrl = this.configuration.server + 'api/dashboard/role/';

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
    }

    public GetAll = (): Observable<Role[]> => {
        return this.http
            .get(this.actionUrl)
            .map((response: Response) => <Role[]>response.json());
    }

    public GetSingle = (id: number): Observable<Role> => {
        return this.http
            .get(this.actionUrl)// + 'GetSigle?id=' + id
            .map((response: Response) => <Role>response.json());
    }

    public Add = (itemToAdd: Role): Observable<Role> => {
        var toAdd = JSON.stringify(
            {
                localizations: itemToAdd.localizations,
                roleInPermissions: itemToAdd.roleInPermissions
            });

        return this.http
            .post(this.actionUrl, toAdd, { headers: this.headers })
            .map((response: Response) => <Role>response.json());
    }

    public Update = (id: number, itemToUpdate: any): Observable<Role> => {
        return this.http
            .put(this.actionUrl + id, JSON.stringify(itemToUpdate), { headers: this.headers })
            .map((response: Response) => <Role>response.json());
    }

    public Delete = (id: number): Observable<any> => {
        return this.http.delete(this.actionUrl + id);
    }
}