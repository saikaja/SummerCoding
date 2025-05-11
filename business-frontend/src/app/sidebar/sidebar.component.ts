import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  template: `
    <nav>
      <a routerLink="/dashboard">Dashboard</a> |
      <a routerLink="/limits">Limits</a> |
      <a routerLink="/liabilities">Liabilities</a>
    </nav>
  `
})
export class SidebarComponent {}
