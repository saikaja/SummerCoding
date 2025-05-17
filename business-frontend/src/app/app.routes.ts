import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BusinessDashboardComponent } from './limitsdeductibles/limitsdeductibles.component';
import { LiabilitiesComponent } from './liabilities/liabilities.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { SetPasswordComponent } from './set-password/set-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { BlankLayoutComponent } from './layouts/blank-layout/blank-layout.component';
import { FullLayoutComponent } from './layouts/full-layout/full-layout.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: BlankLayoutComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'set-password', loadComponent: () => import('./set-password/set-password.component').then(m => m.SetPasswordComponent) },
      { path: 'reset-password', loadComponent: () => import('./reset-password/reset-password.component').then(m => m.ResetPasswordComponent) }
    ]
  },
  {
  path: '',
  component: FullLayoutComponent,
  children: [
    { path: 'dashboard', loadComponent: () => import('./dashboard/dashboard.component').then(m => m.DashboardComponent), canActivate: [AuthGuard] },
    { path: 'limits-deductibles', loadComponent: () => import('./limitsdeductibles/limitsdeductibles.component').then(m => m.BusinessDashboardComponent), canActivate: [AuthGuard] },
    { path: 'liabilities', loadComponent: () => import('./liabilities/liabilities.component').then(m => m.LiabilitiesComponent), canActivate: [AuthGuard] }
  ]
}
];
