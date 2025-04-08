using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DbConnection.DatabaseConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasMany(d => d.ProcedureEvents)
            .WithOne(pe => pe.Doctor)
            .HasForeignKey(pe => pe.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
