import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/event.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
declare var bootstrap: any;

@Component({
  selector: 'app-event-selection',
  templateUrl: './selection-screen.component.html',
  styleUrls: ['./selection-screen.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  providers: [EventService],
})
export class EventSelectionComponent implements OnInit {
  events: Event[] = [];
  selectedEvent: Event | null = null;

  constructor(
    private eventService: EventService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchEvents();
  }

  fetchEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (events) => (this.events = events),
      error: (err) => console.error('Error fetching events:', err),
    });
  }

  openDetailsModal(event: Event): void {
    this.selectedEvent = event;
    const modalElement = document.getElementById('eventDetailsModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  chooseEvent(event: Event): void {
    localStorage.setItem('selectedEvent', JSON.stringify(event));
    this.router.navigate(['/registration']);
  }
}
