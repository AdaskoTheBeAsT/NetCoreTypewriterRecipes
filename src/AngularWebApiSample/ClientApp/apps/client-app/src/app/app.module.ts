import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { environment } from '../environments/environment';
import { API_BASE_URL } from './app-config.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { TopNavComponent } from './ui/top-nav/top-nav.component';

@NgModule({
  declarations: [AppComponent, MainComponent, TopNavComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [{ provide: API_BASE_URL, useValue: window._env_.API_BASE_URL }],
  bootstrap: [AppComponent],
})
export class AppModule {}
