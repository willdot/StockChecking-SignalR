import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError} from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';
import { StockItems } from '../models/StockItems';


@Injectable({
  providedIn: 'root'
})
export class StockService {

  constructor(private _http: HttpClient) { }

  AddStock(item: StockItems): Observable<any> {
    console.log('lets post');
    const input = {
      name: item.name,
      amount: ''
    };

    const body = JSON.stringify(item);
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = {headers: headers};
    return this._http.post<any>('https://localhost:44398/api/stock/add', body, options)
    .pipe(
      tap(
        data => console.log(data),
        error => this.handleError(error)
      )
    );
  }

  RemoveStock(item: StockItems): Observable<any> {
    console.log('lets post');
    const input = {
      name: item.name,
      amount: ''
    };

    const body = JSON.stringify(item);
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = {headers: headers};
    return this._http.post<any>('https://localhost:44398/api/stock/remove', body, options)
    .pipe(
      tap(
        data => console.log(data),
        error => this.handleError(error)
      )
    );
  }

  GetStock(): Observable<StockItems[]> {
    console.log('Lets get some stock');

    return this._http.get<StockItems[]>('https://localhost:44398/api/stock/')
    .pipe(
      tap(
        data => console.log(data),
        error => this.handleError(error)
      )
    );
  }

  private handleError(err: HttpErrorResponse) {
    if (err.statusText != null) {
      console.log('Something bad happened');
        console.log(err.statusText);
    }
    return throwError(err.status);
  }

}
