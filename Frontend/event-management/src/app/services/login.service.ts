import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

interface LoginResponse {
  token: string; // Assuming the response contains a token field
}

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'https://localhost:7104/api/Account/signin';

  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, credentials).pipe(
      tap(response => {
        // Store the JWT token in local storage
        localStorage.setItem('token', response.token);
      })
    );
  }

  // Optional: Method to retrieve the stored token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Optional: Method to clear the token (e.g., on logout)
  clearToken(): void {
    localStorage.removeItem('token');
  }
}
