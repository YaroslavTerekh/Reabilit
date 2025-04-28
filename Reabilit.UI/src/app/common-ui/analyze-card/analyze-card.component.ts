import { Component, Input } from '@angular/core';
import { AnalyzeDTO } from '../../services/responseModels/AnalyzeDTO';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-analyze-card',
  imports: [CommonModule],
  templateUrl: './analyze-card.component.html',
  styleUrl: './analyze-card.component.scss'
})
export class AnalyzeCardComponent {
  @Input()
  public analysis!: AnalyzeDTO;
}
