import { UserDTO } from "./UserDTO";

export interface BaseNotification {
    id: string,
    message: string,
    isRead: boolean,
    appUser: UserDTO
}