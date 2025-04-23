using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;

namespace Reabilit.BL.Behaviours.Authentication.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthToken>
{
    private readonly DataContext _context;
    private readonly IJWTService _jwtService;
    private readonly UserManager<AppUser> _userManager;

    public LoginCommandHandler(DataContext context, UserManager<AppUser> userManager, IJWTService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public async Task<AuthToken> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

        if(user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        //var result = await _userManager.CheckPasswordAsync(user, request.Password);

        //if(!result)
        //{
        //    throw new RequestException(ErrorMessages.WrongPassword);
        //}

        return _jwtService.GenerateJWT(user, (await _userManager.GetRolesAsync(user)).ToArray());
    }
}
