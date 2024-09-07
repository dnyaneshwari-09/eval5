import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from '../../services/registration.service';
import { TicketTypeService } from '../../services/ticket-type.service';
import { TicketType } from '../../models/ticket-type.model';
import { Registration } from '../../models/registration.model';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import * as jwtDecode from 'jwt-decode';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  standalone: true,
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule],
  providers: [RegistrationService, TicketTypeService]
})
export class RegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  ticketTypes: TicketType[] = [];
  genders = ['Male', 'Female', 'Other'];
  message: string | null = null;
  isSuccess: boolean = false;

  constructor(
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private ticketTypeService: TicketTypeService,
    private router: Router
  ) {
    this.registrationForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(1)]],
      gender: ['', Validators.required],
      ticketType: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadTicketTypes();
    this.setInitialValues();
  }

  private loadTicketTypes(): void {
    this.ticketTypeService.getTicketTypes().subscribe({
      next: (types) => this.ticketTypes = types,
      error: (err) => console.error('Error fetching ticket types:', err)
    });
  }

  private setInitialValues(): void {
    const selectedEvent = JSON.parse(localStorage.getItem('selectedEvent') || '{}');
    if (!selectedEvent) {
      this.router.navigate(['/events']);
    }
  }

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

  private getUserIdFromToken(): string | null {
    const token = localStorage.getItem('token'); // Replace with your actual token retrieval logic
    if (token) {
      try {
        const decoded: any = this.decodeToken(token);
        console.log("id",decoded.sub)
        return decoded.sub|| null; // Adjust based on your token structure
      } catch (error) {
        console.error('Error decoding token:', error);
        return null;
      }
    }
    return null;
  }

  onSubmit(): void {
    if (this.registrationForm.invalid) {
      return;
    }
  
    // Get the user ID from the token and log the decoded token
    const userId = this.getUserIdFromToken();
    const selectedEvent = JSON.parse(localStorage.getItem('selectedEvent') || '{}');
    const token = localStorage.getItem('userToken'); // Assuming the token is stored under this key
  
    if (token) {
      console.log('Token:', token); // Log the token to ensure it's being retrieved correctly
  
      try {
        const decodedToken = this.decodeToken(token);
        console.log('Decoded Token:', decodedToken); // Print the decoded token to the console
      } catch (error) {
        console.error('Error decoding token:', error);
      }
    } else {
      console.log('No token found in local storage.');
    }
  
    const registrationData: Registration = {
      userId: userId || '', // Ensure userId is not null
      eventId: selectedEvent.eventId,
      name: this.registrationForm.value.name,
      email: this.registrationForm.value.email,
      mobileNo: this.registrationForm.value.phone,
      age: this.registrationForm.value.age,
      gender: this.registrationForm.value.gender,
      ticketTypeId: this.registrationForm.value.ticketType
    };
  
    this.registrationService.register(registrationData).subscribe({
      next: () => {
        this.message = 'Registration successful!';
        this.isSuccess = true;
        setTimeout(() => this.router.navigate(['/events']), 2000); // Redirect after 2 seconds
      },
      error: (err) => {
        this.message = 'Registration failed. Please try again.';
        this.isSuccess = false;
        console.error('Error during registration:', err);
      }
    });
  }
  
  
}
