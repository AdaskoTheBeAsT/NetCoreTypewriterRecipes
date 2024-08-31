import { Component, OnInit, Renderer2 } from '@angular/core';

import { ImageFormatService } from './image-format.service';

@Component({
  selector: 'atb-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'ClientApp';

  constructor(
    private imageFormatService: ImageFormatService,
    private renderer: Renderer2,
  ) {}

  async ngOnInit() {
    const format = await this.imageFormatService.getSupportedFormat();
    this.imageFormatService.replaceImageUrlsInCSS(format);
  }
}
