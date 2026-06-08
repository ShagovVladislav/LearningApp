using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;
using LearningApp.api.Exceptions;
using LearningApp.api.Models;
using LearningApp.api.Repositories.interfaces;
using LearningApp.api.Services.Interfaces;

namespace LearningApp.api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILessonRepository _lessonRepository;

    public CourseService(ICourseRepository courseRepository, ILessonRepository lessonRepository)
    {
        _courseRepository = courseRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<List<CourseDto>> GetCourses()
    {
        var course = (await _courseRepository.GetAllCoursesAsync()).Select(CourseToDto).ToList();
        
        return course;
    }

    public async Task<CourseDto> GetCourse(Guid id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        
        if (course == null)
            throw new NotFoundException("Course not found");
        
        var courseDto = CourseToDto(course);
        courseDto.Lessons = (await _lessonRepository.GetLessons(course.Id)).Select(LessonService.LessonToDto).ToList();
        return courseDto;
    }

    public async Task<CourseDto> CreateCourse(CreateOrUpdateCourseRequest orUpdateCourse)
    {
        var courseId = Guid.NewGuid();
        var newCourse = await _courseRepository.CreateCourseAsync(new Course
        {
            Id = courseId,
            Description = orUpdateCourse.Description,
            Title = orUpdateCourse.Title,
        });
        
        return CourseToDto(newCourse);
    }

    public async Task<CourseDto> UpdateCourse(CreateOrUpdateCourseRequest courseRequest, Guid courseId)
    {
        var course = new Course
        {
            Id = courseId,
            Description = courseRequest.Description,
            Title = courseRequest.Title,
        };
        var updatedCourse = await _courseRepository.UpdateCourseAsync(course);
        
        return CourseToDto(updatedCourse);
    }

    public async Task DeleteCourse(Guid id)
    {
        await _courseRepository.DeleteCourseAsync(id);
    }

    private static CourseDto CourseToDto(Course course)
    {
        return new CourseDto
        {
            CourseId =  course.Id,
            Description = course.Description,
            Title = course.Title,
        };
    }
}
