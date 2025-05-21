import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserProfileComponent implements OnInit {
  firstName: string = '';
  lastName: string = '';
  lastLogin: Date = new Date();

  ngOnInit(): void {
    this.firstName = localStorage.getItem('firstName') || '';
    this.lastName = localStorage.getItem('lastName') || '';

    const login = localStorage.getItem('lastLogin');
    this.lastLogin = login ? new Date(login) : new Date();
  }
}
