import { ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';

@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.scss']
})
export class TruckListComponent implements OnInit, OnDestroy {
  data = [];
  isReady = false;
  loaded = false;

  intervalId: number;
  private dataSubscription: Subscription;

  constructor(private changeDetectorRef: ChangeDetectorRef,
    private orderService: TruckService) {
  }

  ngOnInit() {
    this.onRefresh();
  }

  ngOnDestroy() {
    subscriptionCleaner(this.dataSubscription);
  }

  onRefresh() {
    subscriptionCleaner(this.dataSubscription);

    this.dataSubscription = this.orderService.getAll()
      .subscribe(response => {
        this.loaded = true;
        this.data = response;

        this.isReady = response !== undefined
          && response !== null
          && response.length > 0;

        this.changeDetectorRef.detectChanges();
      });
  }

  deleteTruck(item: any) {
    this.orderService.delete(item.id)
      .subscribe(() => this.onRefresh());
  }
}
