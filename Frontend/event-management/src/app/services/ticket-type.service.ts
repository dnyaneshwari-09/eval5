import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketType } from '../models/ticket-type.model';

@Injectable({
  providedIn: 'root'
})
export class TicketTypeService {
  private readonly apiUrl = 'https://localhost:7104/api/TicketTypes';

  constructor(private http: HttpClient) {}

  getTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.apiUrl);
  }
}
