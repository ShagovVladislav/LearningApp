using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;

namespace LearningApp.api.Services.Interfaces;

public interface ICourseService
{
    public Task<List<CourseDto>> GetCourses();
    public Task<CourseDto> GetCourse(Guid id);
    public Task<CourseDto> CreateCourse(CreateOrUpdateCourseRequest orUpdateCourse);
    public Task<CourseDto> UpdateCourse(CreateOrUpdateCourseRequest request, Guid courseId);
    public Task DeleteCourse(Guid id);
}