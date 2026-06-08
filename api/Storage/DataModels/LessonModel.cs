namespace LearningApp.api.Storage.DataModels;

public class LessonModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}