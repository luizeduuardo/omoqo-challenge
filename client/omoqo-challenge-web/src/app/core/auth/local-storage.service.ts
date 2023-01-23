import { Injectable } from '@angular/core';
import { AuthData } from './auth.model';

export const AUTH_KEY = 'omoqo-auth';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public saveAuth(auth: AuthData): void {
    localStorage.setItem(AUTH_KEY, JSON.stringify(auth));
  }

  public getAuth(): AuthData | null {
    const authData = localStorage.getItem(AUTH_KEY);

    if (!authData) {
      return null;
    }

    return JSON.parse(localStorage.getItem(AUTH_KEY) ?? '');
  }

  public logout(): void {
    localStorage.clear();
  }
}
