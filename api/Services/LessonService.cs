using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;
using LearningApp.api.Exceptions;
using LearningApp.api.Models;
using LearningApp.api.Repositories.interfaces;
using LearningApp.api.Services.Interfaces;

namespace LearningApp.api.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        this.lessonRepository = lessonRepository;
    }

    public async Task<List<LessonDto>> GetLessons(Guid courseId)
    {
        var lessons = await lessonRepository.GetLessons(courseId);

        var lessonDtos = lessons.Select(LessonToDto).ToList();
        
        return lessonDtos;
    }

    public async Task<LessonDto> GetLesson(Guid courseId, Guid lessonId)
    {
        var lesson = await lessonRepository.GetLesson(courseId, lessonId);

        return lesson is null 
            ? throw new NotFoundException("Lesson not found") 
            : LessonToDto(lesson);
    }

    public async Task<LessonDto> UpdateLesson(Guid courseId, Guid lessonId, CreateOrUpdateLessonRequest request)
    {
        var lesson = GetLessonFromRequest(courseId, lessonId, request);
        
        var updatedLesson = await lessonRepository.UpdateLesson(lesson);
        
        return LessonToDto(updatedLesson);
    }

    public async Task<LessonDto> AddLesson(Guid courseId, CreateOrUpdateLessonRequest request)
    {
        var lesson = GetLessonFromRequest(courseId, Guid.NewGuid(), request);
        var newLesson = await lessonRepository.AddLesson(lesson);

        return LessonToDto(newLesson);
    }

    public async Task DeleteLesson(Guid courseId, Guid lessonId)
    {
        await lessonRepository.DeleteLesson(courseId, lessonId);
    }
    
    public static LessonDto LessonToDto(Lesson lesson)
    {
        return new LessonDto
        {
            Title = lesson.Title,
            ContentBlocks = lesson.ContentBlocks,
            Id = lesson.Id,
            Number = lesson.Number,
        };
    }

    private static Lesson DtoToLesson(LessonDto lessonDto, Guid courseId)
    {
        return new Lesson
        {
            Id = lessonDto.Id,
            Title = lessonDto.Title,
            ContentBlocks = lessonDto.ContentBlocks,
            CourseId = courseId,
            Number =  lessonDto.Number,
        };
    }
    
    private static Lesson GetLessonFromRequest(Guid courseId, Guid lessonId, CreateOrUpdateLessonRequest request)
    {
        var lesson = new Lesson
        {
            Id = lessonId,
            Title = request.Title,
            ContentBlocks = request.ContentBlocks,
            Number = request.Number,
            CourseId = courseId,
        };
        return lesson;
    }
}