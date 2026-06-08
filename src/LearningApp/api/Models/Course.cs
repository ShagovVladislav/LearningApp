namespace LearningApp.api.Models;

public class Course
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}