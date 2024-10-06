import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing'; // Import your routing module

// Import feature modules
import { AuthModule } from '../modules/auth/auth.module';
import { UserManagementModule } from '../modules/user-management/user-management.module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    // AppComponent,
    // Declare any other components here if necessary
  ],
  imports: [
    AppComponent,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    AuthModule, // Include the Auth module
    UserManagementModule // Include the User Management module
  ],
  providers: [],
  //bootstrap: [AppComponent],
})
export class AppModule { }
