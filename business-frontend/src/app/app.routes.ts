import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { BlankLayoutComponent } from './layouts/blank-layout/blank-layout.component';
import { FullLayoutComponent } from './layouts/full-layout/full-layout.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  // Public (unauthenticated) routes
  {
    path: '',
    component: BlankLayoutComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      {
        path: 'set-password',
        loadComponent: () => import('./set-password/set-password.component')
          .then(m => m.SetPasswordComponent)
      },
      {
        path: 'reset-password',
        loadComponent: () => import('./reset-password/reset-password.component')
          .then(m => m.ResetPasswordComponent)
      }
    ]
  },

  // Authenticated routes
  {
    path: '',
    component: FullLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/dashboard.component')
          .then(m => m.DashboardComponent)
      },
      {
        path: 'limits-deductibles',
        loadComponent: () => import('./limitsdeductibles/limitsdeductibles.component')
          .then(m => m.BusinessDashboardComponent)
      },
      {
        path: 'liabilities',
        loadComponent: () => import('./liabilities/liabilities.component')
          .then(m => m.LiabilitiesComponent)
      },
      {
        path: 'user-profile',
        loadComponent: () => import('./userprofile/userprofile.component')
          .then(m => m.UserprofileComponent)
      }
    ]
  }
];
