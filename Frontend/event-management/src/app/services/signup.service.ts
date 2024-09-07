import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterDTO } from '../models/register.model';

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  private apiUrl = 'https://localhost:7104/api/Account/signup';

  constructor(private http: HttpClient) {}

  signup(registerDto: RegisterDTO): Observable<any> {
    return this.http.post<any>(this.apiUrl, registerDto);
  }
}
