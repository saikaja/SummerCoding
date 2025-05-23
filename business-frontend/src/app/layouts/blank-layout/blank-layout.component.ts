import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '../../header/header.component';

@Component({
  selector: 'app-blank-layout',
  standalone: true,
  imports: [HeaderComponent, RouterModule],
  templateUrl: './blank-layout.component.html',
  styleUrl: './blank-layout.component.css'
})
export class BlankLayoutComponent {}
