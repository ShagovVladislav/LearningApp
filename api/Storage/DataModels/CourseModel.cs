namespace LearningApp.api.Storage.DataModels;

public class CourseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}