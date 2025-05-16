import { UserDTO } from "./UserDTO";

export interface ChatDTO {
    receiverId: string,
    receiver: UserDTO,
    hasNewMessages: boolean
}