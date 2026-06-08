using LearningApp.api.Models;
using LearningApp.api.Storage.DataModels;

namespace LearningApp.api.Storage.Mappers;

public static class LessonMapperExtension
{
    public static LessonModel ToDataModel(this Lesson lesson)
    {
        return new LessonModel
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Number = lesson.Number,
            CourseId = lesson.CourseId
        };
    }

    public static Lesson ToDomainModel(this LessonModel model)
    {
        return new Lesson
        {
            Id = model.Id,
            Title = model.Title,
            Content = model.Content,
            Number = model.Number,
            CourseId = model.CourseId,
        };
    }
}