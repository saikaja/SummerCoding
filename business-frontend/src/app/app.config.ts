import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import {
  provideHttpClient,
  withInterceptorsFromDi, // <-- Use this instead of withInterceptors
  HTTP_INTERCEPTORS // <-- Import the token
} from '@angular/common/http';
import { routes } from './app.routes';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { AuthInterceptor } from './services/auth.interceptor';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()), // <-- Enable DI-based interceptors
    // Provide the class-based interceptor:
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true // <-- Allows multiple interceptors
    },
    importProvidersFrom(
      TranslateModule.forRoot({
        defaultLanguage: typeof localStorage !== 'undefined'
          ? localStorage.getItem('lang') || 'en'
          : 'en',
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        }
      })
    )
  ]
};