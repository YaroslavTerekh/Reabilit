using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reabilit.Domain.Constants;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DbConnection.DatabaseSeed;

public static class DbSeeding
{
    public async static Task SeedDataContext(DataContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        List<string> ukrainianCities = new List<string>
        {
            "Київ", "Харків", "Одеса", "Дніпро", "Донецьк", "Запоріжжя", "Львів", "Кривий Ріг", "Миколаїв", "Маріуполь",
            "Вінниця", "Полтава", "Чернігів", "Херсон", "Черкаси", "Житомир", "Суми", "Хмельницький", "Чернівці", "Рівне",
            "Івано-Франківськ", "Кропивницький", "Тернопіль", "Луцьк", "Біла Церква", "Краматорськ", "Мелітополь", "Ужгород",
            "Бердянськ", "Слов'янськ", "Сєвєродонецьк", "Нікополь", "Кам'янське", "Олександрія", "Конотоп", "Бровари",
            "Умань", "Мукачево", "Дрогобич", "Хуст", "Червоноград", "Ізмаїл", "Павлоград", "Фастів", "Шепетівка", "Бердичів",
            "Лисичанськ", "Ковель", "Стрий", "Бориспіль", "Алчевськ", "Новомосковськ", "Коростень", "Дубно", "Первомайськ",
            "Трускавець", "Бахмут", "Кременчук", "Вознесенськ", "Канів", "Нова Каховка", "Ірпінь", "Вишгород", "Лозова",
            "Горішні Плавні", "Дубляни", "Ромни", "Золочів", "Нетішин", "Енергодар", "Южне", "Южноукраїнськ", "Чорноморськ",
            "Новоград-Волинський", "Сміла", "Буча", "Вараш", "Коломия", "Прилуки", "Миргород", "Борислав", "Сарни", "Калуш",
            "Сімферополь", "Севастополь", "Євпаторія", "Керч", "Ялта", "Феодосія", "Бахчисарай", "Джанкой", "Алушта", "Саки", "Армянськ", "Судак"
        };

        List<string> rehabCenterDoctors = new List<string> 
        { 
            "Фізіотерапевт", "Реабілітолог", "Невролог", "Травматолог", "Ортопед", "Психолог", 
            "Психотерапевт", "Психіатр", "Логопед", "Кардіолог", "Пульмонолог", "Гастроентеролог", 
            "Ендокринолог", "Масажист", "Терапевт", "Дієтолог", "Анестезіолог", "Інструктор ЛФК", 
            "Ерготерапевт", "Соціальний працівник" 
        };



        if (!await roleManager.Roles.AnyAsync(t => t.Name == ApplicationRoles.RoleAdmin))
            await roleManager.CreateAsync(new AppRole(ApplicationRoles.RoleAdmin));

        if (!await roleManager.Roles.AnyAsync(t => t.Name == ApplicationRoles.RoleSupport))
            await roleManager.CreateAsync(new AppRole(ApplicationRoles.RoleSupport));

        if (!await roleManager.Roles.AnyAsync(t => t.Name == ApplicationRoles.RoleDoctor))
            await roleManager.CreateAsync(new AppRole(ApplicationRoles.RoleDoctor));

        if (!await roleManager.Roles.AnyAsync(t => t.Name == ApplicationRoles.RolePatient))
            await roleManager.CreateAsync(new AppRole(ApplicationRoles.RolePatient));

        var admins = await userManager.GetUsersInRoleAsync(ApplicationRoles.RoleAdmin);

        if (admins.Count < 1)
        {
            var admin = new AppUser
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "+380999999999",
                Email = "Admin@gmail.com",
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(admin, "Pa$$word123!");
            await userManager.AddToRoleAsync(admin, ApplicationRoles.RoleAdmin);
        }

        var cities = await context.Cities.ToListAsync();

        if(cities.Count != ukrainianCities.Count)
        {
            context.RemoveRange(cities);
            
            foreach (var city in ukrainianCities)
            {
                var newCity = new City
                {
                    CityName = city,
                };

                await context.AddAsync(newCity);
            }

            await context.SaveChangesAsync();
        }

        var doctorClasses = await context.DoctorClasses.ToListAsync();

        if (doctorClasses.Count != rehabCenterDoctors.Count)
        {
            context.RemoveRange(doctorClasses);

            foreach (var doctorClass in rehabCenterDoctors)
            {
                var docClassEntity = new DoctorClass
                {
                    ClassName = doctorClass
                };

                await context.DoctorClasses.AddAsync(docClassEntity);
            }

            await context.SaveChangesAsync();
        }
    }
}
