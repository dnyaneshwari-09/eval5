import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'; 
import { AuthGuard } from '../../auth.guard';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,HttpClientModule],
  providers: [LoginService,AuthGuard],
})
export class LoginComponent {
  loginForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private loginService: LoginService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  private decodeToken(token: string): any {
    try {
      const parts = token.split('.');
      if (parts.length !== 3) {
        throw new Error('JWT does not have 3 parts');
      }
      const decoded = atob(parts[1]); // Decode base64-encoded payload
      return JSON.parse(decoded); // Parse the payload as JSON
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.loginService.login(this.loginForm.value).subscribe({
        next: () => {
          this.successMessage = 'Login successful!';
          this.errorMessage = null;

          // Get the token from the service (already stored in local storage)
          const token = this.loginService.getToken();
          if (token) {
            console.log('Token:', token); // Log the token for debugging

            // Decode the token to get the user's info
            const decodedToken = this.decodeToken(token);
            console.log('Decoded Token:', decodedToken); // Log decoded token

            if (decodedToken) {
              // Example: Use `sub` (subject) field or another field like `email` to check user type
              const userId = decodedToken.sub; // `sub` is typically the user ID
              const email = decodedToken.email; // You may have an email field

              // Based on the decoded token info, route accordingly
              if (email === 'admin@admin.com') { // You can check for a specific email for admin
                this.router.navigate(['/admin']);
              } else {
                this.router.navigate(['/event-selection']);
              }
            } else {
              this.errorMessage = 'Error decoding token.';
            }
          }
        },
        error: () => {
          this.successMessage = null;
          this.errorMessage = 'Login failed. Please check your credentials.';
        },
      });
    }
  }
}
