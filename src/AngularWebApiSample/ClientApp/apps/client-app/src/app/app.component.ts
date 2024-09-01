import { Component, OnInit } from '@angular/core';

import { ImageFormatService } from './image-format.service';

@Component({
  selector: 'atb-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  constructor(private imageFormatService: ImageFormatService) {}

  async ngOnInit() {
    const format = await this.imageFormatService.getSupportedFormat();
    this.imageFormatService.replaceImageUrlsInCSS(format);
  }
}
