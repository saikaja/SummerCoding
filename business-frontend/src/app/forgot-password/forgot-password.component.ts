import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  forgotPasswordForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {  
    this.forgotPasswordForm = this.fb.group({
      userName: ['', Validators.required]
    });
  }

  onSubmit() {
  if (this.forgotPasswordForm.valid) {
    this.registerService.sendPasswordResetOtp(this.forgotPasswordForm.value).subscribe({
      next: () => {
        this.successMessage = 'OTP sent. Please check your email for the link and OTP.';
        this.errorMessage = '';
      },
      error: (err) => {
        console.error('Failed to send OTP', err);
        this.successMessage = '';

        if (err.status === 400 || err.status === 404) {
          if (typeof err.error === 'string') {
            try {
              const parsed = JSON.parse(err.error);
              this.errorMessage = parsed.message || 'Invalid request. Please check your input.';
            } catch {
              this.errorMessage = err.error;
            }
          } else if (err.error?.message) {
            this.errorMessage = err.error.message;
          } else {
            this.errorMessage = 'Invalid input. Please try again.';
          }
        } else {
          this.errorMessage = 'Failed to send OTP. Please try again later.';
        }
      }
    });
  } else {
    this.errorMessage = 'Please fill out all required fields.';
    this.successMessage = '';
  }
}
}
