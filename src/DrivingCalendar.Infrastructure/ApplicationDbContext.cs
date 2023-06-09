﻿using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingCalendar.Infrastructure
{
    internal class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, ApplicationRole, int>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<DrivingLessonEntity> DrivingLessons { get; set; }
        public DbSet<AvailabilityEntity> Availabilities { get; set; }
        public DbSet<StudentInstructorEntity> StudentInstructors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().ToTable("Companies");

            modelBuilder.Entity<Student>(builder =>
            {
                builder.ToTable("Students");
                builder.Ignore(s => s.FullName);
            });

            modelBuilder.Entity<Instructor>(builder =>
            {
                builder.ToTable("Instructors");
                builder.Ignore(i => i.FullName);
            });

            modelBuilder.Entity<Instructor>(builder =>
            {
                builder.HasOne(i => i.Company)
                    .WithMany()
                    .HasForeignKey(i => i.CompanyId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<StudentInstructorEntity>(builder =>
            {
                builder.HasIndex(si => new { si.StudentId, si.InstructorId })
                    .IsUnique();

                builder.HasOne(si => si.Student)
                    .WithMany()
                    .HasForeignKey(si => si.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(si => si.Instructor)
                    .WithMany()
                    .HasForeignKey(si => si.InstructorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
           
            modelBuilder.Entity<DrivingLessonEntity>(builder =>
            {
                builder.HasOne(dl => dl.Student)
                    .WithMany()
                    .HasForeignKey(dl => dl.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(dl => dl.Instructor)
                    .WithMany()
                    .HasForeignKey(dl => dl.InstructorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<AvailabilityEntity>()
                .HasOne<IdentityUser<int>>()
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}
