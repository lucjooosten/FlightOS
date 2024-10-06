import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Import services and guards
import { AuthService } from './services/auth.service';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  imports: [CommonModule],
  providers: [
    // Add your core services here
    AuthService,
    AuthGuard,
  ],
  declarations: [],
  exports: [],
})
export class CoreModule {}
