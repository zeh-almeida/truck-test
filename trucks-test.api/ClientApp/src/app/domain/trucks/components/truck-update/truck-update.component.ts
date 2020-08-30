import {
  Component, EventEmitter, OnDestroy, OnInit, Input, Output
} from '@angular/core';

import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';

import { UpdateTruck, FormOptions, FormValidations, ValidTruckTypes } from '../../models/update-truck.model';

@Component({
  selector: 'app-truck-update',
  templateUrl: './truck-update.component.html',
  styleUrls: ['./truck-update.component.scss']
})
export class TruckUpdateComponent implements OnInit, OnDestroy {
  @Input() truckId: string;

  @Output() truckUpdate: EventEmitter<any> = new EventEmitter();
  @Output() close: EventEmitter<any> = new EventEmitter();

  public orderForm: FormGroup;
  public types = ValidTruckTypes;

  public loadingForm = false;
  public submitted = false;

  private dataSubscription: Subscription;

  constructor(private formBuilder: FormBuilder,
    private orderService: TruckService) {
  }

  get f(): { [key: string]: AbstractControl; } { return this.orderForm.controls; }

  ngOnInit() {
    this.orderForm = this.formBuilder.group(FormOptions, FormValidations);

    this.orderService.get(this.truckId)
      .subscribe(truck => {
        this.f.truckType.setValue(truck.truckType);
        this.f.fabricationYear.setValue(truck.fabricationYear);
        this.f.modelYear.setValue(truck.modelYear);
        this.f.name.setValue(truck.name);
        this.f.plate.setValue(truck.plate);
      });
  }

  ngOnDestroy() {
    subscriptionCleaner(this.dataSubscription);
  }

  doCancel() {
    this.orderForm.reset();
    this.close.emit(null);
  }

  doSubmit() {
    this.submitted = true;

    if (!this.orderForm.invalid) {
      this.loadingForm = true;

      const formData = this.orderForm.value as UpdateTruck;
      subscriptionCleaner(this.dataSubscription);

      this.dataSubscription = this.orderService.update(this.truckId, formData)
        .subscribe(result => {
          if (result) {
            this.truckUpdate.emit(null);
            this.doCancel();
          }
        });
    }
  }
}
