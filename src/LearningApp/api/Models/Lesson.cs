using LearningApp.api.DTOs.Content;

namespace LearningApp.api.Models;

public class Lesson
{
    public Guid Id { get; set; }
    public required string Title  { get; set; }
    public int Number  { get; set; }
    public List<LessonContentBlock> ContentBlocks { get; set; } = [];
    public Guid CourseId { get; set; }
}
