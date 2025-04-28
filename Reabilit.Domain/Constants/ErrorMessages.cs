using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Constants;

public static class ErrorMessages
{
    public const string WrongPassword = "Неправильний пароль";
    public const string PhoneNumberExists = "Користувач із таким номером телефону вже існує";
    public const string Unauthorized401 = "Схоже, Ви не увійшли в акаунт. Увійдіть, або спробуйте ще раз";
    public const string Unauthorized403 = "На жаль, Ви не маєте доступу до цієї функції";
    public const string DoctorAbsent = "В цей день лікар не працює";
    public const string DateBusy = "Ця дата вже зайнята";
    public const string SlotAbsent = "В цей час лікар зайнятий або вже не працюватиме";

    public const string Error500 = "Сталась невідома помилка. Спробуйте ще раз";

    public static string Status404EntityNotFound(EntityType entityType)
    {
        return entityType switch
        {
            EntityType.Analyze => "Дослідження не знайдено",
            EntityType.City => "Міста не знайдено",
            EntityType.Doctor => "Лікаря не знайдено",
            EntityType.DoctorClass => "Спеціальності не знайдено",
            EntityType.Patient => "Пацієнта не знайдено",
            EntityType.Banner => "Баннер не знайдено",
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
    ProcedureEvent = 5,
    Banner = 6
}