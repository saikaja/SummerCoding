import { Component, ChangeDetectorRef } from '@angular/core';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [TranslateModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(
    private translate: TranslateService,
    private cdr: ChangeDetectorRef
  ) {
    const savedLang = localStorage.getItem('lang') || 'en';
    this.translate.setDefaultLang(savedLang);
    this.translate.use(savedLang);

    // ✅ Force re-render when language changes
    this.translate.onLangChange.subscribe(() => {
      this.cdr.detectChanges();
    });
  }
}
