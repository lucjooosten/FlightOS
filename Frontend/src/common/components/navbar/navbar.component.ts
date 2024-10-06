import { Component } from '@angular/core';
import { AuthService } from '../../../modules/auth/auth.service'; // Assuming you have an AuthService
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  imports: [CommonModule]
})
export class NavbarComponent {
  constructor(private authService: AuthService) {}

  isLoggedIn(): boolean {
    return this.authService.isAuthenticated(); // Replace with your logic
  }

  login(): void {
    this.authService.login(); // Replace with your logic 
  }

  register(): void {
    this.authService.register(); // Replace with your logic
  }

  logout(): void {
    this.authService.logout(); // Replace with your logic
  }
}
