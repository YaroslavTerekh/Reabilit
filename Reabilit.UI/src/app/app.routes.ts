import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { EventsPageComponent } from './pages/events-page/events-page.component';
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { MyProfilePageComponent } from './pages/my-profile-page/my-profile-page.component';

export const routes: Routes = [
    { path: '', component: MainPageComponent },
    { path: 'events', component: EventsPageComponent },
    { path: 'my-doctor', component: DoctorProfileComponent },
    { path: 'my-profile', component: MyProfilePageComponent }
];
