import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CityDTO } from '../../../services/responseModels/CityDTO';
import { ContentService } from '../../../services/content/content.service';
import { RegisterDoctor } from '../../../services/requestModels/RegisterDoctor';
import { AdminService } from '../../../services/admin/admin.service';

@Component({
  selector: 'app-register-patient',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-patient.component.html',
  styleUrl: './register-patient.component.scss'
})
export class RegisterPatientComponent implements OnInit{
  protected form!: FormGroup;
    protected cities: CityDTO[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly contentService: ContentService,
    private readonly adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [null, Validators.min(0)],
      password: ['', Validators.required],
      cityId: ['', Validators.required]
    });

    this.contentService.GetAllCities()
    .subscribe({
      next: cities => {
        this.cities = cities;
      }
    });
  }

  onSubmit() {
    if (this.form.valid) {
      let request: RegisterDoctor = {
        firstName: this.form.get('firstName')?.value,
        lastName: this.form.get('lastName')?.value,
        age: this.form.get('age')?.value,
        phoneNumber: this.form.get('phoneNumber')?.value,
        degree: this.form.get('degree')?.value,
        experienceInYear: this.form.get('experienceInYear')?.value,
        doctorClassId: this.form.get('doctorClassId')?.value,
        password: this.form.get('password')?.value,
      };

      this.adminService.registerDoctor(request)
        .subscribe({
          next: res=> {
            console.log(res);
            
          }
        })
    } else {
      console.log(this.form.errors);
      
    }
  }
}
