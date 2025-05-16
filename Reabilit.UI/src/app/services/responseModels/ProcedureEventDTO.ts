import { DoctorDTO } from "./DoctorDTO"
import { PatientDTO } from "./PatientDTO"

export interface ProcedureEventDTO {
    id: string,
    title: string,
    description: string,
    result: string,
    status: ProcedureEventStatus,
    startsOn: string,
    patient: PatientDTO | null,
    doctor: DoctorDTO
}

export enum ProcedureEventStatus {
    Planned = 0,
    Cancelled = 1,
    Finished = 2
}
