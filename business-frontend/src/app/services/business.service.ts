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
  
}
