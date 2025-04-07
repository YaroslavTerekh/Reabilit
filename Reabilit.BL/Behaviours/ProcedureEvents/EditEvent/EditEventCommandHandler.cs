using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.EditEvent;

public class EditEventCommandHandler : IRequestHandler<EditEventCommand>
{
    private readonly DataContext _context;

    public EditEventCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(EditEventCommand request, CancellationToken cancellationToken)
    {
        var eventToEdit = await _context.ProcedureEvents.FirstOrDefaultAsync(pe => pe.Id == request.EventId, cancellationToken);

        if (eventToEdit is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        eventToEdit.Title = request.Title;
        eventToEdit.Status = eventToEdit.Status;
        eventToEdit.Description = eventToEdit.Description;
        eventToEdit.DoctorId = eventToEdit.DoctorId;
        eventToEdit.PatientId = eventToEdit.PatientId;

        await _context.SaveChangesAsync(cancellationToken);
    } 
}
