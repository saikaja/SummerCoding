import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserProfileComponent implements OnInit {
  username: string = '';
  lastLogin: Date = new Date();

  ngOnInit(): void {
    const storedUsername = localStorage.getItem('username');
    const storedLastLogin = localStorage.getItem('lastLogin');

    this.username = storedUsername || 'User';
    this.lastLogin = storedLastLogin ? new Date(storedLastLogin) : new Date();
  }
}
