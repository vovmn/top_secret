using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WSS.API.Domain.Entities;

namespace WSS.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // DbSet'ы для сущностей
        public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
        public DbSet<WorkItem> WorkItems { get; set; } = null!;
        public DbSet<ScheduleChangeRequest> ChangeRequests { get; set; } = null!;
        public DbSet<WorkCompletionReport> CompletionReports { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // === WorkSchedule ===
            modelBuilder.Entity<WorkSchedule>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.ObjectId).IsRequired();
                entity.Property(s => s.Status)
                      .IsRequired()
                      .HasConversion<string>(); // Храним enum как строку
                entity.Property(s => s.Version).IsConcurrencyToken(); // Для оптимистичной блокировки
                entity.HasIndex(s => s.ObjectId).IsUnique(); // Один график на объект
            });

            // === WorkItem ===
            modelBuilder.Entity<WorkItem>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.ScheduleId).IsRequired();
                entity.Property(w => w.Name).IsRequired().HasMaxLength(500);
                entity.Property(w => w.PlannedStartDate).IsRequired();
                entity.Property(w => w.PlannedEndDate).IsRequired();
                entity.Property(w => w.Status)
                      .IsRequired()
                      .HasConversion<string>();
                entity.HasOne<WorkSchedule>()
                      .WithMany(s => s.WorkItems)
                      .HasForeignKey(w => w.ScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // === ScheduleChangeRequest ===
            modelBuilder.Entity<ScheduleChangeRequest>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.ScheduleId).IsRequired();
                entity.Property(r => r.WorkItemId).IsRequired();
                entity.Property(r => r.RequestedBy).IsRequired();
                entity.Property(r => r.NewStartDate).IsRequired();
                entity.Property(r => r.NewEndDate).IsRequired();
                entity.Property(r => r.Reason).IsRequired();
                entity.Property(r => r.Status)
                      .IsRequired()
                      .HasConversion<string>();
                entity.HasOne<WorkItem>()
                      .WithMany() // WorkItem может иметь много запросов на изменение
                      .HasForeignKey(r => r.WorkItemId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(r => r.ScheduleId);
                entity.HasIndex(r => r.Status);
            });

            // === WorkCompletionReport ===
            modelBuilder.Entity<WorkCompletionReport>(entity =>
            {
                entity.HasKey(r => r.WorkItemId); // Один отчёт на одну работу
                entity.Property(r => r.ReportedBy).IsRequired();
                entity.Property(r => r.ReportedAt).IsRequired();
                entity.Property(r => r.Latitude);
                entity.Property(r => r.Longitude);
                entity.Property(r => r.Comments);
                entity.Property(r => r.IsVerified).IsRequired();
                entity.HasOne<WorkItem>()
                  .WithOne() // один к одному
                  .HasForeignKey<WorkCompletionReport>(r => r.WorkItemId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => r.IsVerified);
                // PhotoDocumentIds — хранится как JSON (см. ниже)
            });

            // === Хранение списка фото как JSON (PostgreSQL) ===
            modelBuilder.Entity<WorkCompletionReport>()
                    .Property(r => r.PhotoDocumentIds)
                    .HasColumnType("jsonb");

            base.OnModelCreating(modelBuilder);
        }
    }
}
