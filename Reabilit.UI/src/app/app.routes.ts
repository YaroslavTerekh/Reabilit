import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { EventsPageComponent } from './pages/events-page/events-page.component';
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { MyProfilePageComponent } from './pages/my-profile-page/my-profile-page.component';
import { DoctorsListPageComponent } from './pages/doctors-list-page/doctors-list-page.component';

export const routes: Routes = [
    { path: '', component: MainPageComponent },
    { path: 'events', component: EventsPageComponent },
    { path: 'my-doctor', component: DoctorProfileComponent },
    { path: 'my-profile', component: MyProfilePageComponent },
    { path: 'doctors-list', component: DoctorsListPageComponent },
];
