import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

// Import the core and features modules
import { CoreModule } from './core/core.module';
import { FeaturesModule } from './features/features.module';

@NgModule({
  imports: [
    AppComponent,
    BrowserModule,
    CoreModule,
    FeaturesModule,
    RouterModule.forRoot([]),
  ],
  providers: [],
  bootstrap: [],
})
export class AppModule {}
