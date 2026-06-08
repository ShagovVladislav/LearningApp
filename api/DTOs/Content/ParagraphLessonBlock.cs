using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class ParagraphLessonBlock : LessonContentBlock
{
    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string Text { get; set; } = string.Empty;
}
