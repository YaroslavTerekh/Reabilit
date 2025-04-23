import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { EventsPageComponent } from './pages/events-page/events-page.component';
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { MyProfilePageComponent } from './pages/my-profile-page/my-profile-page.component';
import { DoctorsListPageComponent } from './pages/doctors-list-page/doctors-list-page.component';
import { PatientListComponent } from './pages/patient-list-page/patient-list/patient-list.component';
import { DoctorEventsPageComponent } from './pages/doctor-events-page/doctor-events-page/doctor-events-page.component';
import { MyProfilePageDoctorComponent } from './pages/my-profile-page-doctor/my-profile-page-doctor.component';

export const routes: Routes = [
    { path: '', component: MainPageComponent },
    { path: 'events', component: EventsPageComponent },
    { path: 'my-doctor', component: DoctorProfileComponent },
    { path: 'my-profile', component: MyProfilePageComponent },
    { path: 'my-doctor-profile', component: MyProfilePageDoctorComponent },
    { path: 'doctors-list', component: DoctorsListPageComponent },
    { path: 'my-patients', component: PatientListComponent },
    { path: 'doctor-events', component: DoctorEventsPageComponent },
];
