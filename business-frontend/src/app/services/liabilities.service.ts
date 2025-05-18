import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LiabilityData } from '../models/liability-data.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LiabilitiesService {
  private baseUrl = `${environment.apiBaseUrl2}/Liabilities`;
  private integratedStatusUrl = `${environment.apiBaseUrl2}/IntegratedStatus`;

  constructor(private http: HttpClient) {}

  getLiabilitiesData(): Observable<LiabilityData[]> {
    return this.http.get<LiabilityData[]>(`${this.baseUrl}/get-saved`);
  }

  saveLiabilitiesData(data: LiabilityData[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/save`, data);
  }

  setIntegrationStatus(id: number, isIntegrated: boolean): Observable<any> {
    return this.http.put(`${this.integratedStatusUrl}/${id}/update-status?isIntegrated=${isIntegrated}`, null);
  }
}
