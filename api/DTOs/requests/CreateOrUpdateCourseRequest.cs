using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs.requests;

public class CreateOrUpdateCourseRequest
{
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public required string Title { get; set; }

    [MaxLength(DtoConstants.CourseDescriptionMaxLength)]
    public string? Description { get; set; }
}
