import { InjectionToken, NgModule } from '@angular/core';

import { environment } from '../environments/environment';

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
