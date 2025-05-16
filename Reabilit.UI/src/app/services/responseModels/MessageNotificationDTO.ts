import { BaseNotification } from "./BaseNotification";
import { ChatMessageDTO } from "./ChatMessageDTO";

export interface MessageNotificationDTO extends BaseNotification {
    chatMessage: ChatMessageDTO
}