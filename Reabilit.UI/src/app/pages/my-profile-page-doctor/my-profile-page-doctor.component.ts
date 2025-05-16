import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DoctorDTO } from '../../services/responseModels/DoctorDTO';
import { DoctorService } from '../../services/doctor/doctor.service';
import { ModifyDoctorInfo } from '../../services/requestModels/ModifyDoctorInfo';
import { AuthService } from '../../services/authorization/auth.service';
import { AddDoctorScheduleRequest } from '../../services/requestModels/AddDoctorScheduleRequest';
import { ToastService } from '../../services/error-handling/toast.service';
import { FreeSlotsDTO } from '../../services/responseModels/FreeSlotsDTO';
import { GetDoctorSlotsRequest } from '../../services/requestModels/GetDoctorSlotsRequest';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-my-profile-page-doctor',
  imports: [CommonModule, ReactiveFormsModule, MatIconModule],
  templateUrl: './my-profile-page-doctor.component.html',
  styleUrl: './my-profile-page-doctor.component.scss',
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class MyProfilePageDoctorComponent implements OnInit {
  public doctorForm!: FormGroup;
  public doctor!: DoctorDTO;
  public scheduleForm!: FormGroup;
  public doctorSchedule: { value: string, id: string | null}[] = [];
  public daysOfWeek = [
    { label: 'Понеділок', value: 1 },
    { label: 'Вівторок', value: 2 },
    { label: 'Середа', value: 3 },
    { label: 'Четвер', value: 4 },
    { label: 'П’ятниця', value: 5 },
    { label: 'Субота', value: 6 },
    { label: 'Неділя', value: 0 }
  ];

  constructor(
    private readonly fb: FormBuilder,
    private readonly doctorService: DoctorService,
    private readonly authService: AuthService,
    private readonly toastService: ToastService
  ) {
    this.doctorForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [null, [Validators.min(0)]],
      degree: ['', Validators.required],
      experienceInYear: [null, [Validators.min(0)]],
      biography: ['']
    });
  }

  ngOnInit(): void {
    this.doctorService.GetMyAccount()
      .subscribe({
        next: res => {
          this.doctor = res;

          this.reloadForm(res);

          let req: GetDoctorSlotsRequest = {
            doctorId: this.doctor.id
          }

          this.doctorService.GetDoctorSchedule(req).subscribe({
            next: (res: FreeSlotsDTO[]) => {
              this.doctorSchedule = this.mapScheduleToStrings(res);
            }
          });
        }
      })

    this.scheduleForm = this.fb.group({
      day: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required]
    });
  }

  public deleteDoctorSchedule(id: string) {
    this.doctorService.DeleteDoctorSchedule(id)
      .subscribe({
        next: res => {
          this.toastService.show("День успішно видалено. Зверніть увагу: записи на цей день не видалено", "info")

          this.doctorSchedule.map(dc => {
            if(dc.id == id) {
              dc.value =`${dc.value.split('—')[0]} — не працює`;
              dc.id = null;
            }
          })
        }
      })
  }

  private mapScheduleToStrings(schedule: FreeSlotsDTO[]): any {
  const daysMap = [
    'Неділя', 'Понеділок', 'Вівторок', 'Середа', 'Четвер', 'Пʼятниця', 'Субота'
  ];

  return schedule.map(day => {
    const dayName = daysMap[day.day];

    if (!day.slots || day.slots.length === 0) {
      return { value:`${dayName} — не працює`, id: null };
    }

    const sortedSlots = [...day.slots].sort((a, b) =>
      a.time.localeCompare(b.time)
    );

    const start = sortedSlots[0].time;
    const end = sortedSlots[sortedSlots.length - 1].time;

    return { value:`${dayName} — ${start} - ${end}`, id: day.day };
  });
}


  onAddSchedule(): void {
    if (this.scheduleForm.valid && this.doctor) {
      const request: AddDoctorScheduleRequest = {
        doctorId: this.doctor.id,
        day: parseInt(this.scheduleForm.value.day),
        startTime: this.scheduleForm.value.startTime,
        endTime: this.scheduleForm.value.endTime
      };

      this.doctorService.AddSchedule(request).subscribe({
        next: () => {
          let req: GetDoctorSlotsRequest = {
            doctorId: this.doctor.id
          }

          this.doctorService.GetDoctorSchedule(req).subscribe({
            next: (res: FreeSlotsDTO[]) => {
              this.doctorSchedule = this.mapScheduleToStrings(res);
            }
          });

          this.scheduleForm.reset();
          this.toastService.show("День успішно додано", "success")
        }
      });
    } else {
      this.scheduleForm.markAllAsTouched();
    }
  }

  onSubmit(): void {
    if (this.doctorForm.valid) {
      let request: ModifyDoctorInfo = {
        firstName: this.doctorForm.get('firstName')?.value,
        lastName: this.doctorForm.get('lastName')?.value,
        age: this.doctorForm.get('age')?.value,
        phoneNumber: this.doctorForm.get('phoneNumber')?.value,
        biography: this.doctorForm.get('biography')?.value,
        degree: this.doctorForm.get('degree')?.value,
        experienceInYear: this.doctorForm.get('experienceInYear')?.value,
      };

      this.doctorService.ModifyDoctorInfo(request)
        .subscribe({
          next: res => {
            this.reloadForm(res);
          }
        })

    } else {
      this.doctorForm.markAllAsTouched();
    }
  }

  reloadForm(res: DoctorDTO): void {
    this.doctorForm = this.fb.group({
      firstName: [res.firstName, Validators.required],
      lastName: [res.lastName, Validators.required],
      phoneNumber: [res.phoneNumber, [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [res.age, [Validators.min(0)]],
      degree: [res.degree, Validators.required],
      experienceInYear: [res.experienceInYear, [Validators.min(0)]],
      biography: [res.biography]
    });
  }

  logout(): void {
    this.authService.logOut();
  }
}