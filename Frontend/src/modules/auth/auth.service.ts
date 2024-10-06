import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isAuthenticated(): boolean {
    return true;
  }

  login(): void {
    // Login logic
  }

  register(): void {
    // Register logic
  }

  logout(): void {
    // Logout logic
  }
}
