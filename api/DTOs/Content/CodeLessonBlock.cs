using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class CodeLessonBlock : LessonContentBlock
{
    [EnumDataType(typeof(ProgramLanguage))]
    public ProgramLanguage Language { get; set; }

    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(DtoConstants.CourseDescriptionMaxLength)]
    public string? Caption { get; set; }
}
