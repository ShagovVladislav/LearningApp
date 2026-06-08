using LearningApp.api.Storage.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.api.Storage;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<CourseModel> Courses => Set<CourseModel>();
    
    public DbSet<LessonModel> Lessons => Set<LessonModel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CourseModel>(entity =>
        {
            entity.HasKey(course => course.Id);

            entity.Property(course => course.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(course => course.Description)
                .HasMaxLength(1000);
        });

        modelBuilder.Entity<LessonModel>(entity =>
        {
            entity.HasKey(lesson => lesson.Id);

            entity.Property(lesson => lesson.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(lesson => lesson.Content)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne<CourseModel>()
                .WithMany()
                .HasForeignKey(lesson => lesson.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}