import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private oneTimePasscodeUrl = `${environment.apiBaseUrl}/OneTimePassCode`;
  private passwordUrl = `${environment.apiBaseUrl}/Password`;
  private authUrl = `${environment.apiBaseUrl}/auth`;

  constructor(private http: HttpClient) {}

  // Registration - Send initial OTP after user registers
  sendRegistration(data: any): Observable<any> {
    return this.http.post(`${this.oneTimePasscodeUrl}/send-otp`, data);
  }

  // Forgot Password - Request OTP
  sendPasswordResetOtp(dto: { userName: string }): Observable<any> {
    return this.http.post(`${this.passwordUrl}/forgot`, dto, { responseType: 'text' });
  }

  // Set or Reset Password
  setPassword(dto: { userName: string, oneTimePassCode: string, newPassword: string, confirmPassword: string }): Observable<any> {
    return this.http.post(`${this.passwordUrl}/reset`, dto, { responseType: 'text' });
  }

  // Login user
  login(dto: { userName: string, password: string }): Observable<any> {
    return this.http.post(`${this.authUrl}/login`, dto);
  }
}
