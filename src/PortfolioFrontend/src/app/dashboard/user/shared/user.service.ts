import { User } from './user.model';
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { Configuration } from '../../../app.constants';

@Injectable()
export class UserService {

    private actionUrl: string;
    private headers: Headers;

    constructor(private http: Http, private configuration: Configuration) {

        this.actionUrl = configuration.server + 'api/dashboard/user/';

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
    }

    public GetAll = (): Observable<User[]> => {
        return this.http
            .get(this.actionUrl)
            .map((response: Response) => <User[]>response.json());
    }

    public GetSingle = (id: number): Observable<User> => {
        return this.http
            .get(this.actionUrl + id)
            .map((response: Response) => <User>response.json());
    }

    public Add = (userToAdd: User): Observable<User> => {
        var toAdd = JSON.stringify(
            { 
                name: userToAdd.name, 
                login: userToAdd.login, 
                email: userToAdd.email, 
                isBlocked: userToAdd.isBlocked 
            });

        return this.http
            .post(this.actionUrl, toAdd, { headers: this.headers })
            .map((response: Response) => <User>response.json());
    }

    public Update = (id: number, itemToUpdate: any): Observable<User> => {
        return this.http
            .put(this.actionUrl + id, JSON.stringify(itemToUpdate), { headers: this.headers })
            .map((response: Response) => <User>response.json());
    }

    public Delete = (id: number): Observable<any> => {
        return this.http.delete(this.actionUrl + id);
    }
}