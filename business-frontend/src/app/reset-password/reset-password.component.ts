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

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.resetPasswordForm = this.fb.group({
      userName: ['', Validators.required],
      oneTimePassCode: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const formData = this.resetPasswordForm.value;
      this.registerService.setPassword(formData).subscribe({
        next: () => {
          alert('Password reset successful. You can now log in.');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('Reset failed', err);
          alert('Failed to reset password.');
        }
      });
    } else {
      alert('Please fill out all required fields.');
    }
  }
}
