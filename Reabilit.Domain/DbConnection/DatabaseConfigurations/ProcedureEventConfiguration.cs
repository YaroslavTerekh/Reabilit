using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DbConnection.DatabaseConfigurations;

public class ProcedureEventConfiguration : IEntityTypeConfiguration<ProcedureEvent>
{
    public void Configure(EntityTypeBuilder<ProcedureEvent> builder)
    {
        builder.HasMany(pe => pe.EventsNotifications)
            .WithOne(pen => pen.ProcedureEvent)
            .HasForeignKey(pen => pen.ProcedureEventId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
