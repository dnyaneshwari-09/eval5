import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LoginService } from './services/login.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(): boolean {
    const token = this.loginService.getToken(); // Retrieve the token from local storage
    if (token) {
      const decodedToken = this.decodeToken(token);
      if (decodedToken && decodedToken.email === 'admin@admin.com') {
        // Change the condition to check for admin role (e.g., check email or another claim in the token)
        return true; // Allow route access for admins
      }
    }
    // If user is not authenticated or not admin, redirect to login
    this.router.navigate(['/login']);
    return false;
  }

  // Function to decode the token
  private decodeToken(token: string): any {
    try {
      const parts = token.split('.');
      if (parts.length !== 3) {
        throw new Error('JWT does not have 3 parts');
      }
      const decoded = atob(parts[1]);
      return JSON.parse(decoded);
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }
}
