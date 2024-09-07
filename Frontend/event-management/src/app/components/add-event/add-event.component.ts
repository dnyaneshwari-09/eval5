import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/event.model';
import { Category } from '../../models/category.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-event-form',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css'],
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  providers: [EventService],
})
export class EventFormComponent implements OnInit {
  eventForm: FormGroup;
  categories: Category[] = [];
  cities: string[] = ['Pune', 'Mumbai', 'Nagpur', 'satara', 'Banglore']; // List of cities
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private eventService: EventService,
    private router: Router
  ) {
    this.eventForm = this.fb.group({
      eventName: ['', [Validators.required, Validators.maxLength(100)]],
      eventDateTime: ['', [Validators.required, this.dateValidator]],
      eventPrice: ['', [Validators.required, Validators.min(0)]],
      eventAddress: ['', [Validators.required, Validators.maxLength(200)]],
      eventDuration: ['', [Validators.required, Validators.min(0)]],
      eventDetails: ['', [Validators.maxLength(500)]],
      maxCapacity: ['', [Validators.required, Validators.min(1)]],
      categoryId: ['', Validators.required],

      // Newly added fields
      ageLimit: ['', [Validators.required, Validators.min(0)]], // Age Limit must be non-negative
      eventCity: ['', Validators.required], // Event City from dropdown
    });
  }

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.eventService.getCategories().subscribe({
      next: (categories) => (this.categories = categories),
      error: (err) => console.error(err),
    });
  }

  dateValidator(control: any) {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    return selectedDate > currentDate ? null : { invalidDate: true };
  }

  onSubmit(): void {
    if (this.eventForm.valid) {
      const newEvent: Event = this.eventForm.value;
      this.eventService.createEvent(newEvent).subscribe({
        next: (event) => {
          this.successMessage = 'Event created successfully!';
          this.eventForm.reset();
          setTimeout(() => {
            this.successMessage = '';
            this.router.navigate(['/events']);
          }, 3000); // Redirect after 3 seconds
        },
        error: (err) => console.error(err),
      });
    }
  }
}
