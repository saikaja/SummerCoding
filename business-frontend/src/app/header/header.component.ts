import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { TranslateService, TranslateModule } from '@ngx-translate/core';
import { CommonModule, DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [TranslateModule, CommonModule, HttpClientModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [DatePipe]
})
export class HeaderComponent {
  showProfile = false;
  isLoggedIn$: Observable<boolean>;
  hasCheckedLoginState = false;

  firstName = '';
  lastName = '';
  lastLogin: Date = new Date();

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  constructor(
    private authService: AuthService,
    private translate: TranslateService,
    private router: Router
  ) {
    this.isLoggedIn$ = this.authService.isLoggedIn$;

    const savedLang = localStorage.getItem('lang') || 'en';
    this.translate.use(savedLang);

    this.isLoggedIn$.subscribe((state) => {
      this.hasCheckedLoginState = true;

      if (state) {
        this.firstName = localStorage.getItem('firstName') || '';
        this.lastName = localStorage.getItem('lastName') || '';
        this.lastLogin = new Date(localStorage.getItem('lastLogin') || new Date());
      } else {
        this.firstName = '';
        this.lastName = '';
        this.lastLogin = new Date();
        this.showProfile = false;
      }
    });
  }

  switchLanguage(lang: string): void {
    this.translate.use(lang);
    localStorage.setItem('lang', lang);
  }

  logout(): void {
    this.authService.logout();
  }

  goToProfile(): void {
    this.router.navigate(['/user-profile']);
  }

  toggleProfileDropdown(): void {
    this.showProfile = !this.showProfile;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    const clickedInside =
      target.closest('.profile-dropdown') || target.closest('.profile-button');
    if (!clickedInside) {
      this.showProfile = false;
    }
  }
}