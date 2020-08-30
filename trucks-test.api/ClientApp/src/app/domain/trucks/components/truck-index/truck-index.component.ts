import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckListComponent } from '../truck-list/truck-list.component';

@Component({
  selector: 'app-truck-index',
  templateUrl: './truck-index.component.html',
  styleUrls: ['./truck-index.component.scss']
})
export class TruckIndexComponent implements OnInit, OnDestroy {
  @ViewChild(TruckListComponent) private listComponent: TruckListComponent;

  private orderSubscription: Subscription;

  constructor() {
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    subscriptionCleaner(this.orderSubscription);
  }

  onOrderCreate(event:any) {
    this.listComponent.onRefresh();
  }
}
