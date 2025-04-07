using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Constants;

public static class ErrorMessages
{
    public static string Status404EntityNotFound(EntityType entityType)
    {
        return entityType switch
        {
            EntityType.Analyze => "Дослідження не знайдено",
            EntityType.City => "Міста не знайдено",
            EntityType.Doctor => "Лікаря не знайдено",
            EntityType.DoctorClass => "Спеціальності не знайдено",
            EntityType.Patient => "Пацієнта не знайдено",
            EntityType.ProcedureEvent => "Запису не знайдено",
            _ => "Не знайдено"
        };
    }

    public static string Status404UserNotFound(UserRole role)
    {
        return role switch
        {
            UserRole.Patient => "Пацієнта не знайдено",
            UserRole.Doctor => "Лікаря не знайдено",
            _ => "Користувача не знайдено"
        };           
    }
}

public enum UserRole
{
    Doctor = 0,
    Patient = 1,
    Support = 2,
    Admin = 3,
    Default = 4
}

public enum EntityType
{
    Analyze = 0,
    City = 1,
    Doctor = 2,
    DoctorClass = 3,
    Patient = 4,
    ProcedureEvent = 5
}