namespace LearningApp.api.DTOs;

public class CourseDto
{
    public Guid CourseId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public List<LessonDto> Lessons { get; set; } = [];
}