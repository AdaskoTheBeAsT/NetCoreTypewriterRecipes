import { NgModule } from '@angular/core';
import { provideHttpClient, withXhr } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { withTypewriterHttpInterceptor } from '@adaskothebeast/typewriter-http-angular';

import { environment } from '../environments/environment';
import { API_BASE_URL } from './app-config.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { TopNavComponent } from './ui/top-nav/top-nav.component';

@NgModule({
  declarations: [AppComponent, MainComponent, TopNavComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [
    provideHttpClient(withXhr(), withTypewriterHttpInterceptor()),
    { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
