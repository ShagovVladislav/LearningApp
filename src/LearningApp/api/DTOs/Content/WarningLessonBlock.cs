using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class WarningLessonBlock : LessonContentBlock
{
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string Text { get; set; } = string.Empty;

    [EnumDataType(typeof(WarningLevel))]
    public WarningLevel Level { get; set; }
}
