import { Component } from '@angular/core';
import { EventCardComponent } from "../../common-ui/event-card/event-card.component";

@Component({
  selector: 'app-events-page',
  imports: [EventCardComponent],
  templateUrl: './events-page.component.html',
  styleUrl: './events-page.component.scss'
})
export class EventsPageComponent {

}
