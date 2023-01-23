import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthData } from './auth.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private _http: HttpClient,
    private _router: Router,
    private _localStorageService: LocalStorageService
  ) { }

  authenticate(): Observable<any> {
    const body = {
      id: environment.userId,
      apiKey: environment.apiKey
    };

    return this._http.post<AuthData>(`${environment.webApiUrl}/user/authenticate`, body).pipe(
      switchMap((response: AuthData) => {
        this._localStorageService.saveAuth(response);
        return of(true);
      }));
  }

}
