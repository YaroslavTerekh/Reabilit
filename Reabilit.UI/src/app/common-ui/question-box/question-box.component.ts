import { Component } from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { SendMessageToSupport } from '../../services/requestModels/SendMessageToSupport';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-question-box',
  imports: [FormsModule],
  templateUrl: './question-box.component.html',
  styleUrl: './question-box.component.scss'
})
export class QuestionBoxComponent {
  message: string = '';
  isSending: boolean = false;

  constructor(private chatService: ChatService) {}

  sendMessage() {
    if (!this.message.trim()) return;

    this.isSending = true;

    const request: SendMessageToSupport = {
      theme: "Маю декілька питань...",
      message: this.message
    };

    this.chatService.sendMessageToSupport(request)
    .subscribe({
      next: () => {
        this.message = '';
        this.isSending = false;
      }
    });
  }

}
