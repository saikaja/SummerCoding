import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BusinessDashboardComponent } from './business-dashboard/business-dashboard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, BusinessDashboardComponent],  
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']  
})
export class AppComponent {
  title = 'business-frontend';
}
