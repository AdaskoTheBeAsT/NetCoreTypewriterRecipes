import { InjectionToken, NgModule } from '@angular/core';

export const API_BASE_URL = new InjectionToken<string>('baseUrl');

@NgModule({
  providers: [
    {
      provide: API_BASE_URL,
      useValue: window._env_.API_BASE_URL,
    },
  ],
})
export class AppConfigModule {}
