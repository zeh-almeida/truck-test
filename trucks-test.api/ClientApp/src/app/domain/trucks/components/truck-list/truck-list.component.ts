import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';

import { GetTruck } from '../../models/get-truck.model';

@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.scss']
})
export class TruckListComponent implements OnInit, OnDestroy {
  public data: GetTruck[];
  public loaded: boolean;

  private dataSubscription: Subscription;

  constructor(private orderService: TruckService) {
    this.loaded = false;
  }

  ngOnInit() {
    this.onRefresh();
  }

  ngOnDestroy() {
    subscriptionCleaner(this.dataSubscription);
  }

  onRefresh() {
    subscriptionCleaner(this.dataSubscription);
    this.loaded = false;

    this.dataSubscription = this.orderService.getAll()
      .subscribe(response => {
        this.loaded = true;
        this.data = response;
      });
  }
}
