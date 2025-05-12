import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BusinessData } from '../models/business-data.model';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  private apiUrl = 'https://localhost:7127/api/Business/get-saved'; 

  constructor(private http: HttpClient) {}

  getBusinessData(): Observable<BusinessData[]> {
    return this.http.get<BusinessData[]>(this.apiUrl);
  }

  saveBusinessData(data: BusinessData[]): Observable<any> {
    const saveUrl = 'https://localhost:7127/api/Business/save'; 
    return this.http.post(saveUrl, data);
  }
  setIntegrationStatus(id: number, isIntegrated: boolean): Observable<any> {
    return this.http.post(`https://localhost:7127/api/integration/update-status`, {
      integratedTypeId: id,
      isIntegrated
    });
  }
  
  fetchAndSaveFromIntegration(): Observable<any> {
    return this.http.get('https://localhost:7127/api/integration/fetch-and-save');
  }
  
}
