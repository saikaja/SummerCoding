import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BusinessData } from '../models/business-data.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  private baseUrl = `${environment.apiBaseUrl2}/Business`;
  private integratedStatusUrl = `${environment.apiBaseUrl2}/IntegratedStatus`;

  constructor(private http: HttpClient) {}

  getBusinessData(): Observable<BusinessData[]> {
    return this.http.get<BusinessData[]>(`${this.baseUrl}/get-saved`);
  }

  saveBusinessData(data: BusinessData[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/save`, data);
  }

  setIntegrationStatus(id: number, isIntegrated: boolean): Observable<any> {
    return this.http.put(`${this.integratedStatusUrl}/${id}/update-status?isIntegrated=${isIntegrated}`, null);
  }
}
