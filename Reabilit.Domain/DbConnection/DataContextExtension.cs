using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DbConnection;

public static class DataContextExtension
{
    public static IServiceCollection UseDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DataContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
