import { BaseNotification } from "./BaseNotification";
import { ProcedureEventDTO } from "./ProcedureEventDTO";

export interface ProcedureEventNotificationDTO extends BaseNotification {
    procedureEvent: ProcedureEventDTO
}