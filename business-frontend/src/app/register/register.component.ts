import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
  ValidationErrors
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { RegisterService } from '../services/register.service';
import { CommonModule } from '@angular/common';

function canadianPostalCodeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value?.toUpperCase().trim();
  const regex = /^[A-Z]\d[A-Z][ ]?\d[A-Z]\d$/;
  return regex.test(value) ? null : { invalidPostalCode: true };
}

function minimumAgeValidator(minAge: number) {
  return (control: AbstractControl): ValidationErrors | null => {
    const birthDate = new Date(control.value);
    const today = new Date();

    const age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    const day = today.getDate() - birthDate.getDate();

    const isTooYoung =
      age < minAge || (age === minAge && (m < 0 || (m === 0 && day < 0)));

    return isTooYoung ? { underage: true } : null;
  };
}

function alphanumericNoSpaceValidator(control: AbstractControl): ValidationErrors | null {
  const value: string = control.value;

  if (!value) return null; // Avoid triggering on pristine

  // Check for any whitespace and invalid characters
  const hasWhitespace = /\s/.test(value);
  const isAlphanumeric = /^[a-zA-Z0-9]+$/.test(value);

  if (hasWhitespace || !isAlphanumeric) {
    return { invalidUsername: true };
  }

  return null;
}
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required, alphanumericNoSpaceValidator]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', [Validators.required, minimumAgeValidator(18)]],
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
            this.errorMessage = err.error.message;
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