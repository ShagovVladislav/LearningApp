using LearningApp.api.Models;

namespace LearningApp.api.Repositories.interfaces;

public interface ILessonRepository
{
    public Task<List<Lesson> > GetLessons(Guid courseId);
    public Task<Lesson?> GetLesson(Guid courseId, Guid lessonId);
    public Task<Lesson> AddLesson(Lesson lesson);
    public Task<Lesson> UpdateLesson(Lesson lesson);
    public Task DeleteLesson(Guid courseId, Guid lessonId);
}