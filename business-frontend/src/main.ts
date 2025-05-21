import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

// ✅ Set language early
const lang = localStorage.getItem('lang') || 'en';
localStorage.setItem('lang', lang); // fallback
document.documentElement.lang = lang;

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
