import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      addressLine1: ['', Validators.required],
      addressLine2: [''],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;
      this.registerService.sendRegistration(formData).subscribe({
        next: (response) => {
          console.log('OTP sent:', response);
          alert('OTP sent to your email.');
          this.router.navigate(['/set-password']);
        },
        error: (err) => {
          console.error('Registration failed', err);
          if (err.status === 200) {
            alert('OTP sent to your email.');
            this.router.navigate(['/set-password']);
          } else {
            alert('Registration failed.');
          }
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
