import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BusinessDashboardComponent } from './limitsdeductibles/limitsdeductibles.component';
import { LiabilitiesComponent } from './liabilities/liabilities.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'limits', component: BusinessDashboardComponent },
  { path: 'liabilities', component: LiabilitiesComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' } // Default route
];

