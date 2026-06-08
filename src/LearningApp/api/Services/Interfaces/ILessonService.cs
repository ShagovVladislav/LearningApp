using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;

namespace LearningApp.api.Services.Interfaces;

public interface ILessonService
{
    public Task<List<LessonDto>> GetLessons(Guid courseId);
    public Task<LessonDto> GetLesson(Guid courseId, Guid lessonId);
    public Task<LessonDto> UpdateLesson(Guid courseId, Guid lessonId, CreateOrUpdateLessonRequest request);
    public Task<LessonDto> AddLesson(Guid courseId, CreateOrUpdateLessonRequest request);
    public Task DeleteLesson(Guid courseId, Guid lessonId);
}