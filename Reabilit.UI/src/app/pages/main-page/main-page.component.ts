import { Component } from '@angular/core';
import { AnalyzeCardComponent } from "../../common-ui/analyze-card/analyze-card.component";
import { QuestionBoxComponent } from "../../common-ui/question-box/question-box.component";

@Component({
  selector: 'app-main-page',
  imports: [AnalyzeCardComponent, QuestionBoxComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {

}
