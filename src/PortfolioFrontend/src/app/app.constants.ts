import { Injectable } from '@angular/core';
import { Culture } from './shared/models/culture.model';

@Injectable()
export class Configuration {
    public server: string = "http://localhost:5050/";
    public host: string = "http://localhost:4200";
    public cultures: Culture[] = [
        new Culture(1, 'uz', 'Uzbek'),
        new Culture(2, 'ru', 'Russian'),
        new Culture(3, 'en', 'English'),
    ];
}