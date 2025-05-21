function canadianPostalCodeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value?.toUpperCase().trim();
  const regex = /^[A-Z]\d[A-Z][ ]?\d[A-Z]\d$/;

  return regex.test(value) ? null : { invalidPostalCode: true };
}

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { RegisterService } from '../services/register.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
}
)

export class RegisterComponent {
  registerForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;

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
      postalCode: ['', [Validators.required, canadianPostalCodeValidator]],
    });
  }

 onSubmit() {
  if (this.registerForm.valid) {
    const formData = this.registerForm.value;

    this.registerService.sendRegistration(formData).subscribe({
      next: (response) => {
        console.log('OTP sent:', response);
        this.successMessage = response.message || 'OTP sent. Please check your email.';
        this.errorMessage = '';
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.successMessage = '';
        if (err.status === 400 && err.error?.message) {
          this.errorMessage = err.error.message; // Shows "Username already exists"
        } else {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      }
    });
  } else {
    this.errorMessage = 'Please fill out all required fields.';
    this.successMessage = '';
  }
}
}