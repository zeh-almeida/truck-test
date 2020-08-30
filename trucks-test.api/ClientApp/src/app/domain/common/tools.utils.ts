
import { Router, NavigationExtras } from '@angular/router';

import { Subscription } from 'rxjs';

export const subscriptionCleaner = (sub: Subscription) => {
  if (sub) {
    sub.unsubscribe();
  }
};

export const routerRefresh = (router: Router, extras?: NavigationExtras) => {
  const url = router.url.split('?')[0];

  router.navigated = false;
  router.navigate([url], extras);
};
