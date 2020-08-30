import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';

import { CreateTruck, FormOptions, FormValidations } from '../../models/create-truck.model';

@Component({
  selector: 'app-truck-create',
  templateUrl: './truck-create.component.html',
  styleUrls: ['./truck-create.component.scss']
})
export class TruckCreateComponent implements OnInit, OnDestroy {
  @Output() truckCreate: EventEmitter<any> = new EventEmitter();

  public orderForm: FormGroup;
  public loadingForm = false;
  public submitted = false;
  public orderResult = "";

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
  }

  doSubmit() {
    this.submitted = true;

    if (!this.orderForm.invalid) {
      this.loadingForm = true;

      const formData = this.orderForm.value as CreateTruck;
      subscriptionCleaner(this.dataSubscription);

      this.dataSubscription = this.orderService.create(formData)
        .subscribe(result => {
          this.orderResult = result.data;
          this.truckCreate.emit(null);
        });
    }
  }
}
