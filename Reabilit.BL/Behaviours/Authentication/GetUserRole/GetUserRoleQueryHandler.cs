using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Authentication.GetUserRole;

public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, UserRoleDTO>
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public GetUserRoleQueryHandler(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<UserRoleDTO> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.AppUserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        var role = await _userManager.GetRolesAsync(user);

        if (role is null)
        {
            throw new RequestException("Cannot find a role, server error");
        }

        return new UserRoleDTO
        {
            AppRole = role[0],
            UserId = request.AppUserId
        };
    }
}
