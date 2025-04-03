using MediatR;
using Microsoft.AspNetCore.Identity;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Reabilit.BL.Behaviours.Authentication.RegisterDoctor;

public class RegisterDoctorCommandHandler : IRequestHandler<RegisterDoctorCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public RegisterDoctorCommandHandler(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Handle(RegisterDoctorCommand command, CancellationToken cancellationToken)
    {
        var newUser = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = String.Concat(command.FirstName, command.LastName, command.PhoneNumber),
            Age = command.Age,
            PhoneNumber = command.PhoneNumber,
            PhoneNumberConfirmed = true // Create sms validation if need
        };

        var result = await _userManager.CreateAsync(newUser, command.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, ApplicationRoles.RoleDoctor);

            var doctor = new Doctor
            {
                Degree = command.Degree,
                DoctorClassId = command.DoctorClassId,
                ExperienceInYear = command.ExperienceInYear,
                AppUserId = newUser.Id
            };

            await _context.Doctors.AddAsync(doctor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
