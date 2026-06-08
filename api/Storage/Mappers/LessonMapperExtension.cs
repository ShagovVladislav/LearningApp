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
            ContentBlocks = lesson.ContentBlocks.OrderBy(l => l.Order).ToList(),
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
            ContentBlocks = model.ContentBlocks.OrderBy(l => l.Order).ToList(),
            Number = model.Number,
            CourseId = model.CourseId,
        };
    }
}