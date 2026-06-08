using LearningApp.api.DTOs;
using LearningApp.api.DTOs.requests;
using LearningApp.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.api.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService; 
    
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetCourses()
    {
        var courses = await _courseService.GetCourses();
        
        return Ok(courses);
    }
    
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] Guid courseId)
    {
        var course = await _courseService.GetCourse(courseId);
        
        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> PostCourse(CreateOrUpdateCourseRequest courseRequest)
    {
        var course = await _courseService.CreateCourse(courseRequest);
        
        return CreatedAtAction(
            nameof(GetCourse),
            new { courseId = course.CourseId},
            course);
    }

    [HttpDelete("{courseId:guid}")]
    public async Task<ActionResult<CourseDto>> DeleteCourse([FromRoute] Guid courseId)
    {
        await  _courseService.DeleteCourse(courseId);
        
        return NoContent();
    }

    [HttpPut("{courseId:guid}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse([FromRoute] Guid courseId, CreateOrUpdateCourseRequest courseRequest)
    {
        var updatedCourse = await _courseService.UpdateCourse(courseRequest, courseId);
        
        return Ok(updatedCourse);
    }
    
}