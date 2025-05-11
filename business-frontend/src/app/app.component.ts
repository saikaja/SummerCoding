import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { SidebarComponent } from './sidebar/sidebar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BusinessDashboardComponent } from './limitsdeductibles/limitsdeductibles.component';
import { LiabilitiesComponent } from './liabilities/liabilities.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, SidebarComponent, DashboardComponent, BusinessDashboardComponent, LiabilitiesComponent],
  template: `
    <nav>
      <a routerLink="/dashboard">Dashboard</a> |
      <a routerLink="/limits">Limits</a> |
      <a routerLink="/liabilities">Liabilities</a>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {}
