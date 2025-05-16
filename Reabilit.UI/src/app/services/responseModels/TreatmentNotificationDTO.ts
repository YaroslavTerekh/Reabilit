import { BaseNotification } from "./BaseNotification";

export interface TreatmentNotificationDTO extends BaseNotification {
    recommendations: string
}