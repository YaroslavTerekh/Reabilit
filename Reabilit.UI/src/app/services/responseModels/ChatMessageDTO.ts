import { UserDTO } from "./UserDTO";

export interface ChatMessageDTO {
    id: string,
    senderId: string,
    receiverId: string,
    receiver: UserDTO,
    messageText: string,
    isRead: boolean,
    isSender: boolean,
    sentAt: Date
}