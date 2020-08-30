import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  TemplateRef,
  ChangeDetectorRef
} from '@angular/core';

import { Subscription } from 'rxjs';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckService } from '../../services/truck.service';
import { GetTruck } from '../../models/get-truck.model';

@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.scss']
})
export class TruckListComponent implements OnInit, OnDestroy {
  @ViewChild('modalUpdate', { static: true }) updateModal: TemplateRef<any>;

  data = [];
  isReady = false;
  loaded = false;

  selectedTruckId: string;

  intervalId: number;
  private dataSubscription: Subscription;

  constructor(private changeDetectorRef: ChangeDetectorRef,
    private modal: NgbModal,
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

  deleteTruck(item: GetTruck) {
    this.orderService.delete(item.id)
      .subscribe(() => this.onRefresh());
  }

  onModalClose(event: any) {
    this.selectedTruckId = null;
    this.modal.dismissAll();
  }

  onTruckEvent(event: any) {
    this.onRefresh();
  }

  updateTruck(item: GetTruck) {
    this.selectedTruckId = item.id;
    this.modal.open(this.updateModal, { size: 'lg' });
  }
}
