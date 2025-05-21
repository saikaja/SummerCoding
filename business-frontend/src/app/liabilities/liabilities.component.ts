import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LiabilitiesService } from '../services/liabilities.service';
import { LiabilityData } from '../models/liability-data.model';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-liabilities',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './liabilities.component.html',
  styleUrls: ['./liabilities.component.css']
})
export class LiabilitiesComponent implements OnInit {
  liabilityData: LiabilityData[] = [];
  isCollapsed: boolean = false;
  confirmIntegration = false;
  showConfirmation = false;
  integrationMessage: string | null = null;

labelMap: { [key: string]: string } = {
  InjuryLiability: 'LIABILITIESMAP.INJURY',
  PropertyDamageLiability: 'LIABILITIESMAP.PROPERTY',
  ProductLiability: 'LIABILITIESMAP.PRODUCT',
  CyberLiability: 'LIABILITIESMAP.CYBER'
};

  constructor(private liabilityService: LiabilitiesService, private translate: TranslateService) {}
  
  liabilities: any[] = [];
  ngOnInit(): void {
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
      this.liabilityService.getLiabilitiesData().subscribe({
        next: () => {
          this.translate.get('INTEGRATION_SUCCESS').subscribe((msg: string) => {
            this.integrationMessage = 'INTEGRATION_SUCCESS';
          });
          this.loadBusinessData();
        }
      });
    }
  });
}
}
