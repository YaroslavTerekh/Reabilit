import { DoctorClassDTO } from "./DoctorClassDTO";
import { UserDTO } from "./UserDTO";

export interface DoctorDTO extends UserDTO {
    biography: string,
    degree: string,
    experienceInYear: number,
    doctorClassId: string,
    doctorClass: DoctorClassDTO
}