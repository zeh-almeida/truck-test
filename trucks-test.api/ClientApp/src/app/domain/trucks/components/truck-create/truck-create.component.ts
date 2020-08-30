import {
  Component, EventEmitter, OnDestroy, OnInit, Output
} from '@angular/core';

import { FormGroup, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';

import { CreateTruck, FormOptions, FormValidations, ValidTruckTypes } from '../../models/create-truck.model';

@Component({
  selector: 'app-truck-create',
  templateUrl: './truck-create.component.html',
  styleUrls: ['./truck-create.component.scss']
})
export class TruckCreateComponent implements OnInit, OnDestroy {
  @Output() truckCreate: EventEmitter<any> = new EventEmitter();
  @Output() close: EventEmitter<any> = new EventEmitter();

  public orderForm: FormGroup;
  public types = ValidTruckTypes;

  public loadingForm = false;
  public submitted = false;

  private dataSubscription: Subscription;

  constructor(private formBuilder: FormBuilder,
    private orderService: TruckService) {
  }

  get f() { return this.orderForm.controls; }

  ngOnInit() {
    this.orderForm = this.formBuilder.group(FormOptions, FormValidations);
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

      const formData = this.orderForm.value as CreateTruck;
      subscriptionCleaner(this.dataSubscription);

      this.dataSubscription = this.orderService.create(formData)
        .subscribe(result => {
          if (result) {
            this.truckCreate.emit(null);
            this.doCancel();
          }
        });
    }
  }
}
