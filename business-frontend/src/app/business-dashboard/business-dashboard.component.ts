import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';  
import { CommonModule } from '@angular/common';
import { BusinessService } from '../services/business.service';
import { BusinessData } from '../models/business-data.model';

@Component({
  selector: 'app-business-dashboard',
  standalone: true,  
  imports: [CommonModule, FormsModule],  
  templateUrl: './business-dashboard.component.html',
  styleUrls: ['./business-dashboard.component.css']
})
export class BusinessDashboardComponent implements OnInit {
  businessData: BusinessData[] = [];

  constructor(private businessService: BusinessService) {}

  ngOnInit(): void {
    this.businessService.getBusinessData().subscribe(data => {
      this.businessData = data;
    });
  }

  save(): void {
    console.log('Sending data to API:', this.businessData);  
  
    this.businessService.saveBusinessData(this.businessData).subscribe({
      next: () => {
        alert('Data saved successfully.');
      },
      error: (error) => {
        console.error('Save failed:', error);
        alert('Save failed.');
      }
    });
  }
  
}
