import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OtpService {
  private apiUrl = `${environment.apiBaseUrl}/OneTimePassCode/create-password`;

  constructor(private http: HttpClient) {}

  setPassword(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
