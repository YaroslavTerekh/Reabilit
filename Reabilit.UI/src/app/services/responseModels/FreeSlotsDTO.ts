export interface FreeSlotsDTO {
    day: number,
    slots: SlotHourDTO[]
}

export interface SlotHourDTO {
    time: string,
    isAvailable: boolean
}