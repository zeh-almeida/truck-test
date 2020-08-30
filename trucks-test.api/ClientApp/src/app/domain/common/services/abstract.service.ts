
import { HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';

export class AbstractService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  protected handleError<T>(operation, result?: T) {
    return (error: any): Observable<T> => {
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

  protected log(message: string) {
    console.log(`${this.constructor.name}: ${message}`);
  }
}
