import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImageFormatService {
  imgs = {
    avif: 'data:image/avif;base64,AAAAIGZ0eXBhdmlmAAAAAGF2aWZtaWYxbWlhZk1BMUIAAADybWV0YQAAAAAAAAAoaGRscgAAAAAAAAAAcGljdAAAAAAAAAAAAAAAAGxpYmF2aWYAAAAADnBpdG0AAAAAAAEAAAAeaWxvYwAAAABEAAABAAEAAAABAAABGgAAAB0AAAAoaWluZgAAAAAAAQAAABppbmZlAgAAAAABAABhdjAxQ29sb3IAAAAAamlwcnAAAABLaXBjbwAAABRpc3BlAAAAAAAAAAIAAAACAAAAEHBpeGkAAAAAAwgICAAAAAxhdjFDgQ0MAAAAABNjb2xybmNseAACAAIAAYAAAAAXaXBtYQAAAAAAAAABAAEEAQKDBAAAACVtZGF0EgAKCBgANogQEAwgMg8f8D///8WfhwB8+ErK42A=',
    webp: 'data:image/webp;base64,UklGRhoAAABXRUJQVlA4TA0AAAAvAAAAEAcQERGIiP4HAA==',
  };
  constructor() {}

  private isValidUrl(url: string): boolean {
    try {
      new URL(url);
      return true;
    } catch (e) {
      return false;
    }
  }

  checkWebPSupport(): Promise<boolean> {
    return new Promise((resolve) => {
      const webP = new Image();
      webP.onload = () => resolve(true);
      webP.onerror = (error) => {
        console.error(error);
        return resolve(false);
      };
      webP.src = this.imgs.webp;
    });
  }

  checkAVIFSupport(): Promise<boolean> {
    return new Promise((resolve) => {
      const avif = new Image();
      avif.onload = () => resolve(true);
      avif.onerror = (error) => {
        console.error(error);
        return resolve(false);
      };
      avif.src = this.imgs.avif;
    });
  }

  async getSupportedFormat(): Promise<string> {
    if (await this.checkAVIFSupport()) {
      return 'avif';
    } else if (await this.checkWebPSupport()) {
      return 'webp';
    } else {
      return 'jpeg'; // Default fallback
    }
  }

  replaceImageUrlsInCSS(format: string): void {
    const cssRules = Array.from(document.styleSheets)
      .flatMap((styleSheet: CSSStyleSheet) =>
        Array.from(styleSheet.rules || []),
      )
      .filter((rule: CSSRule) => rule instanceof CSSStyleRule);

    const propertiesToCheck = [
      'backgroundImage',
      'borderImageSource',
      'listStyleImage',
      'content',
      'cursor',
      'maskImage',
      'clipPath',
      'filter',
      'shapeOutside',
    ];

    cssRules.forEach((rule: CSSStyleRule) => {
      propertiesToCheck.forEach((property) => {
        const value = rule.style[property as any];
        if (value && value.includes('url(')) {
          const urlMatch = value.match(/url\(["']?(.*?)["']?\)/);
          if (urlMatch && urlMatch[1]) {
            let url = urlMatch[1];
            if (
              url.endsWith('.png') ||
              url.endsWith('.jpg') ||
              url.endsWith('.jpeg')
            ) {
              // const newUrl = url.replace(
              //   /\.(png|jpg|jpeg)$/,
              //   `.${format}`,
              // );
              const newUrl = url
                .replace(/([^\/]+)(\.[^\.]+)$/, '$1.real$2')
                .replace(/\.(png|jpg|jpeg)$/, `.${format}`);
              rule.style[property as any] = value.replace(url, newUrl);
            }
          }
        }
      });
    });
  }
}
