using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs;

public class CourseDto
{
    public Guid CourseId { get; set; }
    
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public required string Title { get; set; }
    
    [MaxLength(DtoConstants.CourseDescriptionMaxLength)]
    public string? Description { get; set; }
    public List<LessonDto> Lessons { get; set; } = [];
}