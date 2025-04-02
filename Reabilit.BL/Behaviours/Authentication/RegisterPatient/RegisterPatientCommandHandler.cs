using MediatR;
using Microsoft.AspNetCore.Identity;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Authentication.RegisterPatient;

public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public RegisterPatientCommandHandler(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Handle(RegisterPatientCommand command, CancellationToken cancellationToken)
    {
        var newUser = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Age = command.Age,
            PhoneNumber = command.PhoneNumber,
            PhoneNumberConfirmed = true // Create sms-validation if need
        };

        var result = await _userManager.CreateAsync(newUser, command.Password);

        if (result.Succeeded)
        {
            var patient = new Patient
            {
                AppUserId = newUser.Id,
                CityId = command.CityId,
            };

            await _context.Patients.AddAsync(patient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
