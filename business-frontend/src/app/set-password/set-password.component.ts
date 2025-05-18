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

  constructor(private fb: FormBuilder, private otpService: OtpService, private router: Router) {
    this.otpForm = this.fb.group({
      userName: ['', Validators.required],
      oneTimePassCode: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.otpForm.valid) {
      this.otpService.setPassword(this.otpForm.value).subscribe({
        next: () => {
          this.successMessage = 'Password set successfully. You can now log in.';
          this.router.navigate(['/login']); 
        },
        error: (err) => {
          console.error(err);
          if (err.status === 200) {
            this.successMessage = 'Password set successfully. You can now log in.';
            this.router.navigate(['/login']);
          } else {
            this.errorMessage = 'Failed to set password. Please try again.';
          }
        }
      });
    }
  }
}
