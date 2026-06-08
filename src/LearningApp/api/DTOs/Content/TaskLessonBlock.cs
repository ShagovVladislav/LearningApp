using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class TaskLessonBlock : LessonContentBlock
{
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string? StarterCode { get; set; }

    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string? ExpectedResult { get; set; }
}
