namespace LearningApp.api.DTOs.requests;

public class CreateOrUpdateLessonRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int Number { get; set; }
}