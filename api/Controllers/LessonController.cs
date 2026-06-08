using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;
using LearningApp.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.api.Controllers;

[ApiController]
[Route("api/courses/{courseId:guid}/lessons")]
public class LessonController : ControllerBase
{
    private readonly ILessonService lessonService;

    public LessonController(ILessonService lessonService)
    {
        this.lessonService = lessonService;
    }

    [HttpGet]
    public async Task<ActionResult<LessonDto>> GetLessons([FromRoute] Guid courseId)
    {
        var lessons = await lessonService.GetLessons(courseId);
        
        return Ok(lessons);
    }

    [HttpGet("{lessonId:guid}")]
    public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] Guid courseId, [FromRoute] Guid lessonId)
    {
        var lesson = await lessonService.GetLesson(courseId, lessonId);
        
        return Ok(lesson);
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> PostLesson([FromRoute] Guid courseId, CreateOrUpdateLessonRequest request)
    {
        var lesson = await lessonService.AddLesson(courseId, request);
        
        return CreatedAtAction(
            nameof(GetLesson),
            new { courseId, lessonId = lesson.Id },
            lesson);
    }
    
    [HttpPut("{lessonId:guid}")]
    public async Task<ActionResult<LessonDto>> UpdateLesson([FromRoute] Guid courseId, [FromRoute] Guid lessonId, CreateOrUpdateLessonRequest request)
    {
        var updatedLesson = await lessonService.UpdateLesson(courseId, lessonId, request);
        
        return Ok(updatedLesson);
    }

    
    [HttpDelete("{lessonId:guid}")]
    public async Task<IActionResult> DeleteLesson([FromRoute] Guid courseId, [FromRoute] Guid lessonId)
    {
        await lessonService.DeleteLesson(courseId, lessonId);
        
        return NoContent(); 
    }
}