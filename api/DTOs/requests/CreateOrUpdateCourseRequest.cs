namespace LearningApp.api.DTOs.requests;

public class CreateOrUpdateCourseRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}