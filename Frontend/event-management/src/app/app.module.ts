import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'; // Corrected import for HTTP_INTERCEPTORS


@NgModule({
  declarations: [
    // Add your components here if any
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    // {
    // //   provide: HTTP_INTERCEPTORS,
    // //   //useClass: TokenInterceptor,
    // //   multi: true
    // }
  ],
  bootstrap: [
    // Add your bootstrap component here if any
  ]
})
export class AppModule { }
