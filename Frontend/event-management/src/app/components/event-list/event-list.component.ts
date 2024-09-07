import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
interface Event {
  eventId: string;
  eventName: string;
  ageLimit: number;
  eventDateTime: string;
  eventPrice: number;
  eventCity: string;
  eventAddress: string;
  eventDuration: number;
  eventDetails: string;
  maxCapacity: number;
  categoryId: string;
}

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css'],
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule, NgxPaginationModule]
})
export class EventListComponent implements OnInit {
  events: Event[] = [];
  filteredEvents: Event[] = [];
  searchQuery: string = '';
  sortField: string = 'eventName';
  sortOrder: string = 'asc';
  page: number = 1;
  pageSize: number = 5;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchEvents();
  }

  fetchEvents(): void {
    this.http.get<Event[]>('https://localhost:7104/api/Events').subscribe({
      next: (data) => {
        this.events = data;
        this.filteredEvents = data;
        this.sortEvents();
      },
      error: (err) => console.error('Error fetching events:', err)
    });
  }

  searchEvents(): void {
    this.filteredEvents = this.events.filter(event =>
      event.eventName.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
    this.sortEvents();
  }

  sortEvents(): void {
    this.filteredEvents.sort((a, b) => {
      let fieldA = a[this.sortField as keyof Event];
      let fieldB = b[this.sortField as keyof Event];

      if (typeof fieldA === 'string') {
        fieldA = fieldA.toLowerCase();
        fieldB = (fieldB as string).toLowerCase();
      }

      if (this.sortOrder === 'asc') {
        return fieldA > fieldB ? 1 : -1;
      } else {
        return fieldA < fieldB ? 1 : -1;
      }
    });
  }

  toggleSort(field: string): void {
    if (this.sortField === field) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortOrder = 'asc';
    }
    this.sortEvents();
  }

  downloadCSV(): void {
    const csvData = this.convertToCSV(this.filteredEvents);
    const blob = new Blob([csvData], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'events.csv';
    a.click();
  }

  convertToCSV(data: Event[]): string {
    const headers = ['Event Name', 'Age Limit', 'Event Date', 'Price', 'City', 'Address', 'Duration', 'Details', 'Capacity'];
    const csvRows = data.map(event => [
      event.eventName,
      event.ageLimit,
      new Date(event.eventDateTime).toLocaleString(),
      event.eventPrice,
      event.eventCity,
      event.eventAddress,
      event.eventDuration,
      event.eventDetails,
      event.maxCapacity
    ]);

    const csvContent = [headers.join(','), ...csvRows.map(row => row.join(','))].join('\n');
    return csvContent;
  }
}
