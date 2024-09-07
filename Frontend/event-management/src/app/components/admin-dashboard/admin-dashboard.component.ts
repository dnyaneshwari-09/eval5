import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginService } from '../../services/login.service';
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
  standalone: true,
  imports: [CommonModule],
  providers:[LoginService]
})
export class AdminDashboardComponent {

  constructor(private router: Router) {}

  navigateToAddEvent() {
    this.router.navigate(['/add-event']);
  }

  navigateToEventsList() {
    this.router.navigate(['/event-list']);
  }

  navigateToRegisteredUsersList() {
    this.router.navigate(['/registered-users-list']);
  }
}
