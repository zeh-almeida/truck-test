import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { TruckModule } from './domain/trucks/trucks.module';

import { AppComponent } from './app.component';

import { NavMenuComponent } from './domain/common/components/nav-menu/nav-menu.component';
import { HomeComponent } from './domain/common/components/home/home.component';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,

    TruckModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],

  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
