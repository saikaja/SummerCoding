import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
  ValidationErrors
} from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TranslateService, TranslateModule } from '@ngx-translate/core';
import { RegisterService } from '../services/register.service';

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

    const isTooYoung = age < minAge || (age === minAge && (m < 0 || (m === 0 && day < 0)));

    return isTooYoung ? { underage: true } : null;
  };
}

function alphanumericNoSpaceValidator(control: AbstractControl): ValidationErrors | null {
  const value: string = control.value;
  if (!value) return null;
  const hasWhitespace = /\s/.test(value);
  const isAlphanumeric = /^[a-zA-Z0-9]+$/.test(value);
  return hasWhitespace || !isAlphanumeric ? { invalidUsername: true } : null;
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    CommonModule,
    TranslateModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService,
    private router: Router,
    private translate: TranslateService,
    private cdr: ChangeDetectorRef
  ) {
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

  ngOnInit(): void {
    this.translate.onLangChange.subscribe(() => {
      this.cdr.detectChanges();
    });
  }

  get translated(): boolean {
    return true;
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;

      this.registerService.sendRegistration(formData).subscribe({
        next: (response) => {
          this.successMessage = response.message || this.translate.instant('REGISTER.OTP_SENT');
          this.errorMessage = '';
        },
        error: (err) => {
          this.successMessage = '';
          if (err.status === 400 && err.error?.message) {
            this.errorMessage = err.error.message;
          } else {
            this.errorMessage = this.translate.instant('REGISTER.REGISTRATION_FAILED');
          }
        }
      });
    } else {
      this.errorMessage = this.translate.instant('REGISTER.ERROR_REQUIRED');
      this.successMessage = '';
    }
  }
}