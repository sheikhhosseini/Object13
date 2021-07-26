using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Object13.DataLayer.Context;

namespace Object13.Core.Utilites.Extention
{
    public static class ConnectionExtention
    {
        public static IServiceCollection Object13ApplicationDbContext(this IServiceCollection service ,
            IConfiguration configuration)
        {
            service.AddDbContext<Object13Context>(option =>
            {
                var connectionString = "ConnectionStrings:Object13Connection:Development";
                option.UseSqlServer(configuration[connectionString]);
            });
            return service;
        }
    }
}
