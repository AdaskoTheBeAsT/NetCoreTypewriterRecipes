import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { environment } from '../environments/environment';
import { API_BASE_URL } from './app-config.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.apiBaseUrl }],
  bootstrap: [AppComponent]
})
export class AppModule { }
