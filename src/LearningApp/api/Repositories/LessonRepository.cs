using LearningApp.api.Exceptions;
using LearningApp.api.Models;
using LearningApp.api.Repositories.interfaces;
using LearningApp.api.Storage;
using LearningApp.api.Storage.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.api.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext dbContext;

    public LessonRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Lesson>> GetLessons(Guid courseId)
    {
        var lessons = await dbContext.Lessons
            .Where(l => courseId == l.CourseId)
            .OrderBy(l => l.Number)
            .Select(l => l.ToDomainModel())
            .ToListAsync();
        return lessons;
    }

    public async Task<Lesson?> GetLesson(Guid courseId, Guid lessonId)
    {
        var lesson = await dbContext.Lessons
            .Where(l => courseId == l.CourseId)
            .Where(l => l.Id == lessonId)
            .Select(l => l.ToDomainModel())
            .FirstOrDefaultAsync();

        return lesson;
    }

    public async Task<Lesson> AddLesson(Lesson lesson)
    {
        var lessonModel = lesson.ToDataModel();
        
        dbContext.Lessons.Add(lessonModel);
        await dbContext.SaveChangesAsync();

        return lessonModel.ToDomainModel();
    }

    public async Task<Lesson> UpdateLesson(Lesson lesson)
    {
        var lessonToUpdate = await dbContext.Lessons
            .Where(l => lesson.CourseId == l.CourseId)
            .Where(l => l.Id == lesson.Id)
            .FirstOrDefaultAsync();

        if (lessonToUpdate is null)
        {
            throw new NotFoundException("Lesson not found");
        }

        lessonToUpdate.ContentBlocks = lesson.ContentBlocks;
        lessonToUpdate.Title = lesson.Title;
        lessonToUpdate.Number = lesson.Number;
        
        await dbContext.SaveChangesAsync();
        
        return lessonToUpdate.ToDomainModel();
    }

    public async Task DeleteLesson(Guid courseId, Guid lessonId)
    {
        var lesson = await GetLesson(courseId, lessonId);
        
        if  (lesson != null)
            dbContext.Lessons.Remove(lesson.ToDataModel());
        
        await dbContext.SaveChangesAsync();
    }
}
