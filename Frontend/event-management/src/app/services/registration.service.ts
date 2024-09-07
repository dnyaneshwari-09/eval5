import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Registration } from '../models/registration.model';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private readonly apiUrl = 'https://localhost:7104/api/Registrations';

  constructor(private http: HttpClient) {}

  register(data: Registration): Observable<Registration> {
    return this.http.post<Registration>(this.apiUrl, data);
  }
}
