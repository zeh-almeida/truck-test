import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { TruckModule } from './domain/trucks/trucks.module';

import { AppComponent } from './app.component';

import { NavMenuComponent } from './domain/common/components/nav-menu/nav-menu.component';

import { TruckIndexComponent } from './domain/trucks/components/truck-index/truck-index.component';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,

    NgbModule,

    TruckModule,

    RouterModule.forRoot([
      { path: '', component: TruckIndexComponent, pathMatch: 'full' }
    ])
  ],

  declarations: [
    AppComponent,
    NavMenuComponent
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
