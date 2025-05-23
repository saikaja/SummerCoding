import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject, Observable, interval, tap, switchMap, catchError, of, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}/Login`;
  private refreshUrl = `${environment.apiBaseUrl}/Login/refresh`;

  private isLoggedInSubject = new ReplaySubject<boolean>(1);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private refreshIntervalSub?: Subscription;

  constructor(private http: HttpClient, private router: Router) {
    this.checkAuthOnStartup();
  }

  private checkAuthOnStartup(): void {
    const token = localStorage.getItem('accessToken');

    if (token) {
      this.isLoggedInSubject.next(true);
      this.scheduleAutoRefresh();
    } else {
      this.isLoggedInSubject.next(false);

      // cleanup if someone hard-refreshed the browser while logged out
      localStorage.removeItem('firstName');
      localStorage.removeItem('lastName');
      localStorage.removeItem('lastLogin');
    }
  }

  login(credentials: any): Observable<any> {
    return this.http.post(this.apiUrl, credentials).pipe(
      tap((response: any) => {
        this.storeTokens(response);
        this.isLoggedInSubject.next(true);
        this.scheduleAutoRefresh();
        console.log('[AuthService] Login successful, refresh timer started.');
      })
    );
  }

  private storeTokens(response: any): void {
    localStorage.setItem('accessToken', response.accessToken);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('firstName', response.firstName);
    localStorage.setItem('lastName', response.lastName);
    localStorage.setItem('lastLogin', response.lastLogin || new Date().toISOString());
  }

  private scheduleAutoRefresh(): void {
    this.refreshIntervalSub?.unsubscribe();

    this.refreshIntervalSub = interval(270000)
      .pipe(
        switchMap(() => this.refreshToken()),
        catchError(err => {
          console.warn('[AuthService] Token refresh failed', err);
          this.logout();
          return of(null);
        })
      )
      .subscribe();
  }

  refreshToken(): Observable<any> {
    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) return of(null);

    return this.http.post(this.refreshUrl, { accessToken }).pipe(
      tap((response: any) => {
        localStorage.setItem('accessToken', response.accessToken);
        localStorage.setItem('refreshToken', response.refreshToken);
        console.log('[AuthService] Access token refreshed.');
      })
    );
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  logout(): void {
    localStorage.clear();
    this.refreshIntervalSub?.unsubscribe();
    this.isLoggedInSubject.next(false);
    console.log('[AuthService] Logout complete, refresh timer cleared.');
    this.router.navigate(['/login']);
  }
}