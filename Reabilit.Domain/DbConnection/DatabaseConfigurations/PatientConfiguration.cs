using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DbConnection.DatabaseConfigurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasMany(p => p.ProcedureEvents)
            .WithOne(pe => pe.Patient)
            .HasForeignKey(pe => pe.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
