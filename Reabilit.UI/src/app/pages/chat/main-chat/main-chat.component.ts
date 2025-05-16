import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../../services/authorization/auth.service';
import { ChatMessageDTO } from '../../../services/responseModels/ChatMessageDTO';
import { ChatService } from '../../../services/chat/chat.service';
import { SendMessage } from '../../../services/requestModels/SendMessage';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatDTO } from '../../../services/responseModels/ChatDTO';
import { SignalrService } from '../../../services/chat/signalr.service';
import { SendMessageToSupport } from '../../../services/requestModels/SendMessageToSupport';

@Component({
  selector: 'app-main-chat',
  imports: [CommonModule, FormsModule],
  templateUrl: './main-chat.component.html',
  styleUrl: './main-chat.component.scss'
})
export class MainChatComponent implements OnInit, AfterViewInit {
  @ViewChild('chatBody') chatBody!: ElementRef;
  chats: ChatDTO[] = [];
  messages: ChatMessageDTO[] = [];
  selectedUserId: string | null = null;
  selectedUserName: string = '';
  messageText: string = '';
  searchText: string = '';
  currentRole: string = '';

  constructor(
    private chatService: ChatService,
    private signalrService: SignalrService,
    private authService: AuthService
  ) {}

  ngAfterViewInit(): void {
    this.scrollToBottom(); 
  }

  ngOnInit(): void {
    this.authService.$currentRole
      .subscribe({
        next: res => {
          if(res == 'Support'){
            this.loadChatList();
            this.currentRole = res;
          }

          if(res == "Patient") {
              this.chatService.getSupportChat()
                .subscribe({
                  next: res => {
                    this.messages = res;
                    setTimeout(() => this.scrollToBottom(), 100);
                  }
                })
          }
        }
      })

    this.signalrService.$receiveChatMessageSubject
      .subscribe({
        next: res => {
          if(res.senderId == this.selectedUserId || this.selectedUserId == null) {
            this.messages.push(res);
          }
          setTimeout(() => this.scrollToBottom(), 100)
        }
      })
  }

  scrollToBottom() {
    try {
      this.chatBody.nativeElement.scrollTop = this.chatBody.nativeElement.scrollHeight;
    } catch (err) {
      console.warn('Scroll error:', err);
    }
  }

  loadChatList(): void {
    this.chatService.getChatList().subscribe((res) => {
      this.chats = res;
    });
  }

  selectUser(userId: string): void {
    this.selectedUserId = userId;
    const selectedUser = this.chats.find(c => c.receiverId === userId);
    this.selectedUserName = `${selectedUser?.receiver?.firstName ?? ''} ${selectedUser?.receiver?.lastName ?? ''}`;

    this.chatService.getChat(userId).subscribe((res) => {
      this.messages = res;
      setTimeout(() => this.scrollToBottom(), 100);
    });
  }

  sendMessage(): void {
    if (!this.messageText.trim()) return;

    if(this.selectedUserId != null) {
      const msg: SendMessage = {
        receiverId: this.selectedUserId,
        message: this.messageText
      };
  
      this.chatService.sendMessage(msg).subscribe(() => {
        this.messages.push({ messageText: this.messageText, isSender: true } as ChatMessageDTO);
        this.messageText = '';
        setTimeout(() => this.scrollToBottom(), 100)
      });
    } 
    
    if(this.selectedUserId == null ){
      const msg: SendMessageToSupport = {
        theme: this.messageText,
        message: this.messageText
      };
  
      this.chatService.sendMessageToSupport(msg).subscribe(() => {
        this.messages.push({ messageText: this.messageText, isSender: true } as ChatMessageDTO);
        this.messageText = '';
        setTimeout(() => this.scrollToBottom(), 100)
      });
    }
  }

  filteredChats(): ChatDTO[] {
    const text = this.searchText.toLowerCase();
    return this.chats.filter(c =>
      (`${c.receiver?.firstName ?? ''} ${c.receiver?.lastName ?? ''}`).toLowerCase().includes(text)
    );
  }
}