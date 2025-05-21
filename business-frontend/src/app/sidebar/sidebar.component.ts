import { Component, ChangeDetectorRef } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  constructor(
    private translate: TranslateService,
    private cdr: ChangeDetectorRef
  ) {
    const lang = localStorage.getItem('lang') || 'en';
    this.translate.setDefaultLang(lang);
    this.translate.use(lang);

    // ✅ This makes the sidebar react when language is toggled
    this.translate.onLangChange.subscribe(() => {
      this.cdr.detectChanges();
    });
  }
}
