import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { OtpService } from '../services/otp.service';

@Component({
  selector: 'app-set-password',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './set-password.component.html',
  styleUrl: './set-password.component.css'
})
export class SetPasswordComponent {
  otpForm: FormGroup;

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
          alert('Password set successfully. You can now log in.');
          this.router.navigate(['/login']); 
        },
        error: (err) => {
          console.error(err);
          alert('Failed to set password. Please try again.');
          if (err.status === 200) {
            alert('Password set successfully. You can now log in.');
            this.router.navigate(['/login']);
          } else {
            alert('Failed to set password. Please try again.');
          }
        }
      });
    }
  }
}
