import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { OtpService } from '../services/otp.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-set-password',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './set-password.component.html',
  styleUrl: './set-password.component.css'
})
export class SetPasswordComponent {
  otpForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  passwordStrength: string = '';
  passwordStrengthClass: string = '';

  constructor(private fb: FormBuilder, private otpService: OtpService, private router: Router) {
    this.otpForm = this.fb.group({
      userName: ['', Validators.required],
      oneTimePassCode: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  checkPasswordStrength(): void {
    const password = this.otpForm.get('newPassword')?.value || '';
    let strength = 0;

    if (password.length >= 8) strength++;
    if (/[A-Z]/.test(password)) strength++;
    if (/[0-9]/.test(password)) strength++;
    if (/[^A-Za-z0-9]/.test(password)) strength++;

    if (strength <= 1) {
      this.passwordStrength = 'Weak';
      this.passwordStrengthClass = 'text-danger';
    } else if (strength === 2 || strength === 3) {
      this.passwordStrength = 'Medium';
      this.passwordStrengthClass = 'text-warning';
    } else {
      this.passwordStrength = 'Strong';
      this.passwordStrengthClass = 'text-success';
    }
  }

  onSubmit() {
    if (this.otpForm.valid) {
      const { newPassword, confirmPassword } = this.otpForm.value;

      if (newPassword !== confirmPassword) {
        this.errorMessage = 'Passwords do not match.';
        this.successMessage = '';
        return;
      }

      this.otpService.setPassword(this.otpForm.value).subscribe({
        next: () => {
          this.successMessage = 'Password set successfully. You can now log in.';
          this.errorMessage = '';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (err) => {
          console.error(err);
          this.successMessage = '';
          if (err.status === 400 && err.error?.message) {
            this.errorMessage = err.error.message;
          } else {
            this.errorMessage = 'Failed to set password. Please try again.';
          }
        }
      });
    } else {
      this.errorMessage = 'Please fill out all required fields.';
      this.successMessage = '';
    }
  }
}
