using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Constants;

public static class ErrorMessages
{
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