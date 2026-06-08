using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.Content;

public class VideoLessonBlock : LessonContentBlock
{
    [Required]
    [Url]
    [MaxLength(DtoConstants.UrlMaxLength)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(DtoConstants.TitleMaxLength)]
    public string? Title { get; set; }
}
