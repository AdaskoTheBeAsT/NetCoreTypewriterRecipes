import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'atb-root',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'ClientApp';
}
