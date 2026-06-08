using LearningApp.api.Models;
using LearningApp.api.Storage.DataModels;

namespace LearningApp.api.Storage.Mappers;

public static class CourseMapperExtension
{
    public static Course ToDomainModel(this CourseModel data)
    {
        return new Course
        {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            
        };
    }

    public static CourseModel ToDataModel(this Course model)
    {
        return new CourseModel
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
        };
    }
}