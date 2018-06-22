import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError} from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class StockService {

  constructor(private _http: HttpClient) { }

  AddStock(item: string): Observable<any> {
    console.log('lets post');
    const input = {
      Value: item
    };

    const body = JSON.stringify(input);
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = {headers: headers};
    return this._http.post<any>('https://localhost:44398/api/stock/', body, options)
    .pipe(
      tap(
        data => console.log(data),
        error => this.handleError(error)
      )
    );
  }

  private handleError(err: HttpErrorResponse) {
    if (err.statusText != null) {
        console.log(err.statusText);
    }
    return Observable.throw(err.status);
  }

}
