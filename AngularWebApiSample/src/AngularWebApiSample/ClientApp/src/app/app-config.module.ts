import { NgModule, InjectionToken } from '@angular/core';

export let API_BASE_URL = new InjectionToken<string>('baseUrl');

@NgModule({
    providers: [{
        provide: API_BASE_URL,
        useValue: ''
    }]
})
export class AppConfigModule { }
