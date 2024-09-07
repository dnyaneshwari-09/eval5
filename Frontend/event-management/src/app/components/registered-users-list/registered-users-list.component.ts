import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
//import { saveAs } from 'file-saver'; // Correct import for file-saver

@Component({
  selector: 'app-registered-users-list',
  templateUrl: './registered-users-list.component.html',
  styleUrls: ['./registered-users-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class RegisteredUsersListComponent implements OnInit {
  registeredUsers: any[] = [];
  filteredUsers: any[] = [];
  searchQuery: string = '';
  sortField: string = 'name';
  sortDirection: string = 'asc';
  currentPage: number = 1;
  pageSize: number = 5;
  totalPages: number = 1;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchRegisteredUsers();
  }

  // Fetch data from API
  fetchRegisteredUsers(): void {
    this.http.get('https://localhost:7104/api/Registrations').subscribe((data: any) => {
      this.registeredUsers = data;
      this.filteredUsers = data;
      this.calculateTotalPages();
    });
  }

  // Search by city or any field
  onSearch(): void {
    this.filteredUsers = this.registeredUsers.filter(user =>
      user.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      user.email.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      user.mobileNo.toString().includes(this.searchQuery) ||
      user.age.toString().includes(this.searchQuery) ||
      user.gender.toLowerCase().includes(this.searchQuery)
    );
    this.calculateTotalPages();
  }

  // Sort by any field
  onSort(field: string): void {
    this.sortField = field;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.filteredUsers.sort((a, b) => {
      const comparison = a[field] > b[field] ? 1 : -1;
      return this.sortDirection === 'asc' ? comparison : -comparison;
    });
  }

  // Pagination
  onPageChange(page: number): void {
    this.currentPage = page;
  }

  calculateTotalPages(): void {
    this.totalPages = Math.ceil(this.filteredUsers.length / this.pageSize);
  }

  // Export to CSV
  downloadCSV(): void {
    const csvData = this.filteredUsers.map(user => ({
      Name: user.name,
      Email: user.email,
      Mobile: user.mobileNo,
      Age: user.age,
      Gender: user.gender
    }));

    const csvString = this.convertToCSV(csvData);
    const blob = new Blob([csvString], { type: 'text/csv;charset=utf-8;' });
   // saveAs(blob, 'registered-users.csv');
  }

  convertToCSV(objArray: { Name: string; Email: string; Mobile: string; Age: number; Gender: string }[]): string {
    const array = typeof objArray !== 'object' ? JSON.parse(objArray) : objArray;
    let str = '';
    const header = Object.keys(array[0]).join(',');
    str += header + '\r\n';

    array.forEach((item: { Name: string; Email: string; Mobile: string; Age: number; Gender: string }) => {
      let line = '';
      for (const key in item) {
        if (line !== '') line += ',';
       // line += item[key];
      }
      str += line + '\r\n';
    });
    return str;
  }
}
