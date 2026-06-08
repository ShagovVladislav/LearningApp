using System.ComponentModel.DataAnnotations;
using LearningApp.api.Constants;

namespace LearningApp.api.DTOs;

public class LessonDto
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(DtoConstants.TitleMaxLength)]
    public required string Title { get; set; }
    
    [Required]
    [MaxLength(DtoConstants.LessonContentMaxLength)]
    public required string Content { get; set; }
    public int Number { get; set; }
}