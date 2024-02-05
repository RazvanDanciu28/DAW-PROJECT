import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ValidationErrors, ValidatorFn, AbstractControl } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';
import { User, UserDetails } from '../types';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder, private authenticationService: AuthenticationService) {
    this.registrationForm = this.fb.group({
      firstName: new FormControl<string>('', [Validators.required]),
      lastName: new FormControl<string>('', [Validators.required]),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl<string>('', [Validators.required]),
      password: new FormControl<string>('', [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl<string>('', [Validators.required, Validators.minLength(8)]),
    }, { validators: this.confirmPasswordValidator });
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      const userDetails: UserDetails = {
        firstName: this.registrationForm.get('firstName')?.value,
        lastName: this.registrationForm.get('lastName')?.value,
        email: this.registrationForm.get('email')?.value,
        phoneNumber: this.registrationForm.get('phoneNumber')?.value,
        password: this.registrationForm.get('password')?.value,
      };
      this.authenticationService.register(userDetails).subscribe(
        {
          next: response => {
            window.alert('account created successfully!' + response);
            const user: User = {
              email: userDetails.email, 
              password: userDetails.password
            };
            this.authenticationService.login(user).subscribe(
              {
                next: response => {
                  window.alert('logged in successfully!' + response);
                },
                error: err => window.alert('login failed!' + err)
              }
            );
          },
          error: err => window.alert('registration failed!' + err)
        },
      );
    }
    else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  }

  private confirmPasswordValidator: ValidatorFn = ( control: AbstractControl): ValidationErrors | null => {
    return control.value.password === control.value.confirmPassword ? null : { passwordMismatch: true };
  };
}
