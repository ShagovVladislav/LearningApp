using LearningApp.api.Exceptions;
using LearningApp.api.Models;
using LearningApp.api.Repositories.interfaces;
using LearningApp.api.Storage;
using LearningApp.api.Storage.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.api.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext dbContext;

    public CourseRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await dbContext.Courses
            .AsNoTracking()
            .Select(data => data.ToDomainModel())
            .ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        var model = await dbContext.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(data => data.Id == id);

        return model?.ToDomainModel();
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        var model = course.ToDataModel();

        dbContext.Courses.Add(model);
        await dbContext.SaveChangesAsync();

        return model.ToDomainModel();
    }

    public async Task<Course> UpdateCourseAsync(Course course)
    {
        var model = await dbContext.Courses
            .FirstOrDefaultAsync(data => data.Id == course.Id);

        if (model is null)
        {
            throw new NotFoundException("Course not found");
        }

        model.Title = course.Title;
        model.Description = course.Description;

        await dbContext.SaveChangesAsync();

        return model.ToDomainModel();
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        var model = await dbContext.Courses
            .FirstOrDefaultAsync(data => data.Id == id);
        
        if (model is not null)
            dbContext.Courses.Remove(model);
        
        await dbContext.SaveChangesAsync();
    }
}