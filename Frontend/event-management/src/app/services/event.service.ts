import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../models/event.model';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private eventApiUrl = 'https://localhost:7104/api/Events';
  private categoryApiUrl = 'https://localhost:7104/api/Categories';

  constructor(private http: HttpClient) {}

  createEvent(event: Event): Observable<Event> {
    return this.http.post<Event>(this.eventApiUrl, event);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categoryApiUrl);
  }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.eventApiUrl);
  }

  getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.eventApiUrl}/${id}`);
  }
}
