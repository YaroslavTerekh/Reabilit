import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { EventsPageComponent } from './pages/events-page/events-page.component';
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { MyProfilePageComponent } from './pages/my-profile-page/my-profile-page.component';
import { DoctorsListPageComponent } from './pages/doctors-list-page/doctors-list-page.component';
import { PatientListComponent } from './pages/patient-list-page/patient-list/patient-list.component';
import { DoctorEventsPageComponent } from './pages/doctor-events-page/doctor-events-page/doctor-events-page.component';
import { MyProfilePageDoctorComponent } from './pages/my-profile-page-doctor/my-profile-page-doctor.component';
import { RegisterDoctorComponent } from './pages/admin/register-doctor/register-doctor.component';
import { AttachDoctorComponent } from './pages/admin/attach-doctor/attach-doctor.component';
import { RegisterPatientComponent } from './pages/admin/register-patient/register-patient.component';
import { MainComponent } from './pages/admin/main/main.component';
import { AddBannerComponent } from './pages/admin/add-banner/add-banner.component';
import { AllBannersComponent } from './pages/admin/all-banners/all-banners.component';
import { AllDoctorsComponent } from './pages/admin/all-doctors/all-doctors.component';
import { AllPatientsComponent } from './pages/admin/all-patients/all-patients.component';

export const routes: Routes = [
    { path: '', component: MainPageComponent },
    { path: 'events', component: EventsPageComponent },
    { path: 'my-doctor', component: DoctorProfileComponent },
    { path: 'my-profile', component: MyProfilePageComponent },
    { path: 'my-doctor-profile', component: MyProfilePageDoctorComponent },
    { path: 'doctors-list/:searchTerm', component: DoctorsListPageComponent },
    { path: 'my-patients', component: PatientListComponent },
    { path: 'doctor-events', component: DoctorEventsPageComponent },
    { path: 'admin', component: MainComponent, children: [
        { path: 'doctor/register', component: RegisterDoctorComponent },
        { path: 'doctor/attach', component: AttachDoctorComponent },
        { path: 'patient/register', component: RegisterPatientComponent },
        { path: 'banner/add', component: AddBannerComponent },
        { path: 'banner/all', component: AllBannersComponent },
        { path: 'doctors/all', component: AllDoctorsComponent },
        { path: 'patients/all', component: AllPatientsComponent },
    ] },
];
