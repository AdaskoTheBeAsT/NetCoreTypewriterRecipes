import { Component } from '@angular/core';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
})
export class TopNavComponent {
  public show = false;

  toggleCollapse(): void {
    this.show = !this.show;
  }
}
