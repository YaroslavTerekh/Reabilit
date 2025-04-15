using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Constants;

public static class ApplicationPolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string Admins = "Admins";
    public const string Doctors = "Doctors";
    public const string Patients = "Patients";
}
