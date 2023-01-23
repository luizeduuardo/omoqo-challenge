import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, filter, finalize, Observable, Subject, switchMap, take, throwError } from 'rxjs';
import { AuthService } from './auth.service';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private refreshTokenInProgress = false;
  private refreshTokenSubject: Subject<any> = new BehaviorSubject<any>(null);

  constructor(
    private _localStorageService: LocalStorageService,
    private _authService: AuthService
  ) { }

  private injectToken(request: HttpRequest<any>): HttpRequest<any> {
    const authentication = this._localStorageService.getAuth();

    if (!authentication) {
      return request;
    }

    return request.clone({
      headers: new HttpHeaders().set('Authorization', `Bearer ${authentication.token}`)
    });
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = this.injectToken(request);


    return next.handle(request).pipe(
      catchError(error => this.handleResponseError(error, request, next))
    );
  }

  handleResponseError(error: HttpErrorResponse, req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (error.status === 401) {

      if (this.refreshTokenInProgress) {
        return this.refreshTokenSubject.pipe(
          filter(result => result !== null),
          take(1),
          switchMap(() => next.handle(this.injectToken(req)))
        );
      } else {
        this.refreshTokenInProgress = true;

        this.refreshTokenSubject.next(null);

        return this._authService.authenticate().pipe(
          switchMap(() => next.handle(this.injectToken(req))),
          finalize(() => this.refreshTokenInProgress = false)
        );
      }
    }

    return throwError(error as HttpErrorResponse);
  }
}
