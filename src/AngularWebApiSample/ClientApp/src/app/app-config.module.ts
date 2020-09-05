import { NgModule, InjectionToken } from '@angular/core';
import { environment } from 'src/environments/environment';

export const API_BASE_URL = new InjectionToken<string>('baseUrl');

@NgModule({
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiBaseUrl,
    },
  ],
})
export class AppConfigModule {}
