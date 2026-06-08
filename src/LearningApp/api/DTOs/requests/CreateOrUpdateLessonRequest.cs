using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;
using LearningApp.api.DTOs.Content;

namespace LearningApp.api.DTOs.requests;

public class CreateOrUpdateLessonRequest
{
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public required string Title { get; set; }

    [Required]
    public List<LessonContentBlock> ContentBlocks { get; set; } = [];

    [Range(0, int.MaxValue)]
    public int Number { get; set; }
}
