import { Component } from '@angular/core';
import { DoctorCardComponent } from "./components/doctor-card/doctor-card.component";

@Component({
  selector: 'app-doctors-list-page',
  imports: [DoctorCardComponent],
  templateUrl: './doctors-list-page.component.html',
  styleUrl: './doctors-list-page.component.scss'
})
export class DoctorsListPageComponent {

}
