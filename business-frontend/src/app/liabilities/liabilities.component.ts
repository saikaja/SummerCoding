import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LiabilitiesService } from '../services/liabilities.service';
import { LiabilityData } from '../models/liability-data.model';

@Component({
  selector: 'app-liabilities',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './liabilities.component.html',
  styleUrls: ['./liabilities.component.css']
})
export class LiabilitiesComponent implements OnInit {
  liabilityData: LiabilityData[] = [];
  isCollapsed: boolean = false;
  confirmIntegration = false;
  showConfirmation = false;

   labelMap: { [key: string]: string } = {
    InjuryLiability: 'Injury Liability',
    PropertyDamageLiability: 'Property Damage Liability',
    ProductLiability: 'Product Liability',
    CyberLiability: 'Cyber Liability'
  };

  constructor(private liabilityService: LiabilitiesService) {}
  
  liabilities: any[] = [];
  ngOnInit(): void {
    this.loadBusinessData();
  }

  loadBusinessData(): void {
    this.liabilityService.getLiabilitiesData().subscribe(data => {
      this.liabilityData = data;
      console.log(this.liabilities);
    });
  }

  
  save(): void {
    this.liabilityService.saveLiabilitiesData(this.liabilityData).subscribe({
      next: () => alert('Data saved successfully.'),
      error: (error) => {
        console.error('Save failed:', error);
        alert('Save failed.');
      }
    });
  }

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  onIntegrationCheck() {
    if (this.confirmIntegration) {
      this.showConfirmation = true;
    }
  }

  cancelIntegration() {
    this.confirmIntegration = false;
    this.showConfirmation = false;
  }

  confirmIntegrationAction() {
    this.showConfirmation = false;

    this.liabilityService.setIntegrationStatus(2, true).subscribe({
      next: () => {
        this.liabilityService.fetchAndSaveFromIntegration().subscribe({
          next: () => {
            alert('Data integrated successfully.');
            this.loadBusinessData();
          }
        });
      }
    });
  }
}
