import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Ship, ShipListResponse } from './ship.model';

const url = `${environment.webApiUrl}/ship`;

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  constructor(
    private _http: HttpClient
  ) { }

  list(name: string, code: string, skip: number, limit: number): Observable<ShipListResponse> {
    let params: HttpParams = new HttpParams();
    params = params.set('skip', skip);
    params = params.set('limit', limit);
    params = params.set('name', name ?? '');
    params = params.set('code', code ?? '');

    return this._http.get<ShipListResponse>(`${url}/list`, { params });
  }

  remove(id: number): Observable<any> {
    return this._http.delete(`${url}/${id}`);
  }

  get(id: number): Observable<Ship> {
    return this._http.get<Ship>(`${url}/${id}`);
  }

  add(model: Ship): Observable<any> {
    return this._http.post(url, model);
  }

  update(model: Ship): Observable<any> {
    return this._http.put(url, model);
  }
}
