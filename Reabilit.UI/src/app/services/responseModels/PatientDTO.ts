import { CityDTO } from "./CityDTO";
import { DoctorDTO } from "./DoctorDTO";
import { UserDTO } from "./UserDTO";

export interface PatientDTO extends UserDTO {
    doctorId: string | null,
    doctor: DoctorDTO | null,
    cityId: string,
    city: CityDTO
}