import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BusinessService } from '../services/business.service';
import { BusinessData } from '../models/business-data.model';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-business-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './limitsdeductibles.component.html',
  styleUrls: ['./limitsdeductibles.component.css']
})
export class BusinessDashboardComponent implements OnInit {
  businessData: BusinessData[] = [];
  isCollapsed: boolean = false;  // Toggle state
  integrationMessage: string | null = null;

  labelMap: { [key: string]: string } = {
    GeneralAggregateLimit: 'LIMITSDEDUCTIBLES.GENERAL_AGG',
    ProductsOperationsAggregateLimit: 'LIMITSDEDUCTIBLES.PRODUCTS_OPS_AGG',
    EachOccurenceLimit: 'LIMITSDEDUCTIBLES.EACH_OCCURRENCE',
    DamagePremisesLimit: 'LIMITSDEDUCTIBLES.DAMAGE_PREMISES',
    PersonalAdvertisingInjuryLimit: 'LIMITSDEDUCTIBLES.PERSONAL_ADVERTISING',
    MedicalExpenseLimit: 'LIMITSDEDUCTIBLES.MEDICAL_EXPENSE',
    PremOpsBiPdCombinedDeductible: 'LIMITSDEDUCTIBLES.PREMOPS_COMBINED_DED',
    ProductsBiPdCombinedDeductible: 'LIMITSDEDUCTIBLES.PRODUCTS_COMBINED_DED',
    GLAggregateDeductible: 'LIMITSDEDUCTIBLES.GL_AGG_DED'
};

  constructor(private businessService: BusinessService, private translate: TranslateService
  ) {}

  ngOnInit(): void {
  }
  loadBusinessData(): void {
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

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
  }
  confirmIntegration = false;
showConfirmation = false;

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

  this.businessService.setIntegrationStatus(1, true).subscribe({
    next: () => {
      console.log('Integration status updated.');
      this.businessService.getBusinessData().subscribe({
        next: () => {
          this.translate.get('INTEGRATION_SUCCESS').subscribe((msg: string) => {
            this.integrationMessage = msg;
          });
          this.loadBusinessData();
        }
      });
    }
  });
}
}
