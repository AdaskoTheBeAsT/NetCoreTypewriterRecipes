import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'atb-top-nav',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './top-nav.component.html',
})
export class TopNavComponent {
  public show = false;

  toggleCollapse(): void {
    this.show = !this.show;
  }
}
