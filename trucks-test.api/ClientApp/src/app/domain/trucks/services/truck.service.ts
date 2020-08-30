import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AbstractService } from '../../common/services/abstract.service';

import { GetTruck } from '../models/get-truck.model';
import { CreateTruck } from '../models/create-truck.model';
import { UpdateTruck } from '../models/update-truck.model';

@Injectable({
  providedIn: 'root',
})
export class TruckService extends AbstractService {

  private serviceUri = 'api/trucks';

  constructor(private http: HttpClient) {
    super();
  }

  getAll(): Observable<GetTruck[]> {
    return this.http.get<GetTruck[]>(this.serviceUri)
      .pipe(catchError(this.handleError<GetTruck[]>('getAll', [])));
  }

  get(id: string): Observable<GetTruck> {
    const uri = `${this.serviceUri}/${id}`;

    return this.http.get<GetTruck>(uri)
      .pipe(catchError(this.handleError<GetTruck>('get')));
  }

  update(id: string, data: UpdateTruck): Observable<GetTruck> {
    const uri = `${this.serviceUri}/${id}`;

    return this.http.put<GetTruck>(uri, data)
      .pipe(catchError(this.handleError<GetTruck>('update')));
  }

  delete(id: string): Observable<GetTruck> {
    const uri = `${this.serviceUri}/${id}`;

    return this.http.delete<GetTruck>(uri)
      .pipe(catchError(this.handleError<GetTruck>('delete')));
  }

  create(data: CreateTruck): Observable<GetTruck> {
    return this.http.post<GetTruck>(this.serviceUri, data)
      .pipe(catchError(this.handleError<GetTruck>('create')));
  }
}
