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
        dbContext.Lessons.Add(lesson.ToDataModel());
        await dbContext.SaveChangesAsync();

        var newLesson = await GetLesson(lesson.CourseId, lesson.Id);
        
        return newLesson ?? throw new NotFoundException("Lesson is not created");
    }

    public async Task<Lesson> UpdateLesson(Lesson lesson)
    {
        var lessonToUpdate = await dbContext.Lessons
            .Where(l => lesson.CourseId == l.CourseId)
            .Where(l => l.Id == lesson.Id)
            .FirstOrDefaultAsync();

        if (lessonToUpdate != null)
        {
            lessonToUpdate.Content = lesson.Content;
            lessonToUpdate.Title = lesson.Title;
            lessonToUpdate.Number = lesson.Number;

            dbContext.Lessons.Update(lessonToUpdate);
        }
        await dbContext.SaveChangesAsync();
        
        return await GetLesson(lesson.CourseId, lesson.Id) ?? throw new NotFoundException("Lesson not found");
    }

    public async Task DeleteLesson(Guid courseId, Guid lessonId)
    {
        var lesson = await GetLesson(courseId, lessonId);
        
        if  (lesson != null)
            dbContext.Lessons.Remove(lesson.ToDataModel());
        
        await dbContext.SaveChangesAsync();
    }
}