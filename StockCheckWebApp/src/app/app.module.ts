import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { GrowlModule } from 'primeng/primeng';
import { AppComponent } from './app.component';
import { StockComponent } from './stock/stock.component';
import { HttpClientModule } from '@angular/common/http';
import { routing } from './app.routing';

@NgModule({
  declarations: [
    AppComponent,
    StockComponent
  ],
  imports: [
    BrowserModule,
    GrowlModule,
    HttpClientModule,
    routing
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
