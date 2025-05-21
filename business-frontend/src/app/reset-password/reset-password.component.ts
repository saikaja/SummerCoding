import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetPasswordForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  passwordStrength: string = '';
  passwordStrengthClass: string = '';

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.resetPasswordForm = this.fb.group({
      userName: ['', Validators.required],
      oneTimePassCode: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  checkPasswordStrength(): void {
    const password = this.resetPasswordForm.get('newPassword')?.value || '';
    let strength = 0;

    if (password.length >= 8) strength++;
    if (/[A-Z]/.test(password)) strength++;
    if (/\d/.test(password)) strength++;
    if (/[\W_]/.test(password)) strength++;

    if (strength <= 1) {
      this.passwordStrength = 'Weak';
      this.passwordStrengthClass = 'text-danger';
    } else if (strength <= 3) {
      this.passwordStrength = 'Medium';
      this.passwordStrengthClass = 'text-warning';
    } else {
      this.passwordStrength = 'Strong';
      this.passwordStrengthClass = 'text-success';
    }
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const { newPassword, confirmPassword } = this.resetPasswordForm.value;

      if (newPassword !== confirmPassword) {
        this.errorMessage = 'Passwords do not match.';
        this.successMessage = '';
        return;
      }

      this.registerService.setPassword(this.resetPasswordForm.value).subscribe({
        next: () => {
          this.successMessage = 'Password reset successful. You can now log in.';
          this.errorMessage = '';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (err) => {
          console.error('Reset failed', err);
          this.successMessage = '';

          if (err.status === 400 || err.status === 404 ) {
            if (typeof err.error === 'string') {
              try {
                const parsed = JSON.parse(err.error);
                this.errorMessage = parsed.message || 'Invalid request.';
              } catch {
                this.errorMessage = err.error;
              }
            } else if (err.error?.message) {
              this.errorMessage = err.error.message;
            } else {
              this.errorMessage = 'Invalid input. Please try again.';
            }
          } else {
            this.errorMessage = 'Failed to reset password. Please try again.';
          }
        }
      });
    } else {
      this.errorMessage = 'Please fill out all required fields.';
      this.successMessage = '';
    }
  }
}
