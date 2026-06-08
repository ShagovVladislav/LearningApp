using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class ImageLessonBlock : LessonContentBlock
{
    [Required]
    [Url]
    [MaxLength(DtoConstants.UrlMaxLength)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(DtoConstants.CourseDescriptionMaxLength)]
    public string? Alt { get; set; }

    [MaxLength(DtoConstants.CourseDescriptionMaxLength)]
    public string? Caption { get; set; }
}
