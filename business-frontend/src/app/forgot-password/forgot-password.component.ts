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
          this.successMessage = 'OTP sent. Please check your email for the link and OTP.'
        },
        error: (err) => {
          console.error('Failed to send OTP', err);
          this.errorMessage = 'Failed to send OTP.';
        }
      });
    }
  }
}
