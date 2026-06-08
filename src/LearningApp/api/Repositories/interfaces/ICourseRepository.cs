using LearningApp.api.Models;

namespace LearningApp.api.Repositories.interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllCoursesAsync();

    Task<Course?> GetCourseByIdAsync(Guid id);

    Task<Course> CreateCourseAsync(Course course);

    Task<Course> UpdateCourseAsync(Course course);

    Task DeleteCourseAsync(Guid id);
}