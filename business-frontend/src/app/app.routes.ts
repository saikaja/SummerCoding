import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BusinessDashboardComponent } from './limitsdeductibles/limitsdeductibles.component';
import { LiabilitiesComponent } from './liabilities/liabilities.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'limits', component: BusinessDashboardComponent },
  { path: 'liabilities', component: LiabilitiesComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' } // Default route
];

