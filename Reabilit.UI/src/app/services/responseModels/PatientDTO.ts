import { AnalyzeDTO } from "./AnalyzeDTO";
import { CityDTO } from "./CityDTO";
import { DoctorDTO } from "./DoctorDTO";
import { UserDTO } from "./UserDTO";

export interface PatientDTO extends UserDTO {
    analyzes: AnalyzeDTO[],
    isActive: boolean,
    doctorId: string | null,
    doctor: DoctorDTO | null,
    cityId: string,
    city: CityDTO
}