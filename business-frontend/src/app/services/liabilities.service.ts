import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LiabilityData } from '../models/liability-data.model';

@Injectable({
  providedIn: 'root'
})
export class LiabilitiesService {
  private apiUrl = 'https://localhost:7127/api/Liabilities/get-saved'; // Adjust the URL as needed

  constructor(private http: HttpClient) {}

  getLiabilitiesData(): Observable<LiabilityData[]> {
    return this.http.get<LiabilityData[]>(this.apiUrl);
  }

  saveLiabilitiesData(data: LiabilityData[]): Observable<any> {
    const saveUrl = 'https://localhost:7127/api/Liabilities/save';
    return this.http.post(saveUrl, data);
  }

  setIntegrationStatus(id: number, isIntegrated: boolean): Observable<any> {
    return this.http.put(
      `https://localhost:7127/api/IntegratedStatus/${id}/update-status?isIntegrated=${isIntegrated}`,
      null
    );
  }

  fetchAndSaveFromIntegration(): Observable<any> {
    return this.http.get('https://localhost:7127/api/integration/fetch-and-save-liabilities');
  }
}
