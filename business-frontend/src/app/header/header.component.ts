import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { TranslateService, TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [TranslateModule]
})
export class HeaderComponent {
  constructor(
    private authService: AuthService,
    private translate: TranslateService
  ) {
    const savedLang = localStorage.getItem('lang') || 'en';
    this.translate.use(savedLang); // No setDefaultLang here
  }

  logout(): void {
    this.authService.logout();
  }

  switchLanguage(lang: string): void {
    this.translate.use(lang);
    localStorage.setItem('lang', lang);
  }
}
