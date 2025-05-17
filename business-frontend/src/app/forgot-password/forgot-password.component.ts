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

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {  
    this.forgotPasswordForm = this.fb.group({
      userName: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      this.registerService.sendPasswordResetOtp(this.forgotPasswordForm.value).subscribe({
        next: () => {
          alert('OTP sent to your email.');
          this.router.navigate(['/reset-password']); 
        },
        error: (err) => {
          console.error('Failed to send OTP', err);
          alert('Failed to send OTP.');
        }
      });
    }
  }
}
