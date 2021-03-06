import {
  Component,
  ChangeDetectionStrategy,
  OnDestroy,
  ViewChild,
  TemplateRef
} from '@angular/core';

import { Subscription } from 'rxjs';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { subscriptionCleaner } from '../../../common/tools.utils';

import { TruckListComponent } from '../truck-list/truck-list.component';

@Component({
  selector: 'app-truck-index',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './truck-index.component.html',
  styleUrls: ['./truck-index.component.scss']
})
export class TruckIndexComponent implements OnDestroy {
  @ViewChild(TruckListComponent, { static: false }) private listComponent: TruckListComponent;
  @ViewChild('modalCreate', { static: true }) createModal: TemplateRef<any>;

  private orderSubscription: Subscription;

  constructor(private modal: NgbModal) {
  }

  ngOnDestroy() {
    subscriptionCleaner(this.orderSubscription);
  }

  createTruck() {
    this.modal.open(this.createModal, { size: 'lg' });
  }

  onModalClose(event: any) {
    this.modal.dismissAll();
  }

  onTruckEvent(event: any) {
    this.listComponent.onRefresh();
  }
}
