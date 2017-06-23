import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { Permission, Permission_Locales } from './permission.model';
import { SelectList } from '../../shared/models/select-list.model';
import { Configuration } from '../../../app.constants';

@Injectable()
export class PermissionService {

    private actionUrl: string;
    private headers: Headers;
    constructor(private http: Http, private configuration: Configuration) {
        this.configuration = configuration;
        this.actionUrl = this.configuration.server + 'api/dashboard/permission/';

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
    }

    public GetAll = (): Observable<Permission[]> => {
        return this.http
            .get(this.actionUrl)// + 'GetCustom'
            .map((response: Response) => <Permission[]>response.json());
    }

    public GetSingle = (id: number): Observable<Permission> => {
        return this.http
            .get(this.actionUrl + id)
            .map((response: Response) => <Permission>response.json());
    }

    public GetPermissionCodeList = (): Observable<SelectList[]> => {
        return this.http
            .get(this.configuration.server + 'api/manual/GetPermissionCode')
            .map((response: Response) => <SelectList[]>response.json());
    }

    public Add = (permissionToAdd: Permission): Observable<Permission> => {
        var toAdd = JSON.stringify(
            {
                code: permissionToAdd.code,
                localizations: permissionToAdd.localizations,
            });

        return this.http
            .post(this.actionUrl, toAdd, { headers: this.headers })
            .map((response: Response) => <Permission>response.json());
    }

    public Update = (id: number, itemToUpdate: any): Observable<Permission> => {
        return this.http
            .put(this.actionUrl + id, JSON.stringify(itemToUpdate), { headers: this.headers })
            .map((response: Response) => <Permission>response.json());
    }

    public Delete = (id: number): Observable<any> => {
        return this.http.delete(this.actionUrl + id);
    }
}