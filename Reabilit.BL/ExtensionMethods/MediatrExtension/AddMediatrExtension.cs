using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.ExtensionMethods.MediatrExtension;

public static class AddMediatrExtension
{
    public static IServiceCollection AddMediatr(this IServiceCollection services) { 
        return services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(AddMediatrExtension).Assembly));
    }
}
