import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { TruckService } from './services/truck.service';

import { TruckListComponent } from './components/truck-list/truck-list.component';
import { TruckCreateComponent } from './components/truck-create/truck-create.component';
import { TruckUpdateComponent } from './components/truck-update/truck-update.component';
import { TruckIndexComponent } from './components/truck-index/truck-index.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],

  declarations: [
    TruckListComponent,
    TruckCreateComponent,
    TruckUpdateComponent,
    TruckIndexComponent
  ],

  exports: [
    TruckListComponent,
    TruckCreateComponent,
    TruckUpdateComponent,
    TruckIndexComponent
  ],

  providers: [
    TruckService
  ],
})
export class TruckModule { }
