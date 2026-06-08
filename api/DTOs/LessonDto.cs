using LearningApp.api.DTOs.Content;

namespace LearningApp.api.DTOs;

public class LessonDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<LessonContentBlock> ContentBlocks { get; set; } = [];
    public int Number { get; set; }
}