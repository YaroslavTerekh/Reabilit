import { Component, Input } from '@angular/core';
import { DoctorDTO } from '../../../../services/responseModels/DoctorDTO';

@Component({
  selector: 'app-doctor-card',
  imports: [],
  templateUrl: './doctor-card.component.html',
  styleUrl: './doctor-card.component.scss'
})
export class DoctorCardComponent {
  @Input()
  doctor!: DoctorDTO;
}
