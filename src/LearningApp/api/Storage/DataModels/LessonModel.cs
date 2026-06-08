using System.ComponentModel.DataAnnotations.Schema;
using LearningApp.api.DTOs.Content;

namespace LearningApp.api.Storage.DataModels;

public class LessonModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Number { get; set; }
    
    [Column(TypeName = "jsonb")]
    public List<LessonContentBlock> ContentBlocks { get; set; } = [];
    public Guid CourseId { get; set; }
}