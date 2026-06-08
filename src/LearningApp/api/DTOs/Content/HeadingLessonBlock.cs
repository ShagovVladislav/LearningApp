using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class HeadingLessonBlock : LessonContentBlock
{
    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string Text { get; set; } = string.Empty;

    [Range(1, 6)]
    public int Level { get; set; }
}
