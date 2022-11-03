﻿using DrivingCalendar.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DrivingCalendar.Infrastructure
{
    public static class DrivingCalendarBuilderExtensions
    {
        public static DrivingCalendarBuilder PersistInSqlServer(
            this DrivingCalendarBuilder builder,
            string connectionString)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return builder;
        }

        public static async Task RunMigrationsAsync(IServiceProvider provider)
        {
            using ApplicationDbContext applicationDbContext = provider.GetRequiredService<ApplicationDbContext>();
            await applicationDbContext.Database.MigrateAsync();
        }
    }
}