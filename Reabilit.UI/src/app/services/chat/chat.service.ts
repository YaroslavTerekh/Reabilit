import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../config';
import { SendMessage } from '../requestModels/SendMessage';
import { Observable } from 'rxjs';
import { ChatMessageDTO } from '../responseModels/ChatMessageDTO';
import { ChatDTO } from '../responseModels/ChatDTO';
import { SendMessageToSupport } from '../requestModels/SendMessageToSupport';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public getChat(id: string): Observable<ChatMessageDTO[]> {
    return this.http.get<ChatMessageDTO[]>(`${this.baseUrl}/Chat/get/${id}`);
  }

  public getSupportChat(): Observable<ChatMessageDTO[]> {
    return this.http.get<ChatMessageDTO[]>(`${this.baseUrl}/Chat/support/get`);
  }

  public sendMessage(request: SendMessage): Observable<any> {
    return this.http.post(`${this.baseUrl}/Chat/send`,request);
  }

  public sendMessageToSupport(request: SendMessageToSupport): Observable<any> {
    return this.http.post(`${this.baseUrl}/Chat/support/send`,request);
  }

  public getChatList(): Observable<ChatDTO[]> {
    return this.http.get<ChatDTO[]>(`${this.baseUrl}/Chat/list`);
  }
}
