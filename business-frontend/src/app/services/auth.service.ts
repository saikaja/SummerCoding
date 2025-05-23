import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ReplaySubject, Observable, interval, tap, switchMap, catchError, of } from 'rxjs';
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

  private refreshInterval$: any;

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
    }
  }

  login(credentials: any): Observable<any> {
    return this.http.post(this.apiUrl, credentials).pipe(
      tap((response: any) => {
        this.storeTokens(response);
        this.isLoggedInSubject.next(true);
        this.scheduleAutoRefresh();
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
    if (this.refreshInterval$) {
      this.refreshInterval$.unsubscribe();
    }

    this.refreshInterval$ = interval(270000) // every 4.5 mins
      .pipe(
        switchMap(() => this.refreshToken()),
        catchError(err => {
          console.warn('Refresh token failed:', err);
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
      })
    );
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  logout(): void {
    localStorage.clear();
    if (this.refreshInterval$) this.refreshInterval$.unsubscribe();
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/login']);
  }
}