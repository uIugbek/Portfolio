import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { Configuration } from '../app.constants';
import { HomeFooterComponent } from './home-footer/home-footer.component';
import { HomeNavigationComponent } from './home-navigation/home-navigation.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    HomeFooterComponent,
    HomeNavigationComponent
  ],
  exports: [
    HomeFooterComponent,
    HomeNavigationComponent
  ]
})

export class SharedModule {
    static forRoot(): ModuleWithProviders{
      return{
        ngModule: SharedModule,
        providers: [
          // UserService,
          Configuration
        ]
      }
    }
 }
